using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Report
{
    public class MohwConditionDto
    {
 
        public string MOHW_TYPE { get; set;}  
        public string START_DATE { get; set;}
        public string END_DATE   { get; set;}
        public string IS_FOOD_COST_YN   { get; set;}
        public string BELOW_ONE_WON_YN  { get; set;}
        public string EXCEPT_IECTURER_FOOD_COST_YN  { get; set;}
        public string ONLY_ATTEND_YN    { get; set;}
        public string ONLY_MEDICINE_YN  { get; set;}
        public string ONLY_MEDICAL_EQUIPMENT_YN { get; set;}
        public string EXCEPT_BAYER_EMPLOYEEE_YN { get; set;}
        public string CONFIRM_YN { get; set; }
 
    }

    public class MohwConditionListDto
    {
        public int IDX { get; set; }
        public string SUBJECT { get; set; }
        public string MOHW_TYPE { get; set; }
        public string MOHW_TYPE_N { get; set; }
        public string START_DATE { get; set; }
        public string END_DATE { get; set; }
        public string IS_FOOD_COST_YN { get; set; }
        public string BELOW_ONE_WON_YN { get; set; }
        public string EXCEPT_IECTURER_FOOD_COST_YN { get; set; }
        public string ONLY_ATTEND_YN { get; set; }
        public string ONLY_MEDICINE_YN { get; set; }
        public string ONLY_MEDICAL_EQUIPMENT_YN { get; set; }
        public string EXCEPT_BAYER_EMPLOYEEE_YN { get; set; }
        public string CONFIRM_YN { get; set; }
        public string FILE_PATH {get; set;}
        public string FILE_STATUS { get; set; }
        public string MOHW_PATH { get; set; }
        public string MOHW_STATUS { get; set; }

        public string STATUS { get; set; }

        public string STATUS_NAME { get; set; }
        
        public string CREATOR_ID { get; set; }
        public string CREATE_DATE { get; set; }

    }
}
