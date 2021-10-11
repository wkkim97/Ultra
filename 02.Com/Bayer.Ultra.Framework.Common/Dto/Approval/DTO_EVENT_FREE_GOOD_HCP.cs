using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    public class DTO_EVENT_FREE_GOOD_HCP
    {
        /// <summary>
        /// 프로세스 아이디
        /// </summary> 
        public string PROCESS_ID { get; set; }

        /// <summary>
        /// 일련번호
        /// </summary> 
        public int? IDX { get; set; }
         
        /// <summary>
        /// 의사 Code
        /// </summary> 
        public string HCP_CODE { get; set; }

        /// <summary>
        /// 구분
        /// </summary> 
        public string TYPE { get; set; }

        /// <summary>
        /// 전문부분
        /// </summary>
        public string SPECIALITY { get; set; }

        /// <summary>
        /// 의사명
        /// </summary> 
        public string HCP_NAME { get; set; }

        /// <summary>
        /// 병원코드
        /// </summary> 
        public string HCO_CODE { get; set; }

        /// <summary>
        /// 병원
        /// </summary> 
        public string HCO_NAME { get; set; }
         
        /// <summary>
        /// 제품코드
        /// </summary> 
        public string PRODUCT_CODE { get; set; }

        /// <summary>
        /// 제품명
        /// </summary> 
        public string PRODUCT_NAME { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string SAMPLE_CODE { get; set; }

        /// <summary>
        /// 수량
        /// </summary> 
        public int QTY { get; set; }

         //<!-- Ver 1.0.7 : Go-Direct -->
        public string IS_DEVICE { get; set; }

        public DateTime? RETURN_DATE { get; set; }
         //<!-- Ver 1.0.7 : Go-Direct -->
        /// <summary>
        /// 삭제여부
        /// </summary> 
        public string IS_DELETED { get; set; }

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
        /// 수정일
        /// </summary> 
        public DateTime? UPDATE_DATE { get; set; }



    }

    public class EventFreeGoodHcpListDto
    {
        /// <summary>
        /// 프로세스 아이디
        /// </summary> 
        public string PROCESS_ID { get; set; }

        /// <summary>
        /// 일련번호
        /// </summary> 
        public int? IDX { get; set; }

        /// <summary>
        /// 의사 Code
        /// </summary> 
        public string HCP_CODE { get; set; }

        /// <summary>
        /// 구분
        /// </summary> 
        public string TYPE { get; set; }

        /// <summary>
        /// 전문부분
        /// </summary>
        public string SPECIALITY { get; set; }

        /// <summary>
        /// 의사명
        /// </summary> 
        public string HCP_NAME { get; set; }

        /// <summary>
        /// 병원코드
        /// </summary> 
        public string HCO_CODE { get; set; }

        /// <summary>
        /// 병원
        /// </summary> 
        public string HCO_NAME { get; set; }

        /// <summary>
        /// 제품코드
        /// </summary> 
        public string PRODUCT_CODE { get; set; }

        /// <summary>
        /// 제품명
        /// </summary> 
        public string PRODUCT_NAME { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string SAMPLE_CODE { get; set; }

        /// <summary>
        /// 수량
        /// </summary> 
        public int QTY { get; set; }

        /// <summary>
        /// 삭제여부
        /// </summary> 
        public string IS_DELETED { get; set; }

        public string RECEIPT_DATE { get; set; }

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
        /// 수정일
        /// </summary> 
        public DateTime? UPDATE_DATE { get; set; }



    }
}
