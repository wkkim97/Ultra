using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_PAYMENT_UPLOAD_SRM_HISTORY
    {

        /// <summary>
        /// 프로세스 아이디
        /// </summary> 
        [DataMember]
        public string PROCESS_ID { get; set; }

        /// <summary>
        /// 일련번호
        /// </summary> 
        [DataMember]
        public int SRM_HISTORY_IDX { get; set; }

        /// <summary>
        /// PO Number
        /// </summary>
        [DataMember]
        public string PO_NUMBER { get; set; }

        /// <summary>
        /// 합계 금액
        /// </summary> 
        [DataMember]
        public decimal? AMOUNT { get; set; }

        /// <summary>
        /// 코맨트
        /// </summary> 
        [DataMember]
        public string COMMENT { get; set; }

        /// <summary>
        /// 삭제여부
        /// </summary> 
        [DataMember]
        public string IS_DELETED { get; set; }

        /// <summary>
        /// 파일 표시 명
        /// </summary> 
        [DataMember]
        public string DISPLAY_FILE_NAME { get; set; }

        /// <summary>
        /// 파일 저장 명
        /// </summary> 
        [DataMember]
        public string SAVED_FILE_NAME { get; set; }

        /// <summary>
        /// 파일 경로
        /// </summary> 
        [DataMember]
        public string FILE_PATH { get; set; }

        /// <summary>
        /// Handler Url
        /// </summary> 
        [DataMember]
        public string FILE_HANDLER_URL { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string UPLOADER_ID { get; set; }

        /// <summary>
        ///  업로더
        /// </summary>
        [DataMember]
        public string UPLOADER_NAME { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string UPLOAD_DATE { get; set; }


    }
}
