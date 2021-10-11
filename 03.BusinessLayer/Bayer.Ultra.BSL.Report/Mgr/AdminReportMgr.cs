using Bayer.Ultra.Framework.Common.Dto.Approval;
using Bayer.Ultra.Framework.Common.Dto.Report;
using Bayer.Ultra.Framework.Config;
using HiQPdf;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Bayer.Ultra.BSL.Report.Mgr
{
    public class AdminReportMgr : Framework.Database.MgrBase
    {  
        public List<AdminReportDto> SelectAdminReportList(string code, string from, string to)
        {
            try
            {
                using (Dao.AdminReportDao dao = new Dao.AdminReportDao())
                {
                    return dao.SelectAdminReportList(code,from,to);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string SelectXlsAdminReportList(string code, string from, string to)
        {
            List<AdminReportDto> source = null;
            List<AdminReport_AmountGapDto> source_1 = null;
            List<AdminReport_actionplan> source_2 = null;
            string dir = Bayer.Ultra.Framework.Config.WebSiteConfigHandler.WebServer.UploadFile.Temp.PhysicalPath;
            string fullPath = string.Format(@"{0}\adminreport_{1}_{2}.xlsx",dir, code, DateTime.Now.ToString("yyyyMMdd_HHmmss"));

            List<ReportColumnsDto> columns = null; 
            try
            {
                using (Dao.AdminReportDao dao = new Dao.AdminReportDao())
                {
                    source = dao.SelectXlsAdminReportList(code,from,to);

                    columns = new List<ReportColumnsDto>()
                    {
                        new ReportColumnsDto() { Field = "EVENT_ID" , Title = "EVENT_ID"},
                        new ReportColumnsDto() { Field = "PROCESS_ID" , Title = "PROCESS_ID"},
                        new ReportColumnsDto() { Field = "PROCESS_STATUS" , Title = "PROCESS_STATUS"},
                        new ReportColumnsDto() { Field = "EVENT_KEY" , Title = "EVENT_KEY"},
                        new ReportColumnsDto() { Field = "REQUESTER" , Title = "REQUESTER"},
                        new ReportColumnsDto() { Field = "ORGANIZATION" , Title = "ORGANIZATION"},
                        new ReportColumnsDto() { Field = "EVENT_NAME" , Title = "EVENT_NAME"},
                        new ReportColumnsDto() { Field = "SUBJECT" , Title = "SUBJECT"},
                        new ReportColumnsDto() { Field = "INTERNAL" , Title = "INTERNAL"},
                        new ReportColumnsDto() { Field = "EXTERANL" , Title = "EXTERANL"},
                        new ReportColumnsDto() { Field = "PLAN_TOTAL" , Title = "PLAN_TOTAL"},
                        new ReportColumnsDto() { Field = "ACTUAL_TOTAL" , Title = "ACTUAL_TOTAL"},
                        new ReportColumnsDto() { Field = "AMOUNT_GAP" , Title = "AMOUNT_GAP"},
                        new ReportColumnsDto() { Field = "RATIO" , Title = "RATIO"},
                        new ReportColumnsDto() { Field = "EVENT_SEGMENTATION" , Title = "EVENT_SEGMENTATION"}
                    };

                    Bayer.Ultra.BSL.Excel.Dao.FileHandler.CreateExcelDocument<AdminReportDto>(source, columns, fullPath);
                    code = "AmountGap";
                    source_1 = dao.SelectXlsAdminReport_admoungapList(code, from, to);
                    columns = new List<ReportColumnsDto>()
                    {
                        new ReportColumnsDto() { Field = "EVENT_NAME", Title="EVENT_NAME"},
                        new ReportColumnsDto() { Field = "EVENT_KEY", Title="EVENT_KEY"},
                        new ReportColumnsDto() { Field = "AMOUNT_PLAN", Title="AMOUNT_PLAN"},
                        new ReportColumnsDto() { Field = "AMOUNT_ACTUAL", Title="AMOUNT_ACTUAL"},
                        new ReportColumnsDto() { Field = "AMOUNT_GAP", Title="AMOUNT_GAP"},
                        new ReportColumnsDto() { Field = "RATIO", Title="RATIO"}

                    };
                    Bayer.Ultra.BSL.Excel.Dao.FileHandler.AddExcelDocument<AdminReport_AmountGapDto>(source_1, columns, fullPath);

                    code = "PlanActual";
                    source_2 = dao.SelectXlsAdminReport_actionplanList(code, from, to);
                    columns = new List<ReportColumnsDto>()
                    {
                        new ReportColumnsDto() { Field = "EVENT_ID", Title="EVENT_ID"},
                        new ReportColumnsDto() { Field = "PROCESS_ID", Title="PROCESS_ID"},
                        new ReportColumnsDto() { Field = "EVENT_KEY", Title="EVENT_KEY"},
                        new ReportColumnsDto() { Field = "CATEGORY_NAME", Title="CATEGORY_NAME"},
                        new ReportColumnsDto() { Field = "AMOUNT_PLAN", Title="AMOUNT_PLAN"},
                        new ReportColumnsDto() { Field = "AMOUNT_ACTUAL", Title="AMOUNT_ACTUAL"},
                        new ReportColumnsDto() { Field = "YOUR_DOCS_AMT", Title="YOUR_DOCS_AMT"},
                        new ReportColumnsDto() { Field = "SRM_AMT", Title="SRM_AMT"},
                        new ReportColumnsDto() { Field = "CUNCUR_AMT", Title="CUNCUR_AMT"},
                        new ReportColumnsDto() { Field = "AMOUNT_GAP", Title="AMOUNT_GAP"},
                        new ReportColumnsDto() { Field = "RATIO_NEW", Title="RATIO_NEW"},
                        new ReportColumnsDto() { Field = "COST_WARNING", Title="COST_WARNING"}

                    };
                    Bayer.Ultra.BSL.Excel.Dao.FileHandler.AddExcelDocument<AdminReport_actionplan>(source_2, columns, fullPath);
                }
            }
            catch (Exception ex)
            {
                fullPath = "error";
                throw ex;
            }
            return fullPath;
        }
 

    }
}
