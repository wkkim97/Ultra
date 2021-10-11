using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Radiology
{
    public class AssignedNonOneKeyListDto
    {
        public int NON_ONEKEY_ID { get; set; }
        public string REQUESTER_ID { get; set; }
        public string MAIL_ADDRESS { get; set; }
        public string REQUEST_TYPE { get; set; }
        public string CUSTOMER_TYPE { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public string GENDER { get; set; }
        public string ORGANIZATION_NAME { get; set; }
        public string ORGANIZATION_ID { get; set; }
        public string NON_ONEKEY_STATUS { get; set; }
        public string REMARK { get; set; }
        public string FULL_NAME { get; set; }
        public string IS_DELETED { get; set; }
        public string USER_ID { get; set; }
        public string ORG_ACRONYM { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public DateTime UPDATE_DATE { get; set; }
    }

    public class CustomerListDto
    {
        public string GUBUN { get; set; }
        public string HCPName { get; set; }
        public string SpecialtyName { get; set; }
        public string OrganizationName { get; set; }
        public string INS_ONEKEY_CODE { get; set; }
        public string DOC_ONEKEY_CODE { get; set; }
        public string NON_ONEKEY_STATUS { get; set; }
        public string GENDER { get; set; }
        public string HCP_TYPE { get; set; }
    }
    public class DeleteAttachDto
    {
        public int IDX { get; set; }
        public string UPDATER_ID { get; set; }
    }
    public class NonOnekeyAttachDto
    {
        public int IDX { get; set; }
        public int NON_ONEKEY_ID { get; set; }
        public string ATTACH_FILE_TYPE { get; set; }
        public int SEQ { get; set; }
        public int REFER_IDX { get; set; }
        public string DISPLAY_FILE_NAME { get; set; }
        public string SAVED_FILE_NAME { get; set; }
        public int FILE_SIZE { get; set; }
        public string FILE_PATH { get; set; }
        public string FILE_HANDLER_URL { get; set; }
        public string IS_DELETED { get; set; }
        public string CREATOR_ID { get; set; }

    }
    public class MergeAttachDto
    {
        public List<NonOnekeyAttachDto> INSERT_ATTACH { get; set; }
        public List<DeleteAttachDto> DELETE_ATTACH { get; set; }
    }
    public class MergeCustomerDto
    {
        public int NON_ONEKEY_ID { get; set; }
        public string REQUESTER_ID { get; set; }
        public string REQUEST_TYPE { get; set; }
        public string CUSTOMER_TYPE { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public string GENDER { get; set; }
        public string ORGANIZATION_ID { get; set; }
        public string DOC_ONEKEY_CODE { get; set; }
        public string ORGANIZATION_NAME { get; set; }
        public string NON_ONEKEY_STATUS { get; set; }
        public string REMARK { get; set; }
        public string CREATOR_ID { get; set; }
    }
    public class HospitalListDto
    {
        public string OrganizationName { get; set; }
        public string INS_ONEKEY_CODE { get; set; }
        public string B_DISTRICT { get; set; }
        public string B_STATE { get; set; }
    }
    public class InsertLogDto
    {
        public List<NonOnekeyIdDto> NON_ONEKEY_ID { get; set; }
        public string REGISTER_ID { get; set; }
        public string LOG_TYPE { get; set; }
        public string LOG_CATEGORY { get; set; }
        public string COMMENT { get; set; }
    }
    public class NonOnekeyIdDto
    {
        public int NON_ONEKEY_ID { get; set; }
    }
    public class SelectLogDto
    {
        public int IDX { get; set; }
        public int NON_ONEKEY_ID { get; set; }
        public string LOG_TYPE { get; set; }
        public string LOG_CATEGORY { get; set; }
        public string COMMENT { get; set; }
        public DateTime CREATE_DATE { get; set; }
    }
    
}
