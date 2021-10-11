using Bayer.Ultra.Framework.Common.Dto.Medical;
using Bayer.Ultra.Framework.Common.Dto.Report;
using Bayer.Ultra.Framework.Common.Dto.Approval;
using Bayer.Ultra.Framework.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Medical.Mgr
{
    public class PmsMgr_Nx : Framework.Database.MgrBase
    {  
        public List<pmslistDto> SelectMedicalPmsList(string userID, string isAdmin)
        {
            try
            {
                using (Dao.PmsDao dao = new Dao.PmsDao())
                {
                    return dao.SelectMedicalPMSList(userID, isAdmin);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public string SelectXlsMedicalPmsList(string userID, string isAdmin)
        {
            List<pmslistDto> source = null;
            string dir = Bayer.Ultra.Framework.Config.WebSiteConfigHandler.WebServer.UploadFile.Temp.PhysicalPath;
            string fullPath = string.Format(@"{0}\PMS_{1}_{2}.xlsx", dir, userID, DateTime.Now.ToString("yyyyMMdd_HHmmss"));

            List<ReportColumnsDto> columns = null;
            try
            {
                using (Dao.PmsDao dao = new Dao.PmsDao())
                {
                    source = dao.SelectMedicalPMSList(userID, isAdmin);

                    columns = new List<ReportColumnsDto>()
                    {
                        new ReportColumnsDto() { Field = "CONTRACT_ID" , Title = "Contract No" },

                        new ReportColumnsDto() { Field = "PRODUCT_NAME" , Title = "Product"  },
                        new ReportColumnsDto() { Field = "HCP_NAME", Title = "HCP"   },
                        new ReportColumnsDto() { Field = "REVIEW_ID", Title = "재심사"   },
                        new ReportColumnsDto() { Field = "COST"  , Title = "단가/건" },
                        new ReportColumnsDto() { Field = "NUMBER" , Title = "건수"   },
                        new ReportColumnsDto() { Field = "AMOUNT", Title = "총비용"   },
                        new ReportColumnsDto() { Field = "REMARK" , Title = "Comment"   },
                        new ReportColumnsDto() { Field = "DATE" , Title = "비용지급일자"   },
                        new ReportColumnsDto() { Field = "EVIDENCE_ID" , Title = "EvidenceNo"   },
                        new ReportColumnsDto() { Field = "CREATOR_NAME" , Title = "Creator"   }

                    };

                    Bayer.Ultra.BSL.Excel.Dao.FileHandler.CreateExcelDocument<pmslistDto>(source, columns, fullPath);


                }
            }
            catch (Exception ex)
            {
                fullPath = "error";
                throw ex;
            }
            return fullPath;
        }



        public pmslistDto SelectMedicalPms(string idx)
        {
            try
            {
                using (Dao.PmsDao dao = new Dao.PmsDao())
                {
                    return dao.SelectMedicalPMS(idx);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
