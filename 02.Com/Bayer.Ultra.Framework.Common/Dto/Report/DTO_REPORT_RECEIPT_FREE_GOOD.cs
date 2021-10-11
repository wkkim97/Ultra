using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Report
{
    public class DTO_REPORT_RECEIPT_FREE_GOOD
    {
        /// <summary>
        /// 프로세스 ID
        /// </summary> 
        public string PROCESS_ID { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public int IDX { get; set; }

        /// <summary>
        /// 이벤트키
        /// </summary> 
        public string EVENT_KEY { get; set; }

        /// <summary>
        /// 영수증구
        /// </summary> 
        public string RECEIPT_TYPE { get; set; }

        /// <summary>
        /// 영수증발행
        /// </summary> 
        public string RECEIPT_DATE { get; set; }

        /// <summary>
        /// 영수증
        /// </summary> 
        public string RECEIPT_FILENAME { get; set; }

        public string EVENT_FILE_IDX { get; set; }

        /// <summary>
        /// 파일경
        /// </summary> 
        public string FILE_PATH { get; set; }

        /// <summary>
        /// 파일경
        /// </summary> 
        public string FILE_URL { get; set; }

        /// <summary>
        /// 서명 경로
        /// </summary> 
        public string SIGN_IMG_PATH { get; set; }

        /// <summary>
        /// 서명 경로
        /// </summary> 
        public string SIGN_IMG_URL { get; set; }

        /// <summary>
        /// 반품요청
        /// </summary> 
        public DateTime? RETURN_DATE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string SAP_ORDER { get; set; }

        /// <summary>
        /// 발행상태
        /// </summary> 
        public string STATUS { get; set; }

        /// <summary>
        /// 생성자ID
        /// </summary> 
        public string CREATOR_ID { get; set; }

        /// <summary>
        /// 생성일
        /// </summary> 
        public DateTime? CREATE_DATE { get; set; }

        /// <summary>
        /// 수정자ID
        /// </summary> 
        public string UPDATER_ID { get; set; }

        /// <summary>
        /// 수정일
        /// </summary> 
        public DateTime? UPDATE_DATE { get; set; }

        public string RETURN_COMMENT { get; set; }


    }
}
