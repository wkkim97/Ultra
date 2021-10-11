using Bayer.Ultra.Framework.Common.Dto.Approval;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.WcfBase
{
    public partial class UltraEvent : IUltraEvent
    {

        /// <summary>
        /// 결재라인 조회
        /// </summary>
        /// <param name="eventID"></param>
        /// <param name="userID"></param>
        /// <param name="processID"></param>
        /// <returns></returns>
        public List<DTO_APPROVAL_LINE> SelectApprovalLine(string eventID, string userID, string processID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectApprovalLine(eventID, userID, processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 사용자가 입력한 Reviewer 
        /// </summary>
        /// <param name="processID"></param>
        /// <param name="additional"></param>
        public void InsertAdditionalReviewer(List<DTO_PROCESS_APPROVER_ADDTIONAL> additional)
        {
            try
            {
                string processID = additional.Count > 0 ? additional[0].PROCESS_ID : string.Empty;
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    mgr.DeleteProcessApproveAddtional(processID, "T");
                    mgr.InsertProcessApproveAddtional(additional);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 결재 저장
        /// </summary>
        /// <param name="approverList"></param>
        /// <param name="eventProcess"></param>
        /// <param name="eventID"></param>
        /// <param name="processStatus"></param>
        /// <param name="userID"></param>
        public void InsertProcessApprove(RequestApprovalDto requestApproval)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    mgr.InsertProcessApprove(requestApproval.ApproverList, requestApproval.EventProcess, requestApproval.EventID, requestApproval.ProcessStatus, requestApproval.UserID);
                }
                BSL.Common.Mgr.CommonMgr_Tx.InsertSystemLog("Info", string.Format("{0}.{1}", this.GetType().Name, "CreateProcessApproval"), string.Format("Approver Request!( PROCESS_ID : {0} )", requestApproval.EventProcess.PROCESS_ID), requestApproval.UserID);
            }
            catch (Exception ex)
            {
                BSL.Common.Mgr.CommonMgr_Tx.InsertSystemLog("Error", string.Format("{0}.{1}", this.GetType().Name, ex.TargetSite.Name), ex.ToString(), requestApproval.UserID);
                throw ex;
            }
        }

        /// <summary>
        /// 결재 승인/거절
        /// </summary>
        /// <param name="eventID"></param>
        /// <param name="processID"></param>
        /// <param name="comment"></param>
        /// <param name="processStatus"></param>
        /// <param name="userID"></param>
        /// <param name="approverStatus"></param>
        public void UpdateProcessStatus(string eventID, string processID, string comment, string processStatus, string userID, string approverStatus)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    mgr.UpdateProcessStatus(eventID, processID, comment, processStatus, userID, approverStatus);
                }
                BSL.Common.Mgr.CommonMgr_Tx.InsertSystemLog("Info", string.Format("{0}.{1}", this.GetType().Name, "DoApproval"), string.Format("PROCESS_ID : {0},  ApproverStatus : {1}, EventStatus : {2})", processID, approverStatus, processStatus), userID);
            }
            catch (Exception ex)
            {
                BSL.Common.Mgr.CommonMgr_Tx.InsertSystemLog("Error", string.Format("{0}.{1}", this.GetType().Name, ex.TargetSite.Name), ex.ToString(), userID);
                throw ex;
            }
        }

        public void UpdateProcessCompleted(string eventID, string processID, string processStatus, string userID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    mgr.UpdateProcessCompleted(eventID, processID, processStatus, userID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Event Process저장
        /// </summary>
        /// <param name="processEvent"></param>
        public void InsertProcessEvent(DTO_PROCESS_EVENT processEvent)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    mgr.InsertProcessEvent(processEvent);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Event Complete 처리
        /// </summary>
        /// <param name="eventID"></param>
        /// <param name="processID"></param>
        /// <param name="comment"></param>
        /// <param name="processStatus"></param>
        /// <param name="userID"></param>
        public void DoEventComplete(string eventID, string processID, string commentCategory, string comment, string processStatus, string userID, EventAttachFileResponseDto attachFile)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    mgr.DoEventComplete(eventID, processID, commentCategory, comment, processStatus, userID, attachFile);
                }
                BSL.Common.Mgr.CommonMgr_Tx.InsertSystemLog("Info", string.Format("{0}.{1}", this.GetType().Name, "DoEventComplete"), string.Format("PROCESS_ID : {0}", processID), userID);
            }
            catch (Exception ex)
            {
                BSL.Common.Mgr.CommonMgr_Tx.InsertSystemLog("Error", string.Format("{0}.{1}", this.GetType().Name, ex.TargetSite.Name), ex.ToString(), userID);
                throw ex;
            }
        }

        public void DoPaymentComplete(string eventID, string processID, string processStatus, string userID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    mgr.DoPaymentComplete(eventID, processID, processStatus, userID);


                }
                BSL.Common.Mgr.CommonMgr_Tx.InsertSystemLog("Info", string.Format("{0}.{1}", this.GetType().Name, "DoPaymentComplete"), string.Format("PROCESS_ID : {0}", processID), userID);
            }
            catch (Exception ex)
            {
                BSL.Common.Mgr.CommonMgr_Tx.InsertSystemLog("Error", string.Format("{0}.{1}", this.GetType().Name, ex.TargetSite.Name), ex.ToString(), userID);
                throw ex;
            }
        }

        public void UpdateRecall(string eventID, string processID, string comment, string userID, string logType, string processStatus)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    comment = string.Format("[Recall]({0}) : {1}", DateTime.Now.ToString("yyyy-MM-dd"), userID);
                    mgr.UpdateRecall(eventID, processID, comment, userID, logType, processStatus);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void DeleteEventProcess(string processID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    mgr.DeleteEventProcess(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 결재 관련 첨부파일 조회
        /// </summary>
        /// <param name="processID"></param>
        /// <returns></returns>
        public List<DTO_EVENT_ATTACH_FILES> SelectEventAttachFiles(string processID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectEventAttachFiles(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Event IDX로 첨부파일 조회
        /// </summary>
        /// <param name="processID"></param>
        /// <returns></returns>
        public List<DTO_EVENT_ATTACH_FILES> SelectEventAttachFilesIdxs(string processID, string idxs)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectEventAttachFilesIdxs(processID, idxs);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 결재 Forward
        /// </summary>
        /// <param name="receivers"></param>
        /// <param name="comment"></param>
        /// <param name="userID"></param>
        public void InsertForward(List<DTO_PROCESS_APPROVER_COMPLETED> receivers, string comment, string userID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    comment = string.Format("\"{0}\" To \"{1}\"", userID, comment);
                    mgr.InsertForward(receivers, comment, userID, Framework.Common.ApprovalUtil.LogType.Forward.ToString());
                }
                BSL.Common.Mgr.CommonMgr_Tx.InsertSystemLog("Info", string.Format("{0}.{1}", this.GetType().Name, "DoFoward"), string.Format("PROCESS_ID : {0}", receivers[0].PROCESS_ID), userID);
            }
            catch (Exception ex)
            {
                BSL.Common.Mgr.CommonMgr_Tx.InsertSystemLog("Error", string.Format("{0}.{1}", this.GetType().Name, ex.TargetSite.Name), ex.ToString(), userID);
                throw ex;
            }
        }

        /// <summary>
        /// Forward Approval
        /// </summary>
        /// <param name="eventID"></param>
        /// <param name="processID"></param>
        /// <param name="userID"></param>
        /// <param name="approverID"></param>
        public void InsertForwardApproval(string eventID, string processID, string userID, string approverID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    mgr.InsertForwardApproval(eventID, processID, userID, approverID,
                        Framework.Common.ApprovalUtil.ApprovalStatus.Processing.ToString(),
                        Framework.Common.ApprovalUtil.ProcessStatus.ACEPTER);
                }
                BSL.Common.Mgr.CommonMgr_Tx.InsertSystemLog("Info", string.Format("{0}.{1}", this.GetType().Name, "DoFowardApproval"), string.Format("PROCESS_ID : {0}", processID), userID);
            }
            catch (Exception ex)
            {
                BSL.Common.Mgr.CommonMgr_Tx.InsertSystemLog("Error", string.Format("{0}.{1}", this.GetType().Name, ex.TargetSite.Name), ex.ToString(), userID);
                throw ex;
            }
        }

        public void InsertInputComment(string processID, string commentCategory, string comment, string userID, string logType, EventAttachFileResponseDto attachFile, string sendMailApproverId)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                using (BSL.Common.Mgr.CommonMgr_Tx comMgr = new BSL.Common.Mgr.CommonMgr_Tx())
                {
                    mgr.InsertInputComment(processID, userID, logType, commentCategory, comment, attachFile);
                    comMgr.MailSendApprover(processID, Bayer.Ultra.Framework.Common.ApprovalUtil.SendMailType.InputComment.ToString(), sendMailApproverId);
                }

                BSL.Common.Mgr.CommonMgr_Tx.InsertSystemLog("Info", string.Format("{0}.{1}", this.GetType().Name, "DoInputComment"), string.Format("PROCESS_ID : {0}", processID), userID);
            }
            catch (Exception ex)
            {
                BSL.Common.Mgr.CommonMgr_Tx.InsertSystemLog("Error", string.Format("{0}.{1}", this.GetType().Name, "DoInputComment"), string.Format("PROCESS_ID : {0}, Comment : {1}", processID, comment), userID);
                throw ex;
            }
        }

        public void DoWithdraw(string eventID, string processID, string comment, string processStatus, string userID, string approverStatus)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    mgr.DoWithdraw(eventID, processID, comment, processStatus, userID, approverStatus);
                }
                BSL.Common.Mgr.CommonMgr_Tx.InsertSystemLog("Info", string.Format("{0}.{1}", this.GetType().Name, "DoWithdraw"), string.Format("PROCESS_ID : {0}", processID), userID);
            }
            catch (Exception ex)
            {
                BSL.Common.Mgr.CommonMgr_Tx.InsertSystemLog("Error", string.Format("{0}.{1}", this.GetType().Name, "DoWithdraw"), string.Format("PROCESS_ID : {0}, Comment : {1}", processID, comment), userID);
                throw ex;
            }
        }
    }
}
