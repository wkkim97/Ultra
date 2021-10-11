using Bayer.Ultra.Framework.Common;
using Bayer.Ultra.Framework.Common.Dto.Approval;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;

namespace Bayer.Ultra.BSL.Approval.Mgr
{
    public class _ApprovalMgr_Tx : Framework.Database.MgrBase
    {
        #region [ InsertProcessEvent - 결재 이벤트 저장 ]

        public void InsertProcessEvent(DTO_PROCESS_EVENT evt)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    dao.InsertProcessEvent(evt);
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region InsertProcessApproveAddtional [TB_PROCESS_APPROVER_ADDTIONAL 테이블에 Recipient 를 추가]
        /// <summary>
        /// TB_PROCESS_APPROVER_ADDTIONAL 테이블에 Recipient 를 추가
        /// </summary>
        /// <param name="add">추가할 Recipient Item</param>
        public void InsertProcessApproveAddtional(List<DTO_PROCESS_APPROVER_ADDTIONAL> add)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                    {

                        foreach (DTO_PROCESS_APPROVER_ADDTIONAL item in add)
                        {
                            dao.InsertProcessApproveAddtional(item);
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DeleteProcessApproveAddtional
        /// <summary>
        /// 결재선(AdditionalApproer(A)/Recipient(T) List 삭제) 삭제처리
        /// </summary>
        /// <param name="add"></param>
        public void DeleteProcessApproveAddtional(string processID, string approvalTYPE)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                    {
                        dao.DeleteProcessApproveAddtional(processID, approvalTYPE);
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [ InsertProcessApprove - 결재요청 처리 ]
        /// <summary>
        /// 결재요청 처리 실행
        /// </summary>
        /// <param name="apprList">결재자리스트</param>
        /// <param name="docProc">문서테이블</param>
        /// <param name="eventid"></param>
        /// <param name="processstatus">문서상태</param>
        public void InsertProcessApprove(List<DTO_PROCESS_APPROVER> apprList, DTO_PROCESS_EVENT docProc, string eventid, string processstatus, string userid)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                    {
                        dao.DeleteProcessApprove(apprList[0].PROCESS_ID);
                        foreach (DTO_PROCESS_APPROVER item in apprList)
                        {
                            dao.InsertProcessApprove(item);

                        }
                        dao.InsertProcessEvent(docProc);
                        dao.UpdateEventProcessStatus(eventid, apprList[0].PROCESS_ID, processstatus, userid);
                    }
                    scope.Complete();
                }
                SendMail(docProc.PROCESS_ID, Bayer.Ultra.Framework.Common.ApprovalUtil.SendMailType.CurrentApprover.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [ UpdateProcessStatus - Approval, Reject 처리 ]
        /// <summary>
        /// Approval, Reject 처리
        /// </summary>
        /// <param name="eventID">문서ID</param>
        /// <param name="processID">프로세스ID</param>
        /// <param name="comment">커멘트</param>
        /// <param name="processStatus">문서상태</param>
        /// <param name="userID">현결재자ID</param>
        /// <param name="approverStatus">결재자상태</param>
        public void UpdateProcessStatus(string eventID, string processID, string comment, string processStatus, string userID, string approverStatus)
        {
            string finalApprover = string.Empty;
            bool IsComplete = false;
            bool sendMail = true;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                    {
                        dao.UpdateProcessEventStatus(processID, userID, processStatus);
                        dao.UpdateEventProcessStatus(eventID, processID, processStatus, userID);

                        if (processStatus.Equals(ApprovalUtil.ApprovalStatus.Saved.ToString()))
                        {
                            //Reject 후 재편집으로 다시 저장되는 경우
                            sendMail = false;
                            dao.DeleteProcessApprove(processID);

                        }
                        else
                        {
                            dao.UpdateProcessApproverStatus(processID, userID, approverStatus, comment);

                            if (approverStatus.Equals(ApprovalUtil.ProcessStatus.ACEPTER))
                            {
                                finalApprover = dao.GetFinalApprovalIDLine(processID);
                                // 현결재자가 최종결재자일 경우 결재문서 상태를 Completed로 변경한다.
                                if (userID.Equals(finalApprover))
                                {
                                    dao.UpdateProcessCompleted(eventID, processID, ApprovalUtil.ApprovalStatus.Completed.ToString(), userID);
                                    IsComplete = true;
                                }
                                else
                                {
                                    IsComplete = false;
                                }
                            }
                            else if (approverStatus.Equals(ApprovalUtil.ProcessStatus.REJECTER))
                            {
                                dao.InsertProcessApproverCompleted(processID);
                                if (processStatus.Equals("Canceled"))
                                    dao.InsertEventLog(processID, userID, "Cancel", comment);
                                else
                                    dao.InsertEventLog(processID, userID, processStatus, comment);
                            }
                        }
                    }
                    scope.Complete();
                }

                if (sendMail)
                {
                    if (processStatus.Equals(Bayer.Ultra.Framework.Common.ApprovalUtil.ApprovalStatus.Canceled.ToString()))
                    {
                        SendMail(processID, Bayer.Ultra.Framework.Common.ApprovalUtil.SendMailType.Canceled.ToString());
                    }
                    else if (processStatus.Equals(Bayer.Ultra.Framework.Common.ApprovalUtil.ApprovalStatus.Reject.ToString()))
                    {
                        SendMail(processID, Bayer.Ultra.Framework.Common.ApprovalUtil.SendMailType.Reject.ToString());
                    }
                    else
                    {
                        if (IsComplete)// 최종 결재되였을 경우
                        {
                            SendMail(processID, Bayer.Ultra.Framework.Common.ApprovalUtil.SendMailType.FinalApproval.ToString());
                        }
                        else
                        {
                            SendMail(processID, Bayer.Ultra.Framework.Common.ApprovalUtil.SendMailType.CurrentApprover.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SendMail(string processId, string sendType)
        {
            try
            {
                using (Bayer.Ultra.BSL.Common.Mgr.CommonMgr_Tx mgr = new Bayer.Ultra.BSL.Common.Mgr.CommonMgr_Tx())
                {
                    mgr.MailSend(processId, sendType, Bayer.Ultra.Framework.Config.WebSiteConfigHandler.SmtpManager.Sender);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 결재 없는 이벤트 완료처리
        /// </summary>
        /// <param name="eventID"></param>
        /// <param name="processID"></param>
        /// <param name="comment"></param>
        /// <param name="processStatus"></param>
        /// <param name="userID"></param>
        /// <param name="approverStatus"></param>
        public void UpdateProcessCompleted(string eventID, string processID, string processStatus, string userID)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                    {
                        dao.UpdateProcessEventCompleted(processID, userID, processStatus);
                        dao.UpdateEventProcessStatus(eventID, processID, processStatus, userID);
                        dao.UpdateProcessCompleted(eventID, processID, ApprovalUtil.ApprovalStatus.Completed.ToString(), userID);
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [ InsertForwardApproval - ForwardApproval 처리 ]
        /// <summary>
        /// ForwardApproval 처리
        /// </summary>
        /// <param name="eventID">문서ID</param>
        /// <param name="processID">프로세스ID</param>
        /// <param name="currentUserID">현결재자ID</param>
        /// <param name="forwardApprovalID">Forward Approval Target UserID</param>
        /// <param name="processStatus">문서상태</param>
        /// <param name="approverStatus">결재자상태</param>
        public void InsertForwardApproval(string eventID, string processID, string currentUserID, string forwardApprovalID, string processStatus, string approverStatus)
        {
            string finalApprover = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                    {
                        dao.InsertForwardApprovalAddUser(processID, currentUserID, forwardApprovalID);
                        dao.UpdateProcessEventStatus(processID, currentUserID, processStatus);
                        dao.UpdateEventProcessStatus(eventID, processID, processStatus, currentUserID);
                        dao.UpdateProcessApproverStatus(processID, currentUserID, approverStatus, "Forward Approval");
                        finalApprover = dao.GetFinalApprovalIDLine(processID);

                        // 현결재자가 최종결재자와 동일ID일 경우 최종결재자를 forwardApprover로 변경한다.
                        if (currentUserID.Equals(finalApprover))
                        {
                            dao.ChangeProcessEventFinalApprover(processID, forwardApprovalID);
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [ InsertForward - Forward 처리]
        /// <summary>
        /// Forward처리
        /// </summary>
        /// <param name="reviewers">참조자</param>
        /// <param name="comment">코멘트</param>
        /// <param name="userID">등록자</param>
        /// <param name="logType">로그타입</param>
        public void InsertForward(List<DTO_PROCESS_APPROVER_COMPLETED> reviewers, string comment, string userID, string logType)
        {
            string finalApprover = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                    {
                        if (reviewers.Count > 0)
                        {
                            dao.InsertEventLog(reviewers[0].PROCESS_ID, userID, logType, comment);
                        }
                        foreach (DTO_PROCESS_APPROVER_COMPLETED item in reviewers)
                        {
                            dao.InsertProcessApproveCompletedAddReviewer(item);
                        }
                    }
                    scope.Complete();
                }
                SendMail(reviewers[0].PROCESS_ID, Bayer.Ultra.Framework.Common.ApprovalUtil.SendMailType.Forward.ToString());
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region [ InsertInputComment - InputComment 처리 ]

        public void InsertInputComment(string processID, string registerID, string logType, string commentCategory, string comment, EventAttachFileResponseDto attachFile)
        {
            try
            {
                int logID = 0;
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                    {
                        logID = dao.InsertEventLog(processID, registerID, logType, comment, commentCategory);
                        if (attachFile != null) //첨부파일이 존재하면
                        {
                            string filePath = HttpUtility.UrlDecode(attachFile.FilePath);
                            string tempFolder = Framework.Config.WebSiteConfigHandler.WebServer.UploadFile.Temp.PhysicalPath;
                            string tempFilePath = filePath.Replace(Core.Consts.FILES_TEMP_PATH_PREFIX, tempFolder);
                            string storageFolder = Framework.Config.WebSiteConfigHandler.WebServer.UploadFile.Attach.PhysicalPath;
                            string storageFilePath = filePath.Replace(Core.Consts.FILES_TEMP_PATH_PREFIX, storageFolder);
                            string storageFileFolder = System.IO.Path.GetDirectoryName(storageFilePath);
                            if (!System.IO.Directory.Exists(storageFileFolder))
                            {
                                System.IO.Directory.CreateDirectory(storageFileFolder);
                            }

                            if (System.IO.File.Exists(storageFilePath))
                            {
                                storageFilePath = string.Format(@"{0}\{1}_{2:yyyyMMddHHmmss.fff}{3}", storageFileFolder, System.IO.Path.GetFileNameWithoutExtension(storageFilePath), DateTime.Now, System.IO.Path.GetExtension(storageFilePath));
                            }

                            System.IO.File.Move(tempFilePath, storageFilePath);

                            dao.InsertEventAttachFiles(
                                new DTO_EVENT_ATTACH_FILES()
                                {
                                    PROCESS_ID = processID,
                                    ATTACH_FILE_TYPE = attachFile.AttachType,
                                    SEQ = 0,
                                    DISPLAY_FILE_NAME = HttpUtility.UrlDecode(attachFile.DisplayName),
                                    SAVED_FILE_NAME = System.IO.Path.GetFileName(storageFilePath),
                                    FILE_SIZE = (int)attachFile.FileSize,
                                    FILE_PATH = filePath.Replace(Core.Consts.FILES_TEMP_PATH_PREFIX, Core.Consts.FILES_ATTACH_PATH_PREFIX),
                                    FILE_HANDLER_URL = attachFile.FileHandlerUrl,
                                    REFER_IDX = logID,
                                    IS_DELETED = "N",
                                    CREATOR_ID = registerID,
                                });
                        }
                    }


                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region [ DoEventComplete - Event Complete 처리 ]

        public void DoEventComplete(string eventID, string processID, string commentCategory, string comment, string processStatus, string userID, EventAttachFileResponseDto attachFile)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                    {
                        dao.UpdateProcessEventStatus(processID, userID, processStatus);
                        dao.UpdateEventProcessStatus(eventID, processID, processStatus, userID);
                        if (comment.Length > 0)
                            dao.InsertEventLog(processID, userID, processStatus, comment, commentCategory);

                        if (attachFile != null) //첨부파일이 존재하면
                        {
                            string filePath = HttpUtility.UrlDecode(attachFile.FilePath);
                            string tempFolder = Framework.Config.WebSiteConfigHandler.WebServer.UploadFile.Temp.PhysicalPath;
                            string tempFilePath = filePath.Replace(Core.Consts.FILES_TEMP_PATH_PREFIX, tempFolder);
                            string storageFolder = Framework.Config.WebSiteConfigHandler.WebServer.UploadFile.Attach.PhysicalPath;
                            string storageFilePath = filePath.Replace(Core.Consts.FILES_TEMP_PATH_PREFIX, storageFolder);
                            string storageFileFolder = System.IO.Path.GetDirectoryName(storageFilePath);
                            if (!System.IO.Directory.Exists(storageFileFolder))
                            {
                                System.IO.Directory.CreateDirectory(storageFileFolder);
                            }

                            if (System.IO.File.Exists(storageFilePath))
                            {
                                storageFilePath = string.Format(@"{0}\{1}_{2:yyyyMMddHHmmss.fff}{3}", storageFileFolder, System.IO.Path.GetFileNameWithoutExtension(storageFilePath), DateTime.Now, System.IO.Path.GetExtension(storageFilePath));
                            }

                            System.IO.File.Move(tempFilePath, storageFilePath);

                            dao.InsertEventAttachFiles(
                                new DTO_EVENT_ATTACH_FILES()
                                {
                                    PROCESS_ID = processID,
                                    ATTACH_FILE_TYPE = attachFile.AttachType,
                                    SEQ = 0,
                                    DISPLAY_FILE_NAME = HttpUtility.UrlDecode(attachFile.DisplayName),
                                    SAVED_FILE_NAME = System.IO.Path.GetFileName(storageFilePath),
                                    FILE_SIZE = (int)attachFile.FileSize,
                                    FILE_PATH = filePath.Replace(Core.Consts.FILES_TEMP_PATH_PREFIX, Core.Consts.FILES_ATTACH_PATH_PREFIX),
                                    FILE_HANDLER_URL = attachFile.FileHandlerUrl,
                                    REFER_IDX = 0,
                                    IS_DELETED = "N",
                                    CREATOR_ID = userID,
                                });
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [ DoPaymentComplete - Payment Complete 처리 ]
        public void DoPaymentComplete(string eventID, string processID, string processStatus, string userID)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                    {
                        dao.UpdateProcessEventStatus(processID, userID, processStatus);
                        dao.UpdateEventProcessStatus(eventID, processID, processStatus, userID);
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [ InsertWithdraw -  WithDraw 처리 ]
        public void DoWithdraw(string eventID, string processID, string comment, string processStatus, string userID, string approverStatus)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                    {
                        dao.UpdateProcessEventStatus(processID, userID, processStatus);
                        dao.UpdateEventProcessStatus(eventID, processID, processStatus, userID);
                        dao.UpdateProcessApproverStatus(processID, userID, approverStatus, comment);
                        dao.InsertEventLog(processID, userID, processStatus, comment);
                    }
                    scope.Complete();
                }
                SendMail(processID, Bayer.Ultra.Framework.Common.ApprovalUtil.SendMailType.Withdraw.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [ UpdateRecall ]
        public void UpdateRecall(string eventID, string processID, string comment, string userID, string logType, string processStatus)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                    {

                        dao.UpdateProcessEventStatus(processID, userID, processStatus);
                        dao.UpdateEventProcessStatus(eventID, processID, processStatus, userID);
                        dao.DeleteProcessApprove(processID);

                        dao.InsertEventLog(processID, userID, logType, comment);

                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [ DeleteEventProcess ]
        public void DeleteEventProcess(string processID)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    dao.DeleteEventProcess(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        #endregion

        #region [ InsertEventAttachFiles - 이벤트 첨부파일 저장 ]

        public int InsertEventAttachFiles(DTO_EVENT_ATTACH_FILES file)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.InsertEventAttachFiles(file);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region [ DeleteEventAttachFiles - 이벤트 첨부파일 삭제 ] 

        public void DeleteEventAttachFiles(int idx, string updaterID)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    dao.DeleteEventAttachFiles(idx, updaterID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region [ Cost Plan ]

        public string MergeCostPlan(DTO_MODULE_COST_PLAN dto)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.MergeCostPlan(dto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MergeCostPlanList(List<DTO_MODULE_COST_PLAN> list)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                    {
                        foreach (DTO_MODULE_COST_PLAN dto in list)
                        {
                            dao.MergeCostPlan(dto);
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteCostPlan(string processID, int costPlanIDX, string updaterID)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    dao.DeleteCostPlan(processID, costPlanIDX, updaterID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region [ Praticipants ]

        public string InsertParticipant(List<DTO_MODULE_PARTICIPANTS> participants)
        {
            try
            {
                //참석자 먼저 입력하는 경우
                string processID = participants.Count > 0 ? participants[0].PROCESS_ID : string.Empty;
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                    {
                        for (int i = 0; i < participants.Count; i++)
                        {
                            if (i == 0 && string.IsNullOrEmpty(processID))
                                processID = dao.InsertParticipant(participants[i]);
                            dao.InsertParticipant(participants[i]);
                        }
                    }
                    scope.Complete();
                }

                return processID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateParticipantStatus(string processID, int[] indexes, string isAttended, string updaterID)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                    {
                        foreach (int idx in indexes)
                        {
                            dao.UpdateParticipantStatus(processID, idx, isAttended, updaterID);
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // version 1.0.5 Admin Event Page for change by DM Team
        public void UpdateChangeValue(string PROCESS_ID, string CATEGORY, string ADJUSTMENT_AREA, string OLD_VALUE, string NEW_VALUE, string REASON, string UPDATER_ID)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                    {
                        dao.UpdateChangeValue(PROCESS_ID, CATEGORY, ADJUSTMENT_AREA, OLD_VALUE, NEW_VALUE, REASON, UPDATER_ID);
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteParticipant(string processID, int[] indexes, string updaterID)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                    {
                        foreach (int idx in indexes)
                        {
                            dao.DeleteParticipant(processID, idx, updaterID);
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region [ Agenda ]

        public string MergeAgenda(DTO_MODULE_AGENDA agenda)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.MergeAgenda(agenda);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteAgenda(string processID, int agendaIDX, string updaterID)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    dao.DeleteAgenda(processID, agendaIDX, updaterID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int MergeAgendaRole(DTO_MODULE_AGENDA_ROLE agendaRole)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.MergeAgendaRole(agendaRole);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteAgendaRole(string processID, int agendaIDX, int agendaRoleIDX, string updaterID)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    dao.DeleteAgendaRole(processID, agendaIDX, agendaRoleIDX, updaterID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateMaterialcode(string processID, string agendaIDX, string mcode,string updateID, string costcenter, string sap_no, string krpia)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    dao.UpdateMaterialcode(processID, agendaIDX, mcode, updateID, costcenter, sap_no, krpia);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region [ Delegation ]

        public void MergeDelegation(List<DTO_MODULE_DELEGATION> delegation)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                    {
                        foreach (DTO_MODULE_DELEGATION del in delegation)
                        {
                            dao.MergeDelegation(del);
                        }
                    }
                    scope.Complete();
                }
                SendMail(delegation.FirstOrDefault().PROCESS_ID, Bayer.Ultra.Framework.Common.ApprovalUtil.SendMailType.Delegation.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteDelegation(string processID, string[] userIDs, string updaterID)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                    {
                        foreach (string userID in userIDs)
                        {
                            dao.DeleteDelegation(processID, userID, updaterID);
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region [ Payment ]

        public void InsertEventPaymentUploadSRMHistory(string processID, string userID, DTO_PAYMENT_UPLOAD_SRM_HISTORY history)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                    {
                        dao.UpdatePaymentUploadSRM(processID, history.PO_NUMBER, userID);
                        string filePath = HttpUtility.UrlDecode(history.FILE_PATH);
                        string tempFolder = Framework.Config.WebSiteConfigHandler.WebServer.UploadFile.Temp.PhysicalPath;
                        string tempFilePath = filePath.Replace(Core.Consts.FILES_TEMP_PATH_PREFIX, tempFolder);
                        string storageFolder = Framework.Config.WebSiteConfigHandler.WebServer.UploadFile.Attach.PhysicalPath;
                        string storageFilePath = filePath.Replace(Core.Consts.FILES_TEMP_PATH_PREFIX, storageFolder);
                        string storageFileFolder = System.IO.Path.GetDirectoryName(storageFilePath);
                        if (!System.IO.Directory.Exists(storageFileFolder))
                        {
                            System.IO.Directory.CreateDirectory(storageFileFolder);
                        }

                        if (System.IO.File.Exists(storageFilePath))
                        {
                            storageFilePath = string.Format(@"{0}\{1}_{2:yyyyMMddHHmmss.fff}{3}", storageFileFolder, System.IO.Path.GetFileNameWithoutExtension(storageFilePath), DateTime.Now, System.IO.Path.GetExtension(storageFilePath));
                        }

                        System.IO.File.Move(tempFilePath, storageFilePath);
                        history.DISPLAY_FILE_NAME = HttpUtility.UrlDecode(history.DISPLAY_FILE_NAME);
                        history.SAVED_FILE_NAME = System.IO.Path.GetFileName(storageFilePath);
                        history.FILE_PATH = storageFilePath.Replace(storageFolder, Core.Consts.FILES_ATTACH_PATH_PREFIX);

                        dao.InsertEventPaymentUploadSRMHistory(history);
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_PAYMENT_UPLOAD_SRM> InsertEventPaymentUploadSRM(string processID, string userID, List<DTO_PAYMENT_UPLOAD_SRM> srms)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                    {
                        dao.DeleteEventPaymentUploadSRM(processID, userID);
                        foreach (DTO_PAYMENT_UPLOAD_SRM srm in srms)
                        {
                            dao.InsertEventPaymentUploadSRM(srm);
                        }
                    }
                    scope.Complete();
                }
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectEventPaymentUploadSRM(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteEventPaymentUploadSRMHistory(string processID, string poNumber, string updaterID)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    dao.DeleteEventPaymentUploadSRMHistory(processID, poNumber, updaterID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MergeEventPaymentInputSRM(DTO_PAYMENT_INPUT_SRM srm)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    dao.MergeEventPaymentInputSRM(srm);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteEventPaymentInputSRM(string processID, int srmIDX, string updaterID)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    dao.DeleteEventPaymentInputSRM(processID, srmIDX, updaterID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertEventPaymentUploadConcur(string userID, List<DTO_PAYMENT_UPLOAD_CONCUR> concurs)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                    {
                        dao.DeleteEventPaymentUploadConcur(userID);
                        foreach (DTO_PAYMENT_UPLOAD_CONCUR concur in concurs)
                        {
                            dao.InsertEventPaymentUploadConcur(concur);
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void InsertEventPaymentUploadYourDoces(string userID, List<DTO_PAYMENT_UPLOAD_YOURDOCES> yourdoces)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                    {
                        dao.DeleteEventPaymentUploadYourDoces(userID);
                        foreach (DTO_PAYMENT_UPLOAD_YOURDOCES yourdoc in yourdoces)
                        {
                            dao.InsertEventPaymentUploadYourDoces(yourdoc);
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DTO_PAYMENT_UPLOAD_CONCUR> ReadEventPaymentUploadConcur(string userID)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectEventPaymentUploadConcur(userID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertEventModulePaymentFromConcur(string creatorID)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    dao.InsertEventModulePaymentFromConcur(creatorID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertEventModulePaymentFromYourDoces(string creatorID)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    dao.InsertEventModulePaymentFromYourDoces(creatorID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdatePaymentConcur(DTO_MODULE_PAYMENT_CONCUR concur)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    dao.UpdatePaymentConcur(concur);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MergeICCMaster(DTO_PAYMENT_ICC_MASTER iccinfo, List<EventAttachFileResponseDto> attachFiles)
        {
            int iccID = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                    {
                        iccID = dao.MergeICCMaster(iccinfo);
                    }

                    if (attachFiles != null && attachFiles.Count > 0) //첨부파일이 존재하면
                    {
                        foreach(EventAttachFileResponseDto attachFile in attachFiles)
                        {
                            string filePath = HttpUtility.UrlDecode(attachFile.FilePath);

                            //파일경로가 임시 폴더 가 아닐 경우 upload 제외 (이미 첨부한 첨부파일임)
                            if (filePath.IndexOf(Core.Consts.FILES_TEMP_PATH_PREFIX) < 0) continue;

                            string tempFolder = Framework.Config.WebSiteConfigHandler.WebServer.UploadFile.Temp.PhysicalPath;
                            string tempFilePath = filePath.Replace(Core.Consts.FILES_TEMP_PATH_PREFIX, tempFolder);

                            //change userid -> iccid
                            filePath = filePath.Replace(string.Format(@"\{0}", iccinfo.CREATOR_ID), string.Format(@"\{0}", iccID.ToString()));

                            string storageFolder = Framework.Config.WebSiteConfigHandler.WebServer.UploadFile.Attach.PhysicalPath;
                            string storageFilePath = filePath.Replace(Core.Consts.FILES_TEMP_PATH_PREFIX, storageFolder);
                            string storageFileFolder = System.IO.Path.GetDirectoryName(storageFilePath);
                            if (!System.IO.Directory.Exists(storageFileFolder))
                            {
                                System.IO.Directory.CreateDirectory(storageFileFolder);
                            }

                            if (System.IO.File.Exists(storageFilePath))
                            {
                                storageFilePath = string.Format(@"{0}\{1}_{2:yyyyMMddHHmmss.fff}{3}", storageFileFolder, System.IO.Path.GetFileNameWithoutExtension(storageFilePath), DateTime.Now, System.IO.Path.GetExtension(storageFilePath));
                            }

                            System.IO.File.Move(tempFilePath, storageFilePath);

                            using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                            {
                                dao.InsertICCAttachFiles(new DTO_PAYMENT_ICC_ATTACH_FILES()
                                {
                                    ICC_ID = iccID,
                                    ATTACH_FILE_TYPE = attachFile.AttachType,
                                    SEQ = 0,
                                    DISPLAY_FILE_NAME = HttpUtility.UrlDecode(attachFile.DisplayName),
                                    SAVED_FILE_NAME = System.IO.Path.GetFileName(storageFilePath),
                                    FILE_SIZE = (int)attachFile.FileSize,
                                    FILE_PATH = filePath.Replace(Core.Consts.FILES_TEMP_PATH_PREFIX, Core.Consts.FILES_ATTACH_PATH_PREFIX),
                                    FILE_HANDLER_URL = attachFile.FileHandlerUrl,
                                    IS_DELETED = "N",
                                    CREATOR_ID = iccinfo.CREATOR_ID,
                                });
                            }
                        }
                    }


                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void DeleteICCAttachFiles(int idx, string updaterID)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    dao.DeleteICCAttachFiles(idx, updaterID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
