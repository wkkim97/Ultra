using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Radiology
{
    public class HCPInquiryCustomerDto
    {
        public string HCPCode { get; set; }
        public string HCPName { get; set; }
        public string OrganizationCode { get; set; }
        public string OrganizationName { get; set; }
        public string SpecialtyCode { get; set; }
        public string SpecialtyName { get; set; }
    }
    public class HCPInquiryMergeDto
    {
        public int? HCP_INQUIRY_REQUEST_ID { get; set; }
        public string REQUESTER_ID { get; set; }
        public string REQUEST_TYPE { get; set; }
        public string CUSTOMER_ID { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public string ORGANIZATION_ID { get; set; }
        public string ORGANIZATION_NAME { get; set; }
        public string SPECIALTY_ID { get; set; }
        public string SPECIALTY_NAME { get; set; }
        public string YEAR_FROM { get; set; }
        public string YEAR_TO { get; set; }
        public string INQUIRY_STATUS { get; set; }
        public string REMARK { get; set; }
        public string DELIVERED_TO_HCP { get; set; }
        public string CREATOR_ID { get; set; }
    }
    public class HCPInquiryListDto
    {
        public int HCP_INQUIRY_REQUEST_ID { get; set; }
        public string REQUESTER_ID { get; set; }
        public string FULL_NAME { get; set; }
        public string MAIL_ADDRESS { get; set; }
        public string ORG_ACRONYM { get; set; }
        public string REQUEST_TYPE { get; set; }
        public string CUSTOMER_ID { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public string ORGANIZATION_ID { get; set; }
        public string ORGANIZATION_NAME { get; set; }
        public string SPECIALTY_ID { get; set; }
        public string SPECIALTY_NAME { get; set; }
        public string YEAR_FROM { get; set; }
        public string YEAR_TO { get; set; }
        public string REMARK { get; set; }
        public string DELIVERED_TO_HCP { get; set; }
        public string INQUIRY_STATUS { get; set; }
        public string CREATOR_ID { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public DateTime UPDATE_DATE { get; set; }
    }
    public class InsertHCPInquiryLogDto
    {
        public List<HCPInquiryIdDto> IDs { get; set; }
        public string REGISTER_ID { get; set; }
        public string LOG_TYPE { get; set; }
        public string LOG_CATEGORY { get; set; }
        public string COMMENT { get; set; }
    }
    public class HCPInquiryIdDto
    {
        public int HCP_INQUIRY_REQUEST_ID { get; set; }
        public string MAIL_ADDRESS { get; set; }
    }
    public class SelectHCPInquiryLogDto
    {
        public int IDX { get; set; }
        public int HCP_INQUIRY_REQUEST_ID { get; set; }
        public string LOG_TYPE { get; set; }
        public string LOG_CATEGORY { get; set; }
        public string COMMENT { get; set; }
        public DateTime CREATE_DATE { get; set; }
    }
}
