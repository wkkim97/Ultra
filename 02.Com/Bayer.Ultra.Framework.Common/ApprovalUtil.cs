using System;

namespace Bayer.Ultra.Framework.Common
{


    public class ApprovalUtil : IDisposable
    {
        /// <summary>
        /// Display rule of the button by status and person
        /// </summary>
        ///  1. Request
        ///  2. Approval
        ///  3. ForwardApproval
        ///  4. Reject
        ///  5. Forward
        ///  6. Recall
        ///  7. Withdraw
        ///  8. Remind
        ///  9. Exit
        /// 10. Save
        /// 11. InputComment
        /// 12. ReUse
        /// 13. CompleteEvent
        /// 14. CompletePayment
        /// 15. Cancel
        /// 16. Edit
        private static int[][] StatusMetrix = new int[][]
                                            { new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 0 }, // Requester New
                                              new int[] { 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0 }, // Requester On Going
                                              new int[] { 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0 }, // Requester Completed
                                              new int[] { 0, 1, 1, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 }, // Approver On Going
                                              new int[] { 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0 }, // Approver Completed
                                              new int[] { 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0 }, // Recipient Completed
                                              new int[] { 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0 }, // Reviewer Completed
                                              new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 }, // Default
                                              new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 }, // None
                                              new int[] { 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 0 }, // Event Complete
                                              new int[] { 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0 }, // Payment Complete
                                              new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1 }, // Requester Rejected
                                            };
        /// <summary>
        /// 결재 작성시 버튼
        /// </summary>
        public enum ApprovalButtons : int
        {
            Requet = 0,
            Approval,
            ForwardApproval,
            Reject,
            Forward,
            Recall,
            Withdraw,
            Remind,
            Exit,
            Save,
            InputComment,
            ReUse,
            CompleteEvent,
            CompletePayment,
            Cancel,
            Edit
        }

        /// <summary>
        /// 작성화면 상태
        /// </summary>
        public enum ApprovalViewStatus : int
        {
            NEW_REQUESTER = 0,
            ON_GOING_REQUESTER,
            COMPLETED_REQUESTER,
            ON_GOING_APPROVER,
            COMPLETED_APPROVER,
            COMPLETED_RECIPIENT,
            COMPLETED_REVIEWER,
            DEFAULT,
            EXIT,
            EVENT_COMPLETE,
            PAYMENT_COMPLETE,
            REJECTED_REQUESTER
        }

        public static int[] GetApprovalButtonAuthList(ApprovalViewStatus status)
        {
            return StatusMetrix[(int)status];
        }

        /// <summary>
        /// 문서 상태 
        /// </summary>
        public enum ApprovalStatus
        {
            Temp = 0,
            Request = 1,
            Processing = 2,
            Completed = 3,
            Reject = 4,
            Saved = 5,
            Recall = 6,
            Withdraw = 7,
            EventCompleted = 8,
            PaymentCompleted = 9,
            Canceled = 10,
        }

        /// <summary>
        /// 첨부 파일
        /// </summary>
        public enum AttachFileType
        {
            Temp    // 임시저장
            , Common // 일반첨부
            , Quotation // 출장보고서 견적서
            , VisaApplication
            , Comment //Recipient가 Input Comment입력시 첨부
        }

        /// <summary>
        ///  결재자 진행 상태
        /// </summary> 
        public struct ProcessStatus
        {
            public static string DRAFTER = "D";
            public static string CURRENT_APPROVER = "C";
            public static string AWAITER = "W";
            public static string ACEPTER = "A";
            public static string REJECTER = "R";
        }

        /// <summary>
        /// 결재선 타입
        /// </summary>
        public struct ApprovalType
        {
            public static string DRAFTER = "D";
            public static string APPROVER = "A";
            public static string RECIPIENT = "R";
            public static string REVIEWER = "V";
        }

        /// <summary>
        /// 결재자 타입
        /// </summary>
        public struct ApproverType
        {
            public static string DEFAULT = "D";
            public static string INSERT = "I";
            public static string BEFORE = "B";
            public static string AFTER = "A";
            public static string FORWARD = "F";
        }

        /// <summary>
        /// 메일 발송 여부
        /// </summary>
        public struct SentMail
        {
            public static string SEND = "Y";
            public static string NONE = "N";
        }

        /// <summary>
        /// 로그 타입
        /// </summary>
        public enum LogType
        {
            Forward
            , Recall
            , InputComment
            , Withdraw
        }

        /// <summary>
        /// 메일발송 타입
        /// </summary>
        public enum SendMailType
        {
            CurrentApprover
            , FinalApproval
            , InputComment
            , Reject
            , Forward
            , Withdraw
            , Remind
            , Canceled
            , Violation
            , Delegation
            , Interface
        }

        /// <summary>
        /// 메일 발송 Agent 상태
        /// </summary>
        public enum SendMailStatus
        {
            Ready
            ,Complete
            ,Fail
        }

        /// <summary>
        /// 사용자 Role
        /// </summary>
        public struct UserRole
        {
            public static string Admin = "A";
            public static string Special = "S";
            public static string Design = "D";
            public static string None = "N";
        }

        /// <summary>
        /// 메인 화면 뷰타입
        /// </summary>
        public enum MainVIewType
        {
            A,
            B
        }

        /// <summary>
        /// Security group
        /// </summary>
        public struct SECURITY_GROUP
        {
            public static string LPC_USER = "ef.a.kr_localappl_87_lpc_user";
            public static string MEDICAL_ADMIN = "ef.u.kr_localappl_87_medical_admin";
            public static string SUPPORT_USER = "ef.u.kr_localappl_87_support_user";
            public static string SYSTEM_ADMIN = "ef.u.kr_localappl_87_system_admin";
            public static string SYSTEM_DESINGER = "ef.u.kr_localappl_87_system_designer";
            public static string RAD_USER = "bs.u.0695.bkl-ph-rad";
            public static string RAD_KEY_USER = "ef.u.kr_localappl_87_rad_key_user";
            public static string RAD_KEY_USER2 = "ef.a.kr_localappl_87_rad_user";
            public static string NON_ONEKEY_KEY_USER = "ef.u.kr_localappl_87_non_onekey";
            // version 1.0.7 HCP validation function for Easy On
            public static string HCP_SEARCH_KEY_USER = "ef.u.kr_localappl_87_hcp_search_user";
        }

        public void Dispose()
        {
            //자원해제        
            this.Dispose();
        }

        public struct MOHW_TYPE
        {
            /// <summary>
            /// 개별요양기관
            /// </summary>
            public static string DIV_MEDICAL = "DIV_MEDICAL";
            /// <summary>
            /// 복수요양기관
            /// </summary>
            public static string PLURALITY_MEDICAL = "PLURALITY_MEDICAL";
            /// <summary>
            /// 견본품
            /// </summary>
            public static string SAMPLE = "SAMPLE";

            /// <summary>
            /// 구매전 의료기기
            /// </summary>
            public static string SAMPLE_DEVICE = "SAMPLE_DEVICE";

            /// <summary>
            /// 학술대회 지원
            /// </summary>
            public static string PARTICIPANTS = "PARTICIPANTS"; 

            /// <summary>
            /// 시판후 조사
            /// </summary>
            public static string MARKET_RESEARCH = "MARKET_RESEARCH";

            /// <summary>
            /// 임상시험
            /// </summary>
            public static string MEDICAL_STUDY = "MEDICAL_STUDY";

            /// <summary>
            /// KRPIA 강의료
            /// </summary>
            public static string KRPIA = "KRPIA";
        }

        public enum MOHW_STATUS
        {
            Fail,
            Delete,
            Cancel,
            Wait,
            Excel,
            CreateReport,
            Complete 
        }
    }
}
