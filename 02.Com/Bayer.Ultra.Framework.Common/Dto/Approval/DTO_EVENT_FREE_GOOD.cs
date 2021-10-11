using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    public class DTO_EVENT_FREE_GOOD
    {
        /// <summary>
        /// 프로세스 아이디
        /// </summary> 
        public string PROCESS_ID { get; set; }

        /// <summary>
        /// 제목
        /// </summary> 
        public string SUBJECT { get; set; }

        /// <summary>
        /// 이벤트 키
        /// </summary> 
        public string EVENT_KEY { get; set; }

        /// <summary>
        /// 프로세스 상태
        /// </summary> 
        public string PROCESS_STATUS { get; set; }

        /// <summary>
        /// 기안자 아이디
        /// </summary> 
        public string REQUESTER_ID { get; set; }

        /// <summary>
        /// 기안일자
        /// </summary> 
        public string REQUEST_DATE { get; set; }

        /// <summary>
        /// 회사코드
        /// </summary> 
        public string COMPANY_CODE { get; set; }

        /// <summary>
        /// 조직명
        /// </summary> 
        public string ORGANIZATION_NAME { get; set; }

        /// <summary>
        /// 보존기한
        /// </summary> 
        public string LIFE_CYCLE { get; set; }

        /// <summary>
        /// Business Unit
        /// </summary> 
        public string BU { get; set; }

        /// <summary>
        /// 목적
        /// </summary> 
        public string PURPOSE { get; set; }

        /// <summary>
        /// 지역
        /// </summary> 
        public string LOCATION { get; set; }

        /// <summary>
        /// Impact No
        /// </summary> 
        public string IMPACT_NO { get; set; }

        /// <summary>
        /// Po No
        /// </summary> 
        public string PO_NO { get; set; }

        /// <summary>
        /// 사용여부
        /// </summary> 
        public string IS_DISUSED { get; set; }

        public string COMMENT { get; set; }
        // <!-- Ver 1.0.7 : Go-Direct -->
        public string DELIVER_DATE { get; set; }
        public string RETURN_DATE { get; set; }
        // <!-- Ver 1.0.7 : Go-Direct -->

        /// <summary>
        /// 생성자
        /// </summary> 
        public string CREATOR_ID { get; set; }

        /// <summary>
        /// 생성일자
        /// </summary> 
        public DateTime? CREATE_DATE { get; set; }

        /// <summary>
        /// 수정자
        /// </summary> 
        public string UPDATER_ID { get; set; }

        /// <summary>
        /// 수정일자
        /// </summary> 
        public DateTime? UPDATE_DATE { get; set; }


    }
}
