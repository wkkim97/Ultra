using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Radiology
{
    public  class AssignedHospitalListDto
    {
        public string ORGANIZATION_ID { get; set; }
        public string ONEKEY_INS_CODE { get; set; }
        public string INS_NAME { get; set; }
        public string TYPE { get; set; }
        public string SUB_TYPE { get; set; }
        public string B_STATE { get; set; }
        public string B_DISTRICT { get; set; }
        public string USER_ID { get; set; }
        public string FULL_NAME { get; set; }
        public string ORG_ACRONYM { get; set; }
        public string ORG_NAME { get; set; }
        public string SUPERVISOR { get; set; }
    }
    public class HospitalEquipmentDto
    {
        public int ID { get; set; }
        public string USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string ORGANIZATION_ID { get; set; }
        public string ORGANIZATION_NAME { get; set; }
        public string CATEGORY { get; set; }
        public string SCANNER_MODEL_TYPE { get; set; }
        public string MANUFACTURER { get; set; }
        public string STATUS { get; set; }
        public string INJECTOR_MANUFACTURER { get; set; }
        public string EQUIPMENT_TYPE { get; set; }
        public string DATE_OF_INSTALLATION { get; set; }
        public int AMOUNT { get; set; }
        public string MEMO { get; set; }
        public string CREATOR_ID { get; set; }
    }
    public class HospitalExaminationDto
    {
        public int ID { get; set; }
        public string QUARTER { get; set; }
        public string USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string ORGANIZATION_ID { get; set; }
        public string ORGANIZATION_NAME { get; set; }
        public string EXAMINATION_TYPE { get; set; }
        public string SCAN_TYPE { get; set; }
        public string NUMBER_OF_CASES { get; set; }
        public string NUMBER_OF_ENHANCED_CASES { get; set; }
        public string CASE_RELATION { get; set; }
        public string COPY_TO { get; set; }
        public string COMMENT { get; set; }
        public string CREATOR_ID { get; set; }

    }
    public class MergeHospitalExaminationDto
    {
        public int ID { get; set; }
        public string CURRENT_QUARTER { get; set; }
        public string NEW_QUATER { get; set; }
        public string USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string ORGANIZATION_ID { get; set; }
        public string ORGANIZATION_NAME { get; set; }
        public string EXAMINATION_TYPE { get; set; }
        public string SCAN_TYPE { get; set; }
        public string NUMBER_OF_CASES { get; set; }
        public string NUMBER_OF_ENHANCED_CASES { get; set; }
        public string CASE_RELATION { get; set; }
        public string COMMENT { get; set; }
        public string CREATOR_ID { get; set; }

    }
    public class HospitalMarketShareDto
    {
        public int ID { get; set; }
        public string QUARTER { get; set; }
        public string USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string ORGANIZATION_ID { get; set; }
        public string ORGANIZATION_NAME { get; set; }
        public string SEGMENT { get; set; }
        public string SUB_CATEGORY { get; set; }
        public string MANUFACTURER { get; set; }
        public string PRODUCT_CODE { get; set; }
        public string PRODUCT_FAMAILY { get; set; }
        public string PRODUCT { get; set; }
        public int QUANTITY { get; set; }
        public decimal PRICE { get; set; }
        public decimal SALES { get; set; }
        public string COMMENT { get; set; }
        public string COPY_TO { get; set; }
        public string CREATOR_ID { get; set; }
    }
    public class MergeHospitalMarketShareDto
    {
        public int ID { get; set; }
        public string CURRENT_QUARTER { get; set; }
        public string USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string ORGANIZATION_ID { get; set; }
        public string ORGANIZATION_NAME { get; set; }
        public string SEGMENT { get; set; }
        public string SUB_CATEGORY { get; set; }
        public string MANUFACTURER { get; set; }
        public string PRODUCT_CODE { get; set; }
        public string PRODUCT_FAMAILY { get; set; }
        public string PRODUCT { get; set; }
        public int QUANTITY { get; set; }
        public decimal PRICE { get; set; }
        public decimal SALES { get; set; }
        public string COMMENT { get; set; }
        public string NEW_QUATER { get; set; }
        public string CREATOR_ID { get; set; }
    }

    //Micromarketing function
    public class MasterMarketShare
    {
        public int ID { get; set; }
        public string MANUFACTURE { get; set; }
        public string SEGMENT { get; set; }
        public string PRODUCT_FAMILY { get; set; }
        public string PRODUCT { get; set; }
        public decimal PRICE { get; set; }
        public string CREATOR_ID { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public string UPDATER_ID { get; set; }
        public DateTime UPDATE_DATE { get; set; }


    }

}
