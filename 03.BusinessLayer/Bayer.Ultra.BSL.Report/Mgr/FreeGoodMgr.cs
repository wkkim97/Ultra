using Bayer.Ultra.Framework.Common.Dto.Approval;
using Bayer.Ultra.Framework.Common.Dto.Report;
using Bayer.Ultra.Framework.Config;
using HiQPdf;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Bayer.Ultra.BSL.Report.Mgr
{
    public class FreeGoodMgr : Framework.Database.MgrBase
    {  
        public List<ReceiptForFreeGoodDto> SelectReceiptList(string userid)
        {
            try
            {
                using (Dao.FreeGoodDao dao = new Dao.FreeGoodDao())
                {
                    return dao.SelectReceiptList(userid);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ExportXlsReceiptList(string userid)
        {
            List<ReceiptForFreeGoodDto> source = null;
            string dir = Bayer.Ultra.Framework.Config.WebSiteConfigHandler.WebServer.UploadFile.Temp.PhysicalPath;
            string fullPath = string.Format(@"{0}\ReceiptForFreeGood_{1}_{2}.xlsx",dir, userid, DateTime.Now.ToString("yyyyMMdd_HHmmss"));

            List<ReportColumnsDto> columns = null; 
            try
            {
                using (Dao.FreeGoodDao dao = new Dao.FreeGoodDao())
                {
                    source = dao.SelectReceiptList(userid);

                    columns = new List<ReportColumnsDto>()
                    {
                        new ReportColumnsDto() { Field = "EVENT_KEY" , Title = "Event Key" },
                        
                        new ReportColumnsDto() { Field = "REQUESTER_NAME" , Title = "Requester"  },
                        new ReportColumnsDto() { Field = "HCP_NAME", Title = "HCP(or Custormer)"   },
                        new ReportColumnsDto() { Field = "HCO_NAME", Title = "HCO(or Organization)"   },
                        new ReportColumnsDto() { Field = "PRODUCT_NAME"  , Title = "Product" },
                        new ReportColumnsDto() { Field = "QTY" , Title = "QTY"   },
                        new ReportColumnsDto() { Field = "PURPOSE", Title = "Purpose"   },
                        new ReportColumnsDto() { Field = "BU" , Title = "BU"   },
                        new ReportColumnsDto() { Field = "PRODUCT_CODE" , Title = "SAP CODE"   },
                        new ReportColumnsDto() { Field = "LAST_APPROVER" , Title = "LAST APPROVER"   },
                        //v 1.0.4 : Request_date/Receipt date 추가
                        new ReportColumnsDto() { Field = "REQUEST_DATE" , Title = "REQUEST_DATE"   },
                        new ReportColumnsDto() { Field = "RECEIPT_DATE" , Title = "RECEIPT_DATE"   },
                        new ReportColumnsDto() { Field = "LOG" , Title = "LOG"   }

                    };

                    Bayer.Ultra.BSL.Excel.Dao.FileHandler.CreateExcelDocument<ReceiptForFreeGoodDto>(source, columns, fullPath);
                }
            }
            catch (Exception ex)
            {
                fullPath = "error";
                throw ex;
            }
            return fullPath;
        }
 

        public string ModifyReceiptFreeGood(DTO_REPORT_RECEIPT_FREE_GOOD dto)
        {
            string retValue = string.Empty, dirPath = string.Empty, fileSignName = string.Empty, filePdfName = string.Empty, urlPath = string.Empty;
            byte[] imageBytes = null;
            try
            {
                #region Report Receipt FreeGood 테이블에 추가
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao.FreeGoodDao dao = new Dao.FreeGoodDao())
                    {
                        if (dto.RECEIPT_TYPE.ToLower().Contains("electronic"))
                        {
                            string base64String = dto.SIGN_IMG_PATH;
                            // Convert Base64 String to byte[]
                            imageBytes = Convert.FromBase64String(base64String);
                            
                            using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                            {
                                
                                // Convert byte[] to Image
                                ms.Write(imageBytes, 0, imageBytes.Length);
                                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                                string storageFolder = Framework.Config.WebSiteConfigHandler.WebServer.UploadFile.Attach.PhysicalPath + @"\" + Bayer.Ultra.Core.Consts.FILES_EVENT_ROOT_DIR + @"\" + dto.PROCESS_ID + @"\" + @"Receipt\";

                                dirPath = WebSiteConfigHandler.WebServer.UploadFile.ElectronicSign.PhysicalPath + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + dto.CREATOR_ID + @"\";
                                urlPath = WebSiteConfigHandler.WebServer.UploadFile.ElectronicSign.DownloadUrl + DateTime.Now.ToString("yyyyMMdd") + @"/" + dto.CREATOR_ID + @"/";
                                // 사인 이미지 파일
                                fileSignName = string.Format("{0}_{1}_{2}.jpg", dto.PROCESS_ID, dto.IDX, dto.EVENT_KEY);

                                // PDF 저장할 파일명 지정
                                filePdfName = string.Format("{0}_{1}_{2}.pdf", "ElectronicDocument", dto.PROCESS_ID, dto.IDX);

                                if (!Directory.Exists(dirPath))
                                {
                                    Directory.CreateDirectory(dirPath);
                                }
                                if (!Directory.Exists(storageFolder))
                                {
                                    Directory.CreateDirectory(storageFolder);
                                }

                                image.Save(dirPath + fileSignName, ImageFormat.Jpeg);

                                dto.FILE_PATH = storageFolder + filePdfName;
                                dto.FILE_URL = storageFolder + filePdfName;

                                dto.SIGN_IMG_PATH = dirPath + fileSignName;
                                dto.SIGN_IMG_URL = urlPath + fileSignName;

                                dto.RECEIPT_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                                dto.RECEIPT_FILENAME = filePdfName;
                            }
                        }

                        dao.ModifyReceiptFreeGood(dto);
                        scope.Complete();

                        retValue = "OK";
                    }
                }
                #endregion
                #region 인수증 추가된 정보를 조회하여 PDF 생성함
                if (retValue == "OK" && dto.RECEIPT_TYPE.ToLower().Contains("electronic"))
                {
                    // 사인이 포함되여 있는 PDF를 해당 경로에 생성함.
                    if(dto.RECEIPT_TYPE== "electronic") { 
                    CreateHtmlToPdf(string.Format("{0}{1}?processid={2}&idx={3}"
                                                , WebSiteConfigHandler.WebServer.UploadFile.ElectronicSign.DownloadUrl
                                                , "ElectronicSignatureReport.aspx"
                                                , dto.PROCESS_ID
                                                , dto.IDX)
                                    , dto.FILE_PATH
                    );
                    }
                    else if (dto.RECEIPT_TYPE == "electronicRMD")
                    {
                        CreateHtmlToPdf(string.Format("{0}{1}?processid={2}&idx={3}"
                                                , WebSiteConfigHandler.WebServer.UploadFile.ElectronicSign.DownloadUrl
                                                , "ElectronicSignatureReport_RMD.aspx"
                                                , dto.PROCESS_ID
                                                , dto.IDX)
                                    , dto.FILE_PATH
                        );
                    }
                    else if (dto.RECEIPT_TYPE == "electronicRMD_Return")
                    {
                        string storageFolder = Framework.Config.WebSiteConfigHandler.WebServer.UploadFile.Attach.PhysicalPath + @"\" + Bayer.Ultra.Core.Consts.FILES_EVENT_ROOT_DIR + @"\" + dto.PROCESS_ID + @"\" + @"Receipt\";
                        filePdfName = string.Format("{0}_{1}_{2}_R.pdf", "ElectronicDocument", dto.PROCESS_ID, dto.IDX);
                        dto.FILE_PATH = storageFolder + filePdfName;
                        CreateHtmlToPdf(string.Format("{0}{1}?processid={2}&idx={3}"
                                                , WebSiteConfigHandler.WebServer.UploadFile.ElectronicSign.DownloadUrl
                                                , "ElectronicSignatureReport_RMD_Return.aspx"
                                                , dto.PROCESS_ID
                                                , dto.IDX)
                                    , dto.FILE_PATH
                        );
                    }
                    int index = 0;
                    using (Bayer.Ultra.BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new Bayer.Ultra.BSL.Approval.Mgr._ApprovalMgr_Tx())
                    {
                        int fileSize = (int)(new FileInfo(dto.FILE_PATH).Length);

                        index = mgr.InsertEventAttachFiles(new DTO_EVENT_ATTACH_FILES()
                        {
                            PROCESS_ID = dto.PROCESS_ID,
                            ATTACH_FILE_TYPE = "ReceiptFreeGood",
                            SEQ = 0,
                            DISPLAY_FILE_NAME = Path.GetFileName(dto.FILE_PATH),
                            SAVED_FILE_NAME = Path.GetFileName(dto.FILE_PATH),
                            FILE_SIZE = fileSize,
                            FILE_PATH = dto.FILE_PATH.Replace(Framework.Config.WebSiteConfigHandler.WebServer.UploadFile.Attach.PhysicalPath, Bayer.Ultra.Core.Consts.FILES_ATTACH_PATH_PREFIX),
                            FILE_HANDLER_URL = Framework.Config.WebSiteConfigHandler.WebServer.UploadHandler.Url + @"?file=" + dto.RECEIPT_FILENAME,
                            REFER_IDX = 0,
                            IS_DELETED = "N",
                            CREATOR_ID = dto.CREATOR_ID,
                        });
                    }
                    // TB_RECEIPT Free Good Table 첨부파일 IDX 업데이트
                    using (Dao.FreeGoodDao dao = new Dao.FreeGoodDao())
                    {
                        dao.UpdateReceiptFreeGoodFileIdx(dto.PROCESS_ID, dto.IDX.ToString(), index.ToString());
                    }
                } 
                #endregion
            }
            catch(Exception ex)
            {
                retValue = ex.Message;
                throw;
            }
            finally
            {
                if(imageBytes != null)
                {
                    imageBytes = null;
                }
            }

            return retValue;
        }

        public string ModifyReceiptFreeGood_return(DTO_REPORT_RECEIPT_FREE_GOOD dto)
        {
            string retValue = string.Empty, dirPath = string.Empty, fileSignName = string.Empty, filePdfName = string.Empty, urlPath = string.Empty;
            byte[] imageBytes = null;
            try
            {
                #region Report Receipt FreeGood 테이블에 추가
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao.FreeGoodDao dao = new Dao.FreeGoodDao())
                    {
                        if (dto.RECEIPT_TYPE.ToLower().Contains("electronic"))
                        {
                            string base64String = dto.SIGN_IMG_PATH;
                            // Convert Base64 String to byte[]
                            imageBytes = Convert.FromBase64String(base64String);

                            using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                            {

                                // Convert byte[] to Image
                                ms.Write(imageBytes, 0, imageBytes.Length);
                                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                                string storageFolder = Framework.Config.WebSiteConfigHandler.WebServer.UploadFile.Attach.PhysicalPath + @"\" + Bayer.Ultra.Core.Consts.FILES_EVENT_ROOT_DIR + @"\" + dto.PROCESS_ID + @"\" + @"Receipt\";

                                dirPath = WebSiteConfigHandler.WebServer.UploadFile.ElectronicSign.PhysicalPath + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + dto.CREATOR_ID + @"\";
                                urlPath = WebSiteConfigHandler.WebServer.UploadFile.ElectronicSign.DownloadUrl + DateTime.Now.ToString("yyyyMMdd") + @"/" + dto.CREATOR_ID + @"/";
                                // 사인 이미지 파일
                                fileSignName = string.Format("{0}_{1}_{2}_R.jpg", dto.PROCESS_ID, dto.IDX, dto.EVENT_KEY);

                                // PDF 저장할 파일명 지정
                                filePdfName = string.Format("{0}_{1}_{2}_R.pdf", "ElectronicDocument", dto.PROCESS_ID, dto.IDX);

                                if (!Directory.Exists(dirPath))
                                {
                                    Directory.CreateDirectory(dirPath);
                                }
                                if (!Directory.Exists(storageFolder))
                                {
                                    Directory.CreateDirectory(storageFolder);
                                }

                                image.Save(dirPath + fileSignName, ImageFormat.Jpeg);

                                dto.FILE_PATH = storageFolder + filePdfName;
                                dto.FILE_URL = storageFolder + filePdfName;

                                dto.SIGN_IMG_PATH = dirPath + fileSignName;
                                dto.SIGN_IMG_URL = urlPath + fileSignName;

                                dto.RECEIPT_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                                dto.RECEIPT_FILENAME = filePdfName;
                            }
                        }

                        dao.ModifyReceiptFreeGood_Return(dto);
                        scope.Complete();

                        retValue = "OK";
                    }
                }
                #endregion
                #region 인수증 추가된 정보를 조회하여 PDF 생성함
                if (retValue == "OK" && dto.RECEIPT_TYPE.ToLower().Contains("electronic"))
                {
                    
                    if (dto.RECEIPT_TYPE == "electronicRMD_Return")
                    {
                        
                        CreateHtmlToPdf(string.Format("{0}{1}?processid={2}&idx={3}"
                                                , WebSiteConfigHandler.WebServer.UploadFile.ElectronicSign.DownloadUrl
                                                , "ElectronicSignatureReport_RMD_Return.aspx"
                                                , dto.PROCESS_ID
                                                , dto.IDX)
                                    , dto.FILE_PATH
                        );
                    }
                    int index = 0;
                    using (Bayer.Ultra.BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new Bayer.Ultra.BSL.Approval.Mgr._ApprovalMgr_Tx())
                    {
                        int fileSize = (int)(new FileInfo(dto.FILE_PATH).Length);

                        index = mgr.InsertEventAttachFiles(new DTO_EVENT_ATTACH_FILES()
                        {
                            PROCESS_ID = dto.PROCESS_ID,
                            ATTACH_FILE_TYPE = "ReceiptFreeGood",
                            SEQ = 0,
                            DISPLAY_FILE_NAME = Path.GetFileName(dto.FILE_PATH),
                            SAVED_FILE_NAME = Path.GetFileName(dto.FILE_PATH),
                            FILE_SIZE = fileSize,
                            FILE_PATH = dto.FILE_PATH.Replace(Framework.Config.WebSiteConfigHandler.WebServer.UploadFile.Attach.PhysicalPath, Bayer.Ultra.Core.Consts.FILES_ATTACH_PATH_PREFIX),
                            FILE_HANDLER_URL = Framework.Config.WebSiteConfigHandler.WebServer.UploadHandler.Url + @"?file=" + dto.RECEIPT_FILENAME,
                            REFER_IDX = 0,
                            IS_DELETED = "N",
                            CREATOR_ID = dto.CREATOR_ID,
                        });
                    }
                    // TB_RECEIPT Free Good Table 첨부파일 IDX 업데이트
                    using (Dao.FreeGoodDao dao = new Dao.FreeGoodDao())
                    {
                        dao.UpdateReceiptFreeGoodFileReturnIdx(dto.PROCESS_ID, dto.IDX.ToString(), index.ToString());
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                retValue = ex.Message;
                throw;
            }
            finally
            {
                if (imageBytes != null)
                {
                    imageBytes = null;
                }
            }

            return retValue;
        }

        public void CreateHtmlToPdf(string url, string createfilePath)
        {
            HtmlToPdf htmlToPdfConverter = new HtmlToPdf();
            // convert HTML to PDF
            byte[] pdfBuffer = null; 
            try
            {
                // set browser width
                htmlToPdfConverter.BrowserWidth = 1200;

                // set browser height if specified, otherwise use the default
                htmlToPdfConverter.BrowserHeight = 1200;

                // set HTML Load timeout
                htmlToPdfConverter.HtmlLoadedTimeout = 120;

                // set PDF page size and orientation
                htmlToPdfConverter.Document.PageSize = PdfPageSize.A4;
                htmlToPdfConverter.Document.PageOrientation = PdfPageOrientation.Landscape; // GetSelectedPageOrientation();

                // set PDF page margins
                htmlToPdfConverter.Document.Margins = new PdfMargins(0);

                // set a wait time before starting the conversion
                htmlToPdfConverter.WaitBeforeConvert = 2;
 
                pdfBuffer = htmlToPdfConverter.ConvertUrlToMemory(url);

                File.WriteAllBytes(createfilePath, pdfBuffer);

            }
            catch(Exception ex)
            {
                //CreateHtmlToPdf(url, createfilePath);
            }
            finally
            {
                if(htmlToPdfConverter != null)
                {
                    htmlToPdfConverter = null;
                }
                if (pdfBuffer != null)
                {
                    pdfBuffer = null;
                }
            } 
        }

        public ReceiptForFreeGoodDto SelectReceiptItem(string processid, string idx)
        {
            try
            {
                using (Dao.FreeGoodDao dao = new Dao.FreeGoodDao())
                {
                    return dao.SelectReceiptItem(processid, idx);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string UpdateReceiptStatus(DTO_REPORT_RECEIPT_FREE_GOOD dto)
        {
            string retValue = string.Empty ;
            try
            {
                using (Dao.FreeGoodDao dao = new Dao.FreeGoodDao())
                {
                    dao.UpdateReceiptStatus(dto.PROCESS_ID, dto.IDX.ToString(), dto.EVENT_KEY  , dto.STATUS, dto.SAP_ORDER, dto.UPDATER_ID);
                }
                retValue = "OK";
            }
            catch (Exception ex)
            {
                retValue = ex.Message;
                throw;
            }
            finally
            {
                
            }

            return retValue;
        }

    }
}
