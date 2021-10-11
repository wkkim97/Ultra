using Bayer.Ultra.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.IO;

using Bayer.Ultra.Framework.Config;
using Bayer.Ultra.BSL.Approval.Mgr;
using Bayer.Ultra.Framework.Common.Dto.Approval;

public partial class Handler_FileDownHandler : System.Web.UI.Page
{
    //private static readonly string FILES_PATH = @"C:\Temp\uploader";  //WebSiteConfigHandler.WebServer.UploadFile.Temp.PhysicalPath;
    //private static readonly string SAMPLE_FILES_PATH = @"C:\Temp\sample_srm"; //WebSiteConfigHandler.WebServer.UploadFile.Temp.PhysicalPath;
    //private static readonly string ATTACH_FILES_PATH = WebSiteConfigHandler.WebServer.UploadFile.Attach.PhysicalPath;


    //protected void CreateExcelFile(string strSourceFileName, string strDestPathCombine, string strDestFileName)
    //{

    //    if (!Directory.Exists(strDestPathCombine)) Directory.CreateDirectory(strDestPathCombine);

    //    // 기존 엑셀파일이 존재하는 경우는 덮어쓰기 함.
    //    File.Copy(strSourceFileName, strDestFileName, true);
    //}


    protected void Page_Load(object sender, EventArgs e)
    {
        // 버퍼링 없이 바로 다운로드 받게 한다.
        Page.Response.Clear();
        Page.Response.BufferOutput = false;

        FileInfo ofileinfo = null;

        string strDestFileName = string.Empty;
        //string strProcessID = string.Empty;
        //string userId = string.Empty;
        //string fileName = string.Empty;

        try
        {
            strDestFileName = Request["DestFileName"].NullObjectToEmptyEx();


            #region 주석처리 (공통으로 분리)

            //strProcessID = Request["processID"].NullObjectToEmptyEx();
            //userId = Request["userID"].NullObjectToEmptyEx();
            //fileName = "SRM_Report.xlsx";

            //// sample 폴더에서 Excel 파일 복사
            //string strSourceFileName = Path.Combine(SAMPLE_FILES_PATH, fileName);
            //string strDestPathCombine = Path.Combine(ATTACH_FILES_PATH, userId, strProcessID);
            //string strDestFileName = Path.Combine(strDestPathCombine, fileName);


            //CreateExcelFile(strSourceFileName, strDestPathCombine, strDestFileName);


            //// ParticipantsList 를 조회하여 Excel 파일에 리스트로 입력
            //List<DTO_MODULE_PARTICIPANTS> dtoParticipantsList = null;


            //// ProcessID 로 참여자 조회


            //using (_ApprovalMgr_Nx mgr = new _ApprovalMgr_Nx())
            //{
            //    dtoParticipantsList = mgr.SelectParticipants(strProcessID);

            //    // 참여자가 존재하는 경우
            //    if (dtoParticipantsList.Count > 0)
            //    {

            //        foreach (DTO_MODULE_PARTICIPANTS dtoParticipant in dtoParticipantsList)
            //        {

            //            //dtoParticipant.
            //        }
            //    }
            //    else  // 참여자가 존재하지 않는 경우
            //    {

            //    }
            //}

            #endregion
            //if (strProcessID.IsNullOrEmptyEx())
            //{
            //    using (Bayer.eWF.BSL.Common.Mgr.FileMgr mgr = new Bayer.eWF.BSL.Common.Mgr.FileMgr())
            //    {
            //        fileInfo = mgr.SelectAttachFileInfo(Convert.ToInt32(attachIDX));
            //    }
            //    ofileinfo = new FileInfo(fileInfo.FILE_PATH);
            //}
            //else
            //{
            //    string strTempUploadPath = DNSoft.eW.FrameWork.eWBase.GetConfig("//ServerInfo/WebServer/FileMgr/UploadTempFolder");
            //    strTempUploadPath = string.Format(@"{0}\{1}\{2}\", strTempUploadPath, userId, ApprovalUtil.AttachFileType.Temp.ToString());

            //    fileInfo = new DTO_ATTACH_FILES();
            //    fileInfo.ATTACH_FILE_TYPE = ApprovalUtil.AttachFileType.Temp.ToString();
            //    fileInfo.COMMENT_IDX = 0;
            //    fileName = fileName;
            //    ofileinfo = new FileInfo(strTempUploadPath + fileName);
            //}



            if (File.Exists(strDestFileName))
            {
                ofileinfo = new FileInfo(strDestFileName);


                if (File.Exists(strDestFileName))
                {

                    string fileName = Path.GetFileName(strDestFileName);

                    // 한글명일 경우 깨지지 않게 하기 위해
                    fileName = HttpUtility.UrlEncode(fileName, new System.Text.UTF8Encoding()).Replace("+", "%20");

                    //this.Response.Clear();
                    //this.Response.ContentType = "application/unknown";
                    //this.Response.HeaderEncoding = System.Text.Encoding.GetEncoding("euc-kr");
                    //this.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                    //this.Response.WriteFile(ofileinfo.FullName);
                    //this.Response.End();

                    // 테스트
                    this.Response.Clear();
                    //this.Response.ContentType = "file/unknown";
                    //this.Response.ContentType = "application/vnd.ms-excel";
                    this.Response.ContentType = "application/octet-stream";

                    //this.Response.HeaderEncoding = System.Text.Encoding.GetEncoding("euc-kr");
                    this.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);

                    this.Response.AppendHeader("Content-Length", ofileinfo.Length.ToString());




                    this.Response.WriteFile(ofileinfo.FullName);
                    this.Response.End();




                    //if (agent.indexOf("MSIE 6.0") > -1 || agent.indexOf("MSIE 5.5") > -1)
                    //{
                    //    response.setHeader("Content-type", "application/octet-stream");
                    //    response.setHeader("Content-Disposition", "attachment; filename="Test.xls"");
                    //    response.setHeader("Content-Transfer-Encoding", "binary");
                    //    response.setHeader("Pragma", "no-cache");
                    //    response.setHeader("Cache-Control", "private");
                    //    response.setHeader("Expires", "0");
                    //}
                    //else
                    //{
                    //    response.setHeader("Content-type", "file/unknown");
                    //    response.setHeader("Content-Disposition", "attachment; filename="Test.xls"");
                    //    response.setHeader("Content-Description", "Servlet Generated Data");
                    //    response.setHeader("Pragma", "no-cache");
                    //    response.setHeader("Cache-Control", "private");
                    //    response.setHeader("Expires", "0");
                    //}




                }
                else
                {
                    Response.Write("첨부파일의 존재하지 않거나 접근 권한이 없습니다.");
                    Response.End();
                }
            } else
            {
                Response.Write("첨부파일의 존재하지 않거나 접근 권한이 없습니다.");
                Response.End();
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
        finally
        {
            if (ofileinfo != null)
            {
                ofileinfo = null;
            }
        }

    }

    public List<DTO_MODULE_PARTICIPANTS> SelectParticipants(string processID)
    {
        try
        {
            using (_ApprovalMgr_Nx mgr = new _ApprovalMgr_Nx())
            {
                if (string.IsNullOrEmpty(processID)) processID = string.Empty;
                return mgr.SelectParticipants(processID);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}