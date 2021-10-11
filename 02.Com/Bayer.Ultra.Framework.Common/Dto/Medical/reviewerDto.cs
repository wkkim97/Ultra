using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Medical
{
    public class reviewerDto
    {
        /// <summary>
        /// 메디컬 일련번호
        /// </summary> 
        public int MEDICAL_IDX { get; set; }

        /// <summary>
        /// Editor 아이디
        /// </summary> 
        public string REVIEWER_ID { get; set; }

        /// <summary>
        /// Editor
        /// </summary> 
        public string NAME { get; set; }


        /// <summary>
        /// ORG_ACRONYM
        /// </summary> 
        public string ORG_ACRONYM { get; set; }
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
        public DateTime CREATE_DATE { get; set; }

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
