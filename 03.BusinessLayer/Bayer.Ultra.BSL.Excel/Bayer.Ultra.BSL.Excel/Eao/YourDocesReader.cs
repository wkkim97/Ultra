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
    public class YourDocesReader : BaseReader
    {
        public List<DTO_PAYMENT_UPLOAD_YOURDOCES> ReadPaymentYourdoces(string userID, string filePath)
        {
            List<DTO_PAYMENT_UPLOAD_YOURDOCES> list = null;
            if (!File.Exists(filePath)) return list;

            try
            {
                list = new List<DTO_PAYMENT_UPLOAD_YOURDOCES>();
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
                            DTO_PAYMENT_UPLOAD_YOURDOCES uploadYourdoces = null;
                            if (row.Descendants<Cell>().Count() < 1) continue;
                            string startKey = GetCellValue(doc, row.Descendants<Cell>().ElementAt(0));
                            if (startKey.Equals("Account"))
                            {
                                isStart = true;
                                continue;
                            }
                            if (!isStart) continue;

                            string strText = GetCellValue(doc, GetCell(row, "Q")).Trim();

                            if (strText.Length < 5 || !strText.ToUpper().StartsWith("ULTRA_YOUR")) continue;

                            string errorMsg = string.Empty;

                            string strAccount = GetCellValue(doc, GetCell(row, "A"));
                            string strName_1 = GetCellValue(doc, GetCell(row, "B"));
                            string strDocument_Number = GetCellValue(doc, GetCell(row, "C"));
                            string strDocument_Type = GetCellValue(doc, GetCell(row, "D"));
                            string strPayment_Block = GetCellValue(doc, GetCell(row, "E"));
                            string strDocument_Header_Text = GetCellValue(doc, GetCell(row, "F"));
                            string strDocument_Date = GetCellValue(doc, GetCell(row, "G"));
                            DateTime? dtDocument_Date = ConvertCellDate(strDocument_Date);
                            string strEntry_Date = GetCellValue(doc, GetCell(row, "H"));
                            DateTime? dtEntry_Date = ConvertCellDate(strEntry_Date);
                            string strPosting_Date = GetCellValue(doc, GetCell(row, "I"));
                            DateTime? dtPosting_Date = ConvertCellDate(strPosting_Date);
                            string strNet_due_date = GetCellValue(doc, GetCell(row, "J"));
                            DateTime? dtNet_due_date = ConvertCellDate(strNet_due_date);
                            string strAmount_in_doc_curr = GetCellValue(doc, GetCell(row, "K")); decimal dcAmount_in_doc_curr = ConvertToDecimal(strAmount_in_doc_curr);
                            string strDocument_currency = GetCellValue(doc, GetCell(row, "L"));
                            
                            string strLocal_Currency = GetCellValue(doc, GetCell(row, "N"));
                            //string strText                     = GetCellValue(doc, GetCell(row, "O"));
                            string strAmount_in_local_currency = GetCellValue(doc, GetCell(row, "M")); 

                            string strwithholding_tax_amount = GetCellValue(doc, GetCell(row, "O")); decimal dcwithholding_tax_amount = ConvertToDecimal(strwithholding_tax_amount);
                            string strwithholding_tax_baseamount = GetCellValue(doc, GetCell(row, "P")); decimal dcwithholding_tax_baseamount = ConvertToDecimal(strwithholding_tax_baseamount);
                            //INC12992715 : 
                            /*
                             * 현재 UlTra system에 업로드 되는 YOUR-DOCeS 자료의 값은 M열(Amount in local currency)+O열(Withholding tax amnt)입니다.
                                이를 <P열(Withhldg tax base amount)로 업로드 하되 P열의 값이 0인 경우, M열(Amount in local currency)+O열(Withholding tax amnt)로 업로드> 로 변경 요청 드립니다.
                                참고하시도록 예시 자료 보내 드리며 추가적으로 필요하신 사항 있으시면 말씀 부탁 드립니다.
                             */
                            /*
                             * L열이 KRW라면 P열로 업로드 하되 P열이 0이면 M열+O열, L열이 KRW가 아니라면 M열로 업로드 : 20211215 by Sumin Jo
                             */
                            //decimal dcAmount_in_local_currency = ConvertToDecimal(strAmount_in_local_currency) + dcwithholding_tax_amount;
                            decimal dcAmount_in_local_currency = dcwithholding_tax_baseamount;
                            if (strDocument_currency == "KRW")
                            {
                                if (dcwithholding_tax_baseamount == 0) dcAmount_in_local_currency = ConvertToDecimal(strAmount_in_local_currency) + dcwithholding_tax_amount;
                            }
                            else
                            {
                                dcAmount_in_local_currency = ConvertToDecimal(strAmount_in_local_currency);
                            }
                            

                            string strUser_name = GetCellValue(doc, GetCell(row, "R"));
                            string strClearing_date = GetCellValue(doc, GetCell(row, "S"));
                            DateTime? dtClearing_date = ConvertCellDate(strClearing_date);
                            string strClearing_Document = GetCellValue(doc, GetCell(row, "T"));
                            //Yourdoce 글자수 제한으로 20 추가 by Wookyung Kim 2018-04-02
                            string strReference_key_1 = "20"+ GetCellValue(doc, GetCell(row, "U"));
                            string strReference_key_2 = GetCellValue(doc, GetCell(row, "V"));
                            string strReference_key_3 = GetCellValue(doc, GetCell(row, "W"));

                            string hcoCode = "0";
                            string[] splits = strReference_key_3.Split(new char[] { '|' });
                            if (splits.Length == 2)
                            {
                                strReference_key_3 = splits[0];
                                hcoCode = splits[1];
                            }
                            uploadYourdoces = new DTO_PAYMENT_UPLOAD_YOURDOCES()
                            {
                                ACCOUNT = strAccount,
                                NAME_1 = strName_1,
                                DOCUMENT_NUMBER = strDocument_Number,
                                DOCUMENT_TYPE = strDocument_Type,
                                PAYMENT_BLOCK = strPayment_Block,
                                DOCUMENT_HEADER_TEXT = strDocument_Header_Text,
                                DOCUMENT_DATE = dtDocument_Date.HasValue ? dtDocument_Date.Value.ToString("yyyy-MM-dd") : string.Empty, //Convert.ToDateTime(strDocument_Date)

                                ENTRY_DATE = dtEntry_Date.HasValue ? dtEntry_Date.Value.ToString("yyyy-MM-dd") : string.Empty, //Convert.ToDateTime(strEntry_Date)

                                POSTING_DATE = dtPosting_Date.HasValue ? dtPosting_Date.Value.ToString("yyyy-MM-dd") : string.Empty, //Convert.ToDateTime(strPosting_Date)
                                NET_DUE_DATE = dtNet_due_date.HasValue ? dtNet_due_date.Value.ToString("yyyy-MM-dd") : string.Empty, //Convert.ToDateTime(strNet_due_date)
                                AMOUNT_IN_DOC_CURR = dcAmount_in_doc_curr * -1,
                                DOCUMENT_CURRENCY = strDocument_currency,
                                AMOUNT_IN_LOCAL_CURRENCY = dcAmount_in_local_currency * -1,
                                LOCAL_CURRENCY = strLocal_Currency,
                                WITHHOLDING_TAX_AMOUNT = dcwithholding_tax_amount * -1,
                                WITHHOLDING_TAX_BASE_AMOUNT= dcwithholding_tax_baseamount * -1,
                                TEXT = strText,
                                USER_NAME = strUser_name,
                                CLEARING_DATE = dtClearing_date.HasValue ? dtClearing_date.Value.ToString("yyyy-MM-dd") : string.Empty, //Convert.ToDateTime(strClearing_date)
                                CLEARING_DOCUMENT = strClearing_Document,
                                REFERENCE_KEY_1 = strReference_key_1,
                                REFERENCE_KEY_2 = strReference_key_2,
                                REFERENCE_KEY_3 = strReference_key_3,
                                HCO_CODE = hcoCode,
                                COMMENTS = "",
                                STATUS = "Upload",
                                ERROR_MESSAGE = errorMsg,
                                IS_DELETED = "N",
                                CREATOR_ID = userID
                            };

                            list.Add(uploadYourdoces);
                        }
                    }
                }
                //Event Key가 없는 경우를 다시 확인한다.
                //Event Key가 존재하지 않는경우는 Local Code도 존재하지 않는다.
                //1. Event Key는 해당 자료의 TransactionID와 동일한 데이터의 Event Key로 
                //2. Local Code는 저장시점(DTO_PAYMENT_UPLOAD_YOURDOCES)에서 External ID로 조회하여 저장한다.
                //foreach (DTO_PAYMENT_UPLOAD_YOURDOCES yourdoces in list.Where(c => string.IsNullOrEmpty(c.EVENT_KEY)))
                //{
                //    //DTO_PAYMENT_UPLOAD_YOURDOCES result = list.Find(f => f.TRANSACTION_ID.Equals(yourdoces.TRANSACTION_ID));
                //    //if (result != null)
                //    //{
                //    //    yourdoces.EVENT_KEY = result.EVENT_KEY;
                //    //}
                //    //else
                //    //{
                //    //    yourdoces.ERROR_MESSAGE += yourdoces.ERROR_MESSAGE.Length > 0 ? yourdoces.ERROR_MESSAGE + "|EVENT_KEY" : "EVENT_KEY";
                //    //}
                //}

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





        private DateTime? ConvertCellDate(string strDate)
        {
            double tmp = ConvertToDouble(strDate);
            DateTime? dtTransactionDate = null;
            if (tmp > 0) dtTransactionDate = DateTime.FromOADate(tmp);

            if (dtTransactionDate != null)
            {
                return dtTransactionDate;
            }
            else
            {
                return null;
            }
        }

    }
}
