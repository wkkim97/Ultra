using Bayer.Ultra.Framework.Common.Dto.Common;
using Bayer.Ultra.Framework.Common.Dto.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.WcfBase
{
    public partial class UltraReport : IUltraReport
    {
        public List<AdminReportDto> ExportReportAdmin(string code, string from, string to)
        {
            try
            {
                using (BSL.Report.Mgr.AdminReportMgr mgr = new BSL.Report.Mgr.AdminReportMgr())
                {
                    return mgr.SelectAdminReportList(code, from, to);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ExportXlsReportAdmin(string code, string from, string to)
        {
            try
            {
                using (BSL.Report.Mgr.AdminReportMgr mgr = new BSL.Report.Mgr.AdminReportMgr())
                {
                    return mgr.SelectXlsAdminReportList(code, from, to);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<ReceiptForFreeGoodDto> SelectReceiptForFreeGoodList(string userid)
        {
            try
            {
                using (BSL.Report.Mgr.FreeGoodMgr mgr = new BSL.Report.Mgr.FreeGoodMgr())
                {
                    return mgr.SelectReceiptList(userid);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ExportXlsReceiptForFreeGoodList(string userid)
        {
            try
            {
                using (BSL.Report.Mgr.FreeGoodMgr mgr = new BSL.Report.Mgr.FreeGoodMgr())
                {
                    return mgr.ExportXlsReceiptList(userid);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ModifyReceiptFreeGood(DTO_REPORT_RECEIPT_FREE_GOOD dto)
        {
            try
            {
                using (BSL.Report.Mgr.FreeGoodMgr mgr = new BSL.Report.Mgr.FreeGoodMgr())
                {
                    return mgr.ModifyReceiptFreeGood(dto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ModifyReceiptFreeGood_return(DTO_REPORT_RECEIPT_FREE_GOOD dto)
        {
            try
            {
                using (BSL.Report.Mgr.FreeGoodMgr mgr = new BSL.Report.Mgr.FreeGoodMgr())
                {
                    return mgr.ModifyReceiptFreeGood_return(dto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReceiptForFreeGoodDto SelectReceiptForFreeGoodItem(string processid, string idx)
        {
            try
            {
                using (BSL.Report.Mgr.FreeGoodMgr mgr = new BSL.Report.Mgr.FreeGoodMgr())
                {
                    return mgr.SelectReceiptItem(processid, idx);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string UpdateReceiptStatus(DTO_REPORT_RECEIPT_FREE_GOOD dto)
        {
            try
            {
                using (BSL.Report.Mgr.FreeGoodMgr mgr = new BSL.Report.Mgr.FreeGoodMgr())
                {
                    return mgr.UpdateReceiptStatus(dto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_MOHW_MARKET_RESEARCH> SelectMarketResearchSourceList(MohwConditionDto condi)
        {
            try
            {
                using (BSL.Report.Mgr.MohwReportMgr mgr = new BSL.Report.Mgr.MohwReportMgr())
                {
                    return mgr.SelectMarketResearchSourceList(condi);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_MOHW_PARTICIPANTS> SelectParticipantsSourceList(MohwConditionDto condi)
        {
            try
            {
                using (BSL.Report.Mgr.MohwReportMgr mgr = new BSL.Report.Mgr.MohwReportMgr())
                {
                    return mgr.SelectParticipantsSourceList(condi);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string InsertMohwReport(DTO_MOHW_CONDITIONS condi)
        {
            try
            {
                using (BSL.Report.Mgr.MohwReportMgr mgr = new BSL.Report.Mgr.MohwReportMgr())
                {
                    return mgr.InsertMohwReport(condi);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CreateXlsMohwReport(string idx, string mohwType, string userId)
        {
            try
            {
                using (BSL.Report.Mgr.MohwReportMgr mgr = new BSL.Report.Mgr.MohwReportMgr())
                {
                    mgr.CreateExcelReport(idx, mohwType, userId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MohwConditionListDto> SelectMohwList(string subject, string mohwType, string startDate, string endDate)
        {
            try
            {
                if (string.IsNullOrEmpty(mohwType)) mohwType = string.Empty;
                if (string.IsNullOrEmpty(subject)) subject = string.Empty;
                if (string.IsNullOrEmpty(startDate)) startDate = DateTime.Now.AddYears(-5).ToString("yyyy-MM-dd");
                if (string.IsNullOrEmpty(endDate)) endDate = DateTime.Now.AddYears(5).ToString("yyyy-MM-dd");

                using (BSL.Report.Mgr.MohwReportMgr mgr = new BSL.Report.Mgr.MohwReportMgr())
                {
                    return mgr.SelectMohwList(subject, mohwType, startDate, endDate);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_MOHW_PLURALITY_MEDICAL> SelectPluralityMedicalSourceList(MohwConditionDto condi)
        {
            try
            {
                using (BSL.Report.Mgr.MohwReportMgr mgr = new BSL.Report.Mgr.MohwReportMgr())
                {
                    return mgr.SelectPluralityMedicalSourceList(condi);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_MOHW_DIV_MEDICAL_SRC> SelectIndividualMedicalSourceList(MohwConditionDto condition)
        {
            try
            {
                using (BSL.Report.Mgr.MohwReportMgr mgr = new BSL.Report.Mgr.MohwReportMgr())
                {
                    return mgr.SelectIndividualMedicalSourceList(condition);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string UpdateMohwStatus(string idx, string mohwType, string status, string userId)
        {
            try
            {
                using (BSL.Report.Mgr.MohwReportMgr mgr = new BSL.Report.Mgr.MohwReportMgr())
                {
                    return mgr.UpdateMohwStatus(idx, mohwType, status, userId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CreateMohwCompleteReport(string idx, string mohwType, string userId)
        {
            try
            {
                using (BSL.Report.Mgr.MohwReportMgr mgr = new BSL.Report.Mgr.MohwReportMgr())
                {
                    mgr.CreateMOHWReport(idx, mohwType, userId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_MOHW_FREE_GOODS> SelectSampleSourceList(MohwConditionDto condition)
        {
            try
            {
                using (BSL.Report.Mgr.MohwReportMgr mgr = new BSL.Report.Mgr.MohwReportMgr())
                {
                    return mgr.SelectSampleSourceList(condition);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_MOHW_FREE_GOODS_DEVICE> SelectSampleDeviceSourceList(MohwConditionDto condition)
        {
            try
            {
                using (BSL.Report.Mgr.MohwReportMgr mgr = new BSL.Report.Mgr.MohwReportMgr())
                {
                    return mgr.SelectSampleDeviceSourceList(condition);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_MOHW_MEDICAL_STUDY> SelectMedicalStudySourceList(MohwConditionDto condition)
        {
            try
            {
                using (BSL.Report.Mgr.MohwReportMgr mgr = new BSL.Report.Mgr.MohwReportMgr())
                {
                    return mgr.SelectMedicalStudySourceList(condition);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_MOHW_KRPIA> SelectKRPIASourceList(MohwConditionDto condition)
        {
            try
            {
                using (BSL.Report.Mgr.MohwReportMgr mgr = new BSL.Report.Mgr.MohwReportMgr())
                {
                    return mgr.SelectKRPIASourceList(condition);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void trasfersFTP_Concur(string yyyymm)
        {
            try
            {
                using (BSL.Report.Mgr.Concur_sftpMgr mgr = new BSL.Report.Mgr.Concur_sftpMgr())
                {
                    mgr.trasfersFTP_Concur(yyyymm);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





    }
}
