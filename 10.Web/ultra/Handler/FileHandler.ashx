<%@ WebHandler Language="C#" Class="FileHandler" %>

using System;
using System.Web;
using System.Web.Security;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.IO;
//using System.Security.Principal;
//using System.Transactions;
using Bayer.Ultra.Framework.Config;
using Bayer.Ultra.Framework.Common.Dto.Approval;
using Bayer.Ultra.Framework.Common.Dto.Radiology;

public class FileHandler : IHttpHandler  
{


    private static readonly FilesDisposition FILES_DISPOSITION = FilesDisposition.Absolute;
    private static readonly string FILES_TEMP_PATH = WebSiteConfigHandler.WebServer.UploadFile.Temp.PhysicalPath;
    private static readonly string FILES_ATTACH_PATH = WebSiteConfigHandler.WebServer.UploadFile.Attach.PhysicalPath;
    private static readonly string FILE_QUERY_VAR = "file";
    private static readonly string FILE_HANDLE_METHOD = "method";
    private static readonly string FILE_GET_CONTENT_TYPE = "application/octet-stream";

    private static readonly int ATTEMPTS_TO_WRITE = 3;
    private static readonly int ATTEMPT_WAIT = 1000; //msec

    private static readonly int BUFFER_SIZE = 4 * 1024 * 1024;


    private enum FilesDisposition
    {
        ServerRoot,
        HandlerRoot,
        Absolute
    }

    private static class HttpMethods
    {
        public static readonly string GET = "GET";
        public static readonly string POST = "POST";
        public static readonly string DELETE = "DELETE";


    }

    [DataContract]
    private class UploaderResponse
    {
        [DataMember]
        public EventAttachFileResponseDto[] files;

        public UploaderResponse(EventAttachFileResponseDto[] fileResponses)
        {
            String username_id = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            files = fileResponses;

        }
    }

