using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Medical
{
    public class MedicalInfoDto
    {
        /// <summary>
        /// 메디컬 일련번호
        /// </summary> 
        public int MEDICAL_IDX { get; set; }

        /// <summary>
        /// 카테고리
        /// </summary> 
        public string CATEGORY { get; set; }

        /// <summary>
        /// 구분
        /// </summary> 
        public string TYPE { get; set; }

        /// <summary>
        /// 상태
        /// </summary> 
        public string STATUS { get; set; }

        /// <summary>
        /// 팀
        /// </summary> 
        public string TEAM { get; set; }

        /// <summary>
        /// Impact  번호
        /// </summary> 
        public string IMPACT_NO { get; set; }

        /// <summary>
        /// 제목
        /// </summary> 
        public string TITLE { get; set; }

        /// <summary>
        /// 물질번호
        /// </summary> 
        public string METERIAL_NO { get; set; }

        /// <summary>
        /// 승인번호
        /// </summary> 
        public string APPROVAL_NO { get; set; }
          
        /// <summary>
        /// 승인일자
        /// </summary>  
        public string APPROVAL_DATE { get; set; }

        /// <summary>
        /// 금액
        /// </summary> 
        public string COST_INFORMATION { get; set; }

        /// <summary>
        /// 작성자
        /// </summary> 
        public string AUTHOR { get; set; }

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
        public string CREATE_DATE { get; set; }

        /// <summary>
        /// 수정자
        /// </summary> 
        public string UPDATER_ID { get; set; }

        /// <summary>
        /// 수정자 이름
        /// </summary> 
        public string MODIFIER { get; set; }

        /// <summary>
        /// 수정일자
        /// </summary> 
        public string UPDATE_DATE { get; set; }



    }
}
