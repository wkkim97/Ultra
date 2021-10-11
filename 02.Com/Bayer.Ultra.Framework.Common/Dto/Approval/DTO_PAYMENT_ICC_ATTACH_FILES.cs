using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_PAYMENT_ICC_ATTACH_FILES
    {
        /// <summary>
        /// 첨부파일 일련번호
        /// </summary> 
        [DataMember]
        public int IDX { get; set; }

        /// <summary>
        /// ICC 아이디
        /// </summary> 
        [DataMember]
        public int ICC_ID { get; set; }

        /// <summary>
        /// 첨부파일 타입
        /// </summary> 
        [DataMember]
        public string ATTACH_FILE_TYPE { get; set; }

        /// <summary>
        /// 순번
        /// </summary> 
        [DataMember]
        public int SEQ { get; set; }

        /// <summary>
        /// Comment  일련번호
        /// </summary> 
        [DataMember]
        public int REFER_IDX { get; set; }

        /// <summary>
        /// 파일 명
        /// </summary> 
        [DataMember]
        public string DISPLAY_FILE_NAME { get; set; }

        /// <summary>
        /// 정장된 파일명
        /// </summary> 
        [DataMember]
        public string SAVED_FILE_NAME { get; set; }

        /// <summary>
        /// 파일 사이즈
        /// </summary> 
        [DataMember]
        public int FILE_SIZE { get; set; }

        /// <summary>
        /// 파일 경로
        /// </summary> 
        [DataMember]
        public string FILE_PATH { get; set; }

        /// <summary>
        /// 다운로드 URL
        /// </summary> 
        [DataMember]
        public string FILE_HANDLER_URL { get; set; }

        /// <summary>
        /// 프로세스 상태
        /// </summary>
        [DataMember]
        public string PROCESS_STATUS { get; set; }

        /// <summary>
        /// 삭제여부
        /// </summary> 
        [DataMember]
        public string IS_DELETED { get; set; }

        /// <summary>
        /// 생성자
        /// </summary> 
        [DataMember]
        public string CREATOR_ID { get; set; }

        /// <summary>
        /// 생성일자
        /// </summary> 
        [DataMember]
        public DateTime? CREATE_DATE { get; set; }

        /// <summary>
        /// 수정자
        /// </summary> 
        [DataMember]
        public string UPDATER_ID { get; set; }

        /// <summary>
        /// 수정일
        /// </summary> 
        [DataMember]
        public DateTime? UPDATE_DATE { get; set; }
    }
}