    private static string CreateFileUrl(HttpRequest request, string fileName, FilesDisposition filesDisposition)
    {
        switch (filesDisposition)
        {
            case FilesDisposition.ServerRoot:
                // 1. files directory lies in root directory catalog WRONG
                return String.Format("{0}{1}/{2}", request.Url.GetLeftPart(UriPartial.Authority),
                    FILES_TEMP_PATH, Path.GetFileName(fileName));

            case FilesDisposition.HandlerRoot:
                // 2. files directory lays in current page catalog WRONG
                return String.Format("{0}{1}{2}/{3}", request.Url.GetLeftPart(UriPartial.Authority),
                    Path.GetDirectoryName(request.CurrentExecutionFilePath).Replace(@"\", @"/"), FILES_TEMP_PATH, Path.GetFileName(fileName));

            case FilesDisposition.Absolute:
                // 3. files directory lays anywhere YEAH
                return String.Format("{0}?{1}={2}", request.Url.AbsoluteUri, FILE_QUERY_VAR, HttpUtility.UrlEncode(Path.GetFileName(fileName)));
            default:
                return String.Empty;
        }
    }

    private static EventAttachFileResponseDto CreateFileResponse(HttpRequest request, int idx, string displayName, string savedName, string filePath, long size, string error, string attachType)
    {
        return new EventAttachFileResponseDto()
        {
            Index = idx,
            DisplayName = HttpUtility.UrlEncode(displayName),
            SavedName =  HttpUtility.UrlEncode(savedName),
            FileSize = size,
            AttachType = attachType,
            FilePath = HttpUtility.UrlEncode(filePath),
            FileHandlerUrl = CreateFileUrl(request, savedName, FILES_DISPOSITION),
            ErrorMessage = error
        };
    }

    private static void SerializeUploaderResponse(HttpResponse response, List<EventAttachFileResponseDto> fileResponses)
    {

        var Serializer = new global::System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(UploaderResponse));

        Serializer.WriteObject(response.OutputStream, new UploaderResponse(fileResponses.ToArray()));
    }

    private static void FromStreamToStream(Stream source, Stream destination)
    {


        int BufferSize = source.Length >= BUFFER_SIZE ? BUFFER_SIZE : (int)source.Length;
        long BytesLeft = source.Length;

        byte[] Buffer = new byte[BufferSize];

        int BytesRead = 0;

        while (BytesLeft > 0)
        {
            BytesRead = source.Read(Buffer, 0, BytesLeft > BufferSize ? BufferSize : (int)BytesLeft);

            destination.Write(Buffer, 0, BytesRead);

            BytesLeft -= BytesRead;
        }
    }

    #region IHttpAsyncHandler

    private AsyncProcessorDelegate RequestDelegate;
    private delegate void AsyncProcessorDelegate(HttpContext context);

    public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
    {


        RequestDelegate = new AsyncProcessorDelegate(ProcessRequest);
        String username_id = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        return RequestDelegate.BeginInvoke(context, cb, extraData);
    }

    public void EndProcessRequest(IAsyncResult result)
    {
        
        RequestDelegate.EndInvoke(result);
        
    }

    public bool IsReusable
    {
        get { return false; }
    }

    public void ProcessRequest(HttpContext context)
    {
        

        //string username_id = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        string FilesPath;
        int AttachIndex = 0;
        string UserID = string.Empty;
        string ProcessID = string.Empty;
        string AttachType = string.Empty;
        string Status = string.Empty;
        string Delete = string.Empty;
        string MoveFiles = null;
        int ReferIDX = 0;
        bool IsAttachFile = true; //Temp가 아닌 Attach인 경우 true이면 db도 반영
        switch (FILES_DISPOSITION)
        {
            case FilesDisposition.ServerRoot:
                FilesPath = context.Server.MapPath(FILES_TEMP_PATH);
                break;
            case FilesDisposition.HandlerRoot:
                FilesPath = context.Server.MapPath(Path.GetDirectoryName(context.Request.CurrentExecutionFilePath) + FILES_TEMP_PATH);
                break;
            case FilesDisposition.Absolute: //사용자/프로세스아이디/타입
                AttachIndex = Convert.ToInt32(context.Request.Form["Index"] ?? "0");
                UserID = context.Request.Form["UserID"];
                ProcessID = context.Request.Form["ProcessID"];
                Delete = context.Request.Form["Delete"] ?? string.Empty;
                AttachType = context.Request.Form["AttachType"] ?? string.Empty;
                Status = context.Request.Form["Status"] ?? string.Empty;
                MoveFiles = HttpUtility.UrlDecode(context.Request.Form["MoveFiles"] ?? string.Empty);
                ReferIDX = Convert.ToInt32(context.Request.Form["ReferIDX"] ?? "0");
                //Temporary폴더에 저장하는 경우
                //   1. 프로세스 상태가 Temp이고 EventCommon인 경우는
                //   2. Agenda Role인경우 화면에서 신규이고 첨부만 올린 경우
                //   3. Input Comment
                //   4. Payment SRM
                //나머지는 Attach폴더에 직접 저장

                //DB에 저장 Skip
                if (Status.ToLower().Equals("temp"))
                {
                    //Process Status가 Temp이면 Temp로
                    IsAttachFile = false;
                }
                else if (AttachType.ToLower().Equals("inputcomment") && !Status.ToLower().Equals("move"))
                {
                    //Input Comment이면서 move가 아니면 우선 Temp로
                    IsAttachFile = false;
                }
                else if (AttachType.ToLower().Equals("agendarole"))
                {
                    if (AttachIndex == 0 && ReferIDX == 0)
                    {
                        // Agenda Role이면서 신규로 등록시에는 Temp로
                        IsAttachFile = false;
                    }
                }
                else if (AttachType.ToLower().Equals("paymentsrm"))
                {
                    //Payment의 SRM upload인 경우 
                    IsAttachFile = false;
                }
                else if (AttachType.ToLower().Equals("eventcompleted"))
                {
                    IsAttachFile = false;
                }
                else if (AttachType.ToLower().Equals("iccmaster") && Status.ToLower().Equals("temp"))
                {
                    //ICC Master이면서 move가 아니면 우선 Temp로
                    IsAttachFile = false;
                }
                else if (AttachType.ToLower().Equals("reportnononekey") && Status.ToLower().Equals("temp"))
                {
                    //reportnononekey이면서 move가 아니면 우선 Temp로
                    IsAttachFile = false;
                }

                string filesPath = string.Empty;
                // move인경우는 file을 move하기 전 source위치를 알기 위해
                if (!IsAttachFile || Status.ToLower().Equals("move"))
                    filesPath = FILES_TEMP_PATH;
                else
                    filesPath = FILES_ATTACH_PATH;

                if (AttachType.ToLower().Equals("iccmaster"))
                    filesPath = string.Format(@"{0}\{1}\{2}", filesPath, AttachType, ProcessID);
                else if (AttachType.ToLower().Equals("reportnononekey"))
                {
                    string dir = "NEW_ID";
                    if (IsAttachFile && ProcessID != null) dir = ProcessID;
                    filesPath = string.Format(@"{0}\{1}\{2}", filesPath, AttachType, dir);
                }
                else
                    filesPath = string.Format(@"{0}\{1}\{2}\{3}", filesPath, Bayer.Ultra.Core.Consts.FILES_EVENT_ROOT_DIR, ProcessID, AttachType);

                FilesPath = filesPath;
                break;
            default:
                context.Response.StatusCode = 500;
                context.Response.StatusDescription = "Configuration error (FILES_DISPOSITION)";
                return;
        }

        // prepare directory
        if (!Directory.Exists(FilesPath))
        {
            Directory.CreateDirectory(FilesPath);
        }
        //username_id = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        string QueryFileName = context.Request[FILE_QUERY_VAR];
        string FullFileName = null;
        string ShortFileName = null;

        //if (!String.IsNullOrEmpty(QueryFileName))
        if (QueryFileName != null) // param specified, but maybe in wrong format (empty). else user will download json with listed files
        {
            ShortFileName = HttpUtility.UrlDecode(QueryFileName);

            if (ShortFileName.Contains(Bayer.Ultra.Core.Consts.FILES_ATTACH_PATH_PREFIX) || ShortFileName.Contains(Bayer.Ultra.Core.Consts.FILES_TEMP_PATH_PREFIX))
            {
                //파일 다운로드시
                FullFileName = ShortFileName.Replace(Bayer.Ultra.Core.Consts.FILES_TEMP_PATH_PREFIX, FILES_TEMP_PATH).Replace(Bayer.Ultra.Core.Consts.FILES_ATTACH_PATH_PREFIX, FILES_ATTACH_PATH);
            }
            else
            {
                //파일 삭제시
                FullFileName = String.Format(@"{0}\{1}", FilesPath, ShortFileName);
            }

            if (QueryFileName.Trim().Length == 0 || !File.Exists(FullFileName))
            {
                context.Response.StatusCode = 404;
                context.Response.StatusDescription = "File not found";

                context.Response.End();
                return;
            }
        }

        if (context.Request.HttpMethod.ToUpper() == HttpMethods.GET)
        {
            if (FullFileName != null)
            {
                context.Response.ContentType = FILE_GET_CONTENT_TYPE;                   // http://www.digiblog.de/2011/04/android-and-the-download-file-headers/ :)
                string fileName = string.Format("{0}{1}", Path.GetFileNameWithoutExtension(ShortFileName), Path.GetExtension(ShortFileName));
                fileName = HttpUtility.UrlEncode(fileName, new System.Text.UTF8Encoding()).Replace("+", "%20");
                context.Response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}", fileName));

                using (FileStream FileReader = new FileStream(FullFileName, FileMode.Open, FileAccess.Read))
                {
                    FromStreamToStream(FileReader, context.Response.OutputStream);
                    context.Response.OutputStream.Close();
                }
                context.Response.End();
                return;
            }
            else
            {
                List<EventAttachFileResponseDto> FileResponseList = new List<EventAttachFileResponseDto>();

                string[] FileNames = Directory.GetFiles(FilesPath);

                foreach (string FileName in FileNames)
                {
                    //현재는 미사용
                    FileResponseList.Add(CreateFileResponse(context.Request, 0, FileName, FileName, string.Empty, new FileInfo(FileName).Length, string.Empty, string.Empty));
                }

                SerializeUploaderResponse(context.Response, FileResponseList);
            }
        }
        else if (context.Request.HttpMethod.ToUpper() == HttpMethods.POST && !Delete.Equals("Y"))
        {

            List<EventAttachFileResponseDto> FileResponseList = new List<EventAttachFileResponseDto>();

            if (Status.ToLower().Equals("move"))
            {
                string[] files = MoveFiles.Split(new char[] { ';' });

                //Non onekey 일 경우
                //신규 등록일 경우
                if (AttachType.ToLower().Equals("reportnononekey")) {
                    int index = 0;
                    int NON_ONEKEY_ID = 0;

                    //using (TransactionScope scope = new TransactionScope())
                    //{
                    //게시물 등록
                    try
                    {
                        using (Bayer.Ultra.BSL.Report.Mgr.NonOneKeyMgr mgr = new Bayer.Ultra.BSL.Report.Mgr.NonOneKeyMgr())
                        {
                            NON_ONEKEY_ID = mgr.MergeCustomerData(new MergeCustomerDto()
                            {
                                REQUESTER_ID = context.Request.Form["REQUESTER_ID"],
                                REQUEST_TYPE = context.Request.Form["REQUEST_TYPE"],
                                CUSTOMER_TYPE = context.Request.Form["CUSTOMER_TYPE"],
                                CUSTOMER_NAME = context.Request.Form["CUSTOMER_NAME"],
                                GENDER = context.Request.Form["GENDER"],
                                ORGANIZATION_ID = context.Request.Form["ORGANIZATION_ID"],
                                ORGANIZATION_NAME = context.Request.Form["ORGANIZATION_NAME"],
                                NON_ONEKEY_STATUS = context.Request.Form["NON_ONEKEY_STATUS"],
                                REMARK = context.Request.Form["REMARK"],
                                CREATOR_ID = context.Request.Form["CREATOR_ID"]
                            });
                        }
                    }
                    catch
                    {
                        throw;
                    }

                    //파일 업로드
                    for (int FileIndex = 0; FileIndex < files.Length; FileIndex++)
                    {
                        if (files[FileIndex].Length < 1) continue;
                        string ErrorMessage = string.Empty;


                        string SavedFileName = string.Format(@"{0}\{1}", FilesPath, files[FileIndex]); //Temp폴더

                        string DestFolder = FilesPath.Replace(FILES_TEMP_PATH, FILES_ATTACH_PATH).Replace("NEW_ID", NON_ONEKEY_ID.ToString());
                        // prepare directory
                        if (!Directory.Exists(DestFolder))
                            Directory.CreateDirectory(DestFolder);

                        //string DestFileName = SavedFileName.Replace(FILES_TEMP_PATH, FILES_ATTACH_PATH);
                        string DestFileName = SavedFileName.Replace(FILES_TEMP_PATH, FILES_ATTACH_PATH).Replace("NEW_ID", NON_ONEKEY_ID.ToString());

                        int fileSize = 0;
                        string filePath = string.Empty;
                        for (int Attempts = 0; Attempts < ATTEMPTS_TO_WRITE; Attempts++)
                        {
                            try
                            {
                                //파일 이동(Temp에서 Attach폴더로)
                                File.Move(SavedFileName, DestFileName);
                                string fileHandlerUrl = CreateFileUrl(context.Request, DestFileName, FILES_DISPOSITION);
                                fileSize = (int)(new FileInfo(DestFileName).Length);
                                filePath = DestFileName.Replace(FILES_TEMP_PATH, Bayer.Ultra.Core.Consts.FILES_TEMP_PATH_PREFIX).Replace(FILES_ATTACH_PATH, Bayer.Ultra.Core.Consts.FILES_ATTACH_PATH_PREFIX).Replace("temp123456789", NON_ONEKEY_ID.ToString());
                                try
                                {
                                    using (Bayer.Ultra.BSL.Report.Mgr.NonOneKeyMgr mgr = new Bayer.Ultra.BSL.Report.Mgr.NonOneKeyMgr())
                                    {
                                        index = mgr.InsertAttach(new NonOnekeyAttachDto()
                                        {
                                            NON_ONEKEY_ID = NON_ONEKEY_ID,
                                            ATTACH_FILE_TYPE = AttachType,
                                            SEQ = Attempts,
                                            DISPLAY_FILE_NAME = Path.GetFileName(DestFileName),
                                            SAVED_FILE_NAME = Path.GetFileName(DestFileName),
                                            FILE_SIZE = fileSize,
                                            FILE_PATH = filePath,
                                            FILE_HANDLER_URL = fileHandlerUrl,
                                            REFER_IDX = ReferIDX,
                                            IS_DELETED = "N",
                                            CREATOR_ID = UserID,
                                        });
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ErrorMessage = ex.Message;
                                    System.Threading.Thread.Sleep(ATTEMPT_WAIT);
                                    try
                                    {
                                        System.IO.File.Delete(DestFileName);
                                    }
                                    catch
                                    {

                                    }
                                    continue;
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorMessage = ex.Message;
                                continue;
                            }

                            break;
                        }
                        FileResponseList.Add(CreateFileResponse(context.Request, index, Path.GetFileName(DestFileName), Path.GetFileName(DestFileName), filePath, fileSize, ErrorMessage, AttachType));
                    }
                    // scope.Complete();
                    //}
                }
                //Non onekey 아닐 경우 (기존)
                else
                {
                    for (int FileIndex = 0; FileIndex < files.Length; FileIndex++)
                    {
                        if (files[FileIndex].Length < 1) continue;
                        string ErrorMessage = string.Empty;
                        int index = 0;
                        string SavedFileName = string.Format(@"{0}\{1}", FilesPath, files[FileIndex]); //Temp폴더

                        string DestFolder = FilesPath.Replace(FILES_TEMP_PATH, FILES_ATTACH_PATH);
                        // prepare directory
                        if (!Directory.Exists(DestFolder))
                            Directory.CreateDirectory(DestFolder);

                        string DestFileName = SavedFileName.Replace(FILES_TEMP_PATH, FILES_ATTACH_PATH);
                        int fileSize = 0;
                        string filePath = string.Empty;
                        for (int Attempts = 0; Attempts < ATTEMPTS_TO_WRITE; Attempts++)
                        {
                            try
                            {
                                //파일 이동(Temp에서 Attach폴더로)
                                File.Move(SavedFileName, DestFileName);
                                string fileHandlerUrl = CreateFileUrl(context.Request, DestFileName, FILES_DISPOSITION);
                                fileSize = (int)(new FileInfo(DestFileName).Length);
                                filePath = DestFileName.Replace(FILES_TEMP_PATH, Bayer.Ultra.Core.Consts.FILES_TEMP_PATH_PREFIX).Replace(FILES_ATTACH_PATH, Bayer.Ultra.Core.Consts.FILES_ATTACH_PATH_PREFIX);
                                try
                                {

                                    using (Bayer.Ultra.BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new Bayer.Ultra.BSL.Approval.Mgr._ApprovalMgr_Tx())
                                    {
                                        index = mgr.InsertEventAttachFiles(new DTO_EVENT_ATTACH_FILES()
                                        {
                                            PROCESS_ID = ProcessID,
                                            ATTACH_FILE_TYPE = AttachType,
                                            SEQ = 0,
                                            DISPLAY_FILE_NAME = Path.GetFileName(DestFileName),
                                            SAVED_FILE_NAME = Path.GetFileName(DestFileName),
                                            FILE_SIZE = fileSize,
                                            FILE_PATH = filePath,
                                            FILE_HANDLER_URL = fileHandlerUrl,
                                            REFER_IDX = ReferIDX,
                                            IS_DELETED = "N",
                                            CREATOR_ID = UserID,
                                        });
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ErrorMessage = ex.Message;
                                    System.Threading.Thread.Sleep(ATTEMPT_WAIT);
                                    try
                                    {
                                        System.IO.File.Delete(DestFileName);
                                    }
                                    catch
                                    {

                                    }
                                    continue;
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorMessage = ex.Message;
                                continue;
                            }

                            break;
                        }
                        FileResponseList.Add(CreateFileResponse(context.Request, index, Path.GetFileName(DestFileName), Path.GetFileName(DestFileName), filePath, fileSize, ErrorMessage, AttachType));
                    }
                }
            }
            else
            {
                //Move가 아닌경우
                for (int FileIndex = 0; FileIndex < context.Request.Files.Count; FileIndex++)
                {
                    HttpPostedFile File = context.Request.Files[FileIndex];

                    string DisplayFileName = string.Format(@"{0}\{1}", FilesPath, Path.GetFileName(File.FileName));
                    string SavedFileName = string.Empty;
                    string ErrorMessage = string.Empty;
                    int index = 0;
                    string filePath = string.Empty;
                    for (int Attempts = 0; Attempts < ATTEMPTS_TO_WRITE; Attempts++)
                    {
                        ErrorMessage = string.Empty;

                        if (System.IO.File.Exists(DisplayFileName))
                        {
                            SavedFileName = string.Format(@"{0}\{1}_{2:yyyyMMddHHmmss.fff}{3}", FilesPath, Path.GetFileNameWithoutExtension(DisplayFileName), DateTime.Now, Path.GetExtension(DisplayFileName));
                        }
                        else
                            SavedFileName = DisplayFileName;

                        try
                        {
                            using (Stream FileStreamWriter = new FileStream(SavedFileName, FileMode.CreateNew, FileAccess.Write))
                            {
                                var inputStream = context.Request.Files.Get(0).InputStream;
                                FromStreamToStream(inputStream, FileStreamWriter);
                            }
                            filePath = SavedFileName.Replace(FILES_TEMP_PATH, Bayer.Ultra.Core.Consts.FILES_TEMP_PATH_PREFIX).Replace(FILES_ATTACH_PATH, Bayer.Ultra.Core.Consts.FILES_ATTACH_PATH_PREFIX);
                            //프로세스 상태가 Temp가 아닌경우만  db에 저장                        
                            if (IsAttachFile)
                            {
                                string fileHandlerUrl = CreateFileUrl(context.Request, SavedFileName, FILES_DISPOSITION);
                                try
                                {
                                    //Non onekey 일 경우
                                    if (AttachType.ToLower().Equals("reportnononekey"))
                                    {
                                        using (Bayer.Ultra.BSL.Report.Mgr.NonOneKeyMgr mgr = new Bayer.Ultra.BSL.Report.Mgr.NonOneKeyMgr())
                                        {
                                            int NON_ONEKEY_ID = Convert.ToInt32(context.Request.Form["ProcessID"]);
                                            filePath = filePath.Replace("NEW_ID", NON_ONEKEY_ID.ToString());
                                            index = mgr.InsertAttach(new NonOnekeyAttachDto()
                                            {
                                                NON_ONEKEY_ID = NON_ONEKEY_ID,
                                                ATTACH_FILE_TYPE = AttachType,
                                                SEQ = Attempts,
                                                DISPLAY_FILE_NAME = Path.GetFileName(DisplayFileName),
                                                SAVED_FILE_NAME = Path.GetFileName(SavedFileName),
                                                FILE_SIZE = File.ContentLength,
                                                FILE_PATH = filePath,
                                                FILE_HANDLER_URL = fileHandlerUrl,
                                                REFER_IDX = ReferIDX,
                                                IS_DELETED = "N",
                                                CREATOR_ID = UserID,
                                            });
                                        }
                                    }
                                    else
                                    {
                                        //index=test_db();
                                        using (Bayer.Ultra.BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new Bayer.Ultra.BSL.Approval.Mgr._ApprovalMgr_Tx())
                                        {
                                         //   System.Security.Principal.WindowsImpersonationContext impersonationContext;
                                        
                                        //    impersonationContext = ((System.Security.Principal.WindowsIdentity)User.Identity).Impersonate();
                                            //username_id = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                                            index = mgr.InsertEventAttachFiles(new DTO_EVENT_ATTACH_FILES()
                                            {
                                                PROCESS_ID = ProcessID,
                                                ATTACH_FILE_TYPE = AttachType,
                                                SEQ = 0,
                                                DISPLAY_FILE_NAME = Path.GetFileName(DisplayFileName),
                                                SAVED_FILE_NAME = Path.GetFileName(SavedFileName),
                                                FILE_SIZE = File.ContentLength,
                                                FILE_PATH = filePath,
                                                FILE_HANDLER_URL = fileHandlerUrl,
                                                REFER_IDX = ReferIDX,
                                                IS_DELETED = "N",
                                                CREATOR_ID = UserID,
                                            });
                                            //impersonationContext.Undo();
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ErrorMessage = ex.Message;
                                    context.Response.Write("ttttttt");
                                    System.Threading.Thread.Sleep(ATTEMPT_WAIT);

                                    try
                                    {
                                        System.IO.File.Delete(SavedFileName);
                                    }
                                    catch
                                    {

                                    }
                                    continue;
                                }
                            }
                        }
                        catch (Exception exception)
                        {
                            ErrorMessage = exception.Message;
                            context.Response.Write("aaaaa");
                            System.Threading.Thread.Sleep(ATTEMPT_WAIT);
                            continue;
                        }

                        break;
                    }

                    FileResponseList.Add(CreateFileResponse(context.Request, index, Path.GetFileName(DisplayFileName), Path.GetFileName(SavedFileName), filePath, File.ContentLength, ErrorMessage, AttachType));
                }
            }
            SerializeUploaderResponse(context.Response, FileResponseList);
        }
        else if (context.Request.HttpMethod.ToUpper() == HttpMethods.DELETE || Delete.Equals("Y"))
        {
            bool SuccessfullyDeleted = true;

            try
            {

                if (AttachType.ToLower().Equals("reportnononekey"))
                {
                    using (Bayer.Ultra.BSL.Report.Mgr.NonOneKeyMgr mgr = new Bayer.Ultra.BSL.Report.Mgr.NonOneKeyMgr())
                    {
                        mgr.DeleteAttach(AttachIndex, UserID);
                        File.Delete(FullFileName);
                    }
                }
                else if (AttachType.ToLower().Equals("iccmaster"))
                {
                    using (Bayer.Ultra.BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new Bayer.Ultra.BSL.Approval.Mgr._ApprovalMgr_Tx())
                    {
                        mgr.DeleteICCAttachFiles(AttachIndex, UserID);
                    }
                }
                else if (!AttachType.ToLower().Equals("paymentsrm"))
                {
                    using (Bayer.Ultra.BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new Bayer.Ultra.BSL.Approval.Mgr._ApprovalMgr_Tx())
                    {
                        mgr.DeleteEventAttachFiles(AttachIndex, UserID);
                    }
                }

                //File.Delete(FullFileName);
            }
            catch
            {
                SuccessfullyDeleted = false;
            }
            context.Response.Write(String.Format("{{\"{0}\":{1}}}", ShortFileName, SuccessfullyDeleted.ToString().ToLower()));
        }
        else
        {
            context.Response.StatusCode = 405;
            context.Response.StatusDescription = "Method not allowed";
            context.Response.End();

            return;
        }


        context.Response.End();
    }

    public int test_db()
    {
        int index = 1;
        using (Bayer.Ultra.BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new Bayer.Ultra.BSL.Approval.Mgr._ApprovalMgr_Tx())
        {
            //System.Security.Principal.WindowsImpersonationContext impersonationContext;

            //impersonationContext = ((System.Security.Principal.WindowsIdentity)User.Identity).Impersonate();
            string username_id = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            //index = mgr.InsertEventAttachFiles(new DTO_EVENT_ATTACH_FILES()
            //{
            //    PROCESS_ID = ProcessID,
            //    ATTACH_FILE_TYPE = AttachType,
            //    SEQ = 0,
            //    DISPLAY_FILE_NAME = Path.GetFileName(DisplayFileName),
            //    SAVED_FILE_NAME = Path.GetFileName(SavedFileName),
            //    FILE_SIZE = File.ContentLength,
            //    FILE_PATH = filePath,
            //    FILE_HANDLER_URL = fileHandlerUrl,
            //    REFER_IDX = ReferIDX,
            //    IS_DELETED = "N",
            //    CREATOR_ID = UserID,
            //});
            //impersonationContext.Undo();
        }
        return index;
    }

    #endregion

}