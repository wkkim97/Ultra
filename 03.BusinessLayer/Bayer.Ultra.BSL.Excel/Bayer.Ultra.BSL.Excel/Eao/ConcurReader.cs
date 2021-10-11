using Bayer.Ultra.Framework.Common.Dto.Approval;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Excel.Eao
{
    public class ConcurReader : BaseReader
    {
        
        public List<DTO_PAYMENT_UPLOAD_CONCUR> ReadPaymentConcur(string userID, string filePath)
        {
            List<DTO_PAYMENT_UPLOAD_CONCUR> list = null;
            if (!File.Exists(filePath)) return list;

            try
            {
                list = new List<DTO_PAYMENT_UPLOAD_CONCUR>();
                string user_id = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fs, false))
                    {
                        WorkbookPart workbookPart = doc.WorkbookPart;
                        IEnumerable<Sheet> sheets = doc.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                        string relationshipId = sheets.First().Id.Value;
                        WorksheetPart worksheetPart = (WorksheetPart)doc.WorkbookPart.GetPartById(relationshipId);
                        Worksheet workSheet = worksheetPart.Worksheet;
                        SheetData sheetData = workSheet.GetFirstChild<SheetData>();
                        IEnumerable<Row> rows = sheetData.Descendants<Row>();

                        string poNumber = string.Empty;
                        string poNumberGS = string.Empty;
                        string value = string.Empty;
                        string category = string.Empty;
                        bool isStart = false;
                        foreach (Row row in rows) //this will also include your header row...
                        {
                            DTO_PAYMENT_UPLOAD_CONCUR uploadConcur = null;
                            if (row.Descendants<Cell>().Count() < 1) continue;
                            string startKey = GetCellValue(doc, row.Descendants<Cell>().ElementAt(0));
                            if (startKey.Equals("Company Code"))
                            {
                                isStart = true;
                                continue;
                            }
                            if (!isStart) continue;

                            string errorMsg = string.Empty;

                            string companyCode = GetCellValue(doc, GetCell(row, "A"));
                            if (string.IsNullOrEmpty(companyCode)) continue;
                            string reportID = GetCellValue(doc, GetCell(row, "B"));
                            string reportName = GetCellValue(doc, GetCell(row, "C"));
                            string employeeID = GetCellValue(doc, GetCell(row, "D"));
                            string employee = GetCellValue(doc, GetCell(row, "E"));
                            string transactionID = GetCellValue(doc, GetCell(row, "F"));
                            string transactionDate = GetCellValue(doc, GetCell(row, "G"));
                            double tmp = ConvertToDouble(transactionDate);
                            DateTime? dtTransactionDate = null;
                            if (tmp > 0)
                                dtTransactionDate = DateTime.FromOADate(tmp);
                            if (dtTransactionDate == null) errorMsg += "Transaction Date";
                            string expenseType = GetCellValue(doc, GetCell(row, "H"));
                            string hcpExpenseType = GetCellValue(doc, GetCell(row, "I"));
                            string materialCode = GetCellValue(doc, GetCell(row, "J"));
                            string consultationNumber = GetCellValue(doc, GetCell(row, "K"));
                            string hcpTypeIdentification = GetCellValue(doc, GetCell(row, "L"));
                            string businessPurpose = GetCellValue(doc, GetCell(row, "M"));
                            string expenseAmount = GetCellValue(doc, GetCell(row, "N"));
                            decimal dcExpenseAmount = ConvertToDecimal(expenseAmount);
                            string reimbursementCurrency = GetCellValue(doc, GetCell(row, "O"));
                            string localCode = GetCellValue(doc, GetCell(row, "P"));
                            string hcpCode = string.Empty;
                            string hcoCode = string.Empty;
                            string crmEventKey = string.Empty;
                            string[] splited = localCode.Split(new char[] { '|' });
                            if (splited.Length > 0)
                            {
                                hcpCode = splited[0];
                                if (splited.Length > 1) hcoCode = splited[1];
                                if (splited.Length > 2) crmEventKey = splited[2];
                            }
                            string attendeeAmount = GetCellValue(doc, GetCell(row, "Q"));
                            decimal dcAttendeeAmount = ConvertToDecimal(attendeeAmount);
                            //version 1.0-.5 -금액도 포함 2019-01-03 by WooKyung Kim
                            //if (dcAttendeeAmount < 0)
                            //{
                            //    if (errorMsg.Length > 0) errorMsg += "|";
                            //    errorMsg += "ATTENDEE_AMOUNT";
                            //}
                            string eventKey = GetCellValue(doc, GetCell(row, "R"));

                            string attendeeName = GetCellValue(doc, GetCell(row, "S"));
                            string attendeeFirstName = GetCellValue(doc, GetCell(row, "T"));
                            string attendeeLastName = GetCellValue(doc, GetCell(row, "U"));
                            string title = GetCellValue(doc, GetCell(row, "V"));
                            string company = GetCellValue(doc, GetCell(row, "W"));
                            string affiliation = GetCellValue(doc, GetCell(row, "X"));
                            string attendeeType = GetCellValue(doc, GetCell(row, "Y"));
                            string externalID = GetCellValue(doc, GetCell(row, "Z"));
                            string vender = GetCellValue(doc, GetCell(row, "AA"));
                            string location = GetCellValue(doc, GetCell(row, "AB"));
                            string region = GetCellValue(doc, GetCell(row, "AC"));
                            string country = GetCellValue(doc, GetCell(row, "AD"));
                            string allocationCode = GetCellValue(doc, GetCell(row, "AE"));
                            string allocationNumber = GetCellValue(doc, GetCell(row, "AF"));
                            string allocationAmount = GetCellValue(doc, GetCell(row, "AG"));
                            decimal dcAllocationAmount = ConvertToDecimal(allocationAmount);
                            string paymentType = GetCellValue(doc, GetCell(row, "AH"));
                            string receiptType = GetCellValue(doc, GetCell(row, "AI"));
                            string policy = GetCellValue(doc, GetCell(row, "AJ"));
                            string comments = GetCellValue(doc, GetCell(row, "AK"));

                            uploadConcur = new DTO_PAYMENT_UPLOAD_CONCUR()
                            {
                                COMPANY_CODE = companyCode,
                                REPORT_ID = reportID,
                                REPORT_NAME = reportName,
                                EMPLOYEE_ID = employeeID,
                                EMPLOYEE = employee,
                                TRANSACTION_ID = transactionID,
                                TRANSACTION_DATE = dtTransactionDate.HasValue ? dtTransactionDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                                EXPENSE_TYPE = expenseType,
                                HCP_EXPENSE_TYPE = hcpExpenseType,
                                MATERIAL_CODE = materialCode,
                                CONSULTATION_NUMBER = consultationNumber,
                                HCP_TYPE_IDENTIFICATION = hcpTypeIdentification,
                                BUSINESS_PURPOSE = businessPurpose,
                                EXPENSE_AMOUNT = dcExpenseAmount,
                                REIMBURSEMENT_CURRENCY = reimbursementCurrency,
                                ATTENDEE_AMOUNT = dcAttendeeAmount,
                                EVENT_KEY = eventKey,
                                LOCAL_CODE = localCode,
                                HCP_CODE = hcpCode,
                                HCO_CODE = hcoCode,
                                CRM_EVENT_KEY = crmEventKey,
                                ATTENDEE_NAME = attendeeName,
                                ATTENDEE_FIRST_NAME = attendeeFirstName,
                                ATTENDEE_LAST_NAME = attendeeLastName,
                                TITLE = title,
                                COMPANY = company,
                                AFFILIATION = affiliation,
                                ATTENDEE_TYPE = attendeeType,
                                EXTERNAL_ID = externalID,
                                VENDOR = vender,
                                LOCATION = location,
                                REGION = region,
                                COUNTRY = country,
                                ALLOCATION_CODE = allocationCode,
                                ALLOCATION_NUMBER = allocationNumber,
                                ALLOCATION_AMOUNT = dcAllocationAmount,
                                PAYMENT_TYPE = paymentType,
                                RECEIPT_TYPE = receiptType,
                                POLICY = policy,
                                COMMENTS = comments,
                                STATUS = "Upload",
                                ERROR_MESSAGE = errorMsg,
                                IS_DELETED = "N",
                                CREATOR_ID = userID,

                            };

                            list.Add(uploadConcur);
                        }
                    }

                }                
                //Event Key가 없는 경우를 다시 확인한다.
                //Event Key가 존재하지 않는경우는 Local Code도 존재하지 않는다.
                //1. Event Key는 해당 자료의 TransactionID와 동일한 데이터의 Event Key로 
                //2. Local Code는 저장시점(USP_INSERT_PAYMENT_UPLOAD_CONCUR)에서 External ID로 조회하여 저장한다.
                foreach (DTO_PAYMENT_UPLOAD_CONCUR concur in list.Where(c => string.IsNullOrEmpty(c.EVENT_KEY)))
                {
                    DTO_PAYMENT_UPLOAD_CONCUR result = list.Find(f => f.TRANSACTION_ID.Equals(concur.TRANSACTION_ID) && f.EVENT_KEY != "");
                    if (result != null)
                    {
                        concur.EVENT_KEY = result.EVENT_KEY;
                    }
                    else
                    {
                        concur.ERROR_MESSAGE += concur.ERROR_MESSAGE.Length > 0 ? concur.ERROR_MESSAGE + "|EVENT_KEY" : "EVENT_KEY";
                    }
                }

                return list;
            }
            catch (IOException ioe)
            {
                throw ioe;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
