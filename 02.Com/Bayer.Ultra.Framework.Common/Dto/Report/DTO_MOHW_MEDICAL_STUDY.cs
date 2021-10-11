using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Report
{
    public class DTO_MOHW_MEDICAL_STUDY
    {
 
        /// <summary>
        /// 
        /// </summary> 
        public string INDEX { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string CONTRACT_NO { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string AUTHOR { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string AUTHOR_ORG { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string CATEGORY { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string STATUS { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string TITLE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string TYPE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string APPROVAL_NO { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string APPROVAL_DATE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string HEAD_HCP { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string HEAD_HCO { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string OTHER_HCP { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string OTHER_HCP_CODE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public int PAYMENT_COST { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string PAYMENT_EVIDENCE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string PAYMENT_CREATOR { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string FREE_GOODS { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string FREE_GOODS_HCP_CODE { get; set; }

        public string CONTRACT_DATE { get; set; }
        

    }

    public class DTO_MOHW_MEDICAL_STUDY_REPORT
    {
        /// <summary>
        /// 
        /// </summary> 
        public string INDEX { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string TITLE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string TYPE { get; set; }
         

        /// <summary>
        /// 
        /// </summary> 
        public string APPROVAL_NO { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string APPROVAL_DATE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string HEAD_HCP { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string HEAD_HCO { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string OTHER_HCP { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string OTHER_HCP_CODE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public int PAYMENT_COST { get; set; }
         
        /// <summary>
        /// 
        /// </summary> 
        public string FREE_GOODS { get; set; }


        //임사시험지원에 최초계약일 표시
        /// <summary>
        /// 
        /// </summary> 
        public string FREE_GOODS_HCP_CODE { get; set; }
        /// <summary>
        /// 
        /// </summary> 
        public string CONTRACT_DATE { get; set; }

    }
}
