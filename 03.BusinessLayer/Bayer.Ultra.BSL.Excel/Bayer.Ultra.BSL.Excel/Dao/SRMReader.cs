using Bayer.Ultra.Framework.Common.Dto.Approval;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Excel.Dao
{
    public class SRMReader : IDisposable
    {
        struct SRMCellTitle
        {
            public const string PO_NUMBER = "PO Number";
            public const string PO_NUMBER_FOR_GIMMICK_SOUVENIR = "PO Number for Gimmick/Souvenir";
            public const string TOTAL_COST_FOR_PARTICIPANTS = "Total cost for participants";
            public const string AGENCY_COST = "Agency Cost";
            public const string BANQUET_ROOM_VENUE_SYSTEM = "Banquet Room & Venue System";
            public const string PRINTS = "Prints";
            public const string KOREA_HCP = "KoreaHCP";
            public const string OTHER_HCP = "OtherHCP";
            public const string EMPLOYEE = "Employee";
        }
        public List<DTO_PAYMENT_UPLOAD_SRM> ReadPaymentSRM(string processID, string userID, string filePath)
        {
            List<DTO_PAYMENT_UPLOAD_SRM> list = null;
            if (!File.Exists(filePath)) return list;

            try
            {
                list = new List<DTO_PAYMENT_UPLOAD_SRM>();
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    System.Data.DataTable dt = new System.Data.DataTable();
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
                        decimal amount = 0;
                        string category = string.Empty;
                        foreach (Row row in rows) //this will also include your header row...
                        {
                            DTO_PAYMENT_UPLOAD_SRM uploadSRM = null;
                            if (row.Descendants<Cell>().Count() < 1) continue;
                            string valueKey = GetCellValue(doc, row.Descendants<Cell>().ElementAt(0));

                            if (string.IsNullOrEmpty(valueKey)) continue;

                            switch (valueKey)
                            {
                                case SRMCellTitle.PO_NUMBER:
                                    value = GetCellValue(doc, row.Descendants<Cell>().ElementAt(8));
                                    poNumber = value;
                                    break;
                                case SRMCellTitle.PO_NUMBER_FOR_GIMMICK_SOUVENIR:
                                    value = GetCellValue(doc, row.Descendants<Cell>().ElementAt(8));
                                    poNumberGS = value;
                                    break;
                                case SRMCellTitle.AGENCY_COST: //Agency Cost
                                case SRMCellTitle.BANQUET_ROOM_VENUE_SYSTEM:
                                case SRMCellTitle.PRINTS:
                                    value = GetCellValue(doc, row.Descendants<Cell>().ElementAt(8));
                                    amount = ConvertToDecimal(value);
                                    if (valueKey.Equals(SRMCellTitle.AGENCY_COST)) category = "0002";
                                    else if (valueKey.Equals(SRMCellTitle.BANQUET_ROOM_VENUE_SYSTEM)) category = "0003";
                                    else if (valueKey.Equals(SRMCellTitle.PRINTS)) category = "0006";
                                    if (!string.IsNullOrEmpty(category))
                                    {
                                        uploadSRM = new DTO_PAYMENT_UPLOAD_SRM()
                                        {
                                            PROCESS_ID = processID,
                                            SRM_IDX = list.Count + 1,
                                            PARTICIPANT_TYPE = "Total",
                                            PO_NUMBER = poNumber,
                                            CATEGORY_CODE = category,
                                            CATEGORY_NAME = valueKey,
                                            HCP_CODE = string.Empty,
                                            HCP_NAME = string.Empty,
                                            HCO_CODE = string.Empty,
                                            HCO_NAME = string.Empty,
                                            AMOUNT = amount,
                                            ERROR_MESSAGE = string.Empty,
                                            COMMENT = string.Empty,
                                            STATUS = "Upload",
                                            IS_DELETED = "N",
                                            CREATOR_ID = userID,
                                            CREATE_DATE = DateTime.Now,
                                            UPDATER_ID = userID,
                                        };
                                        list.Add(uploadSRM);
                                    }
                                    break;
                                case SRMCellTitle.KOREA_HCP:
                                case SRMCellTitle.OTHER_HCP:
                                case SRMCellTitle.EMPLOYEE:
                                    string hcpCode = GetCellValue(doc, row.Descendants<Cell>().ElementAt(2));
                                    string hcpName = GetCellValue(doc, row.Descendants<Cell>().ElementAt(1));
                                    string hcoCode = GetCellValue(doc, row.Descendants<Cell>().ElementAt(4));
                                    string hcoName = GetCellValue(doc, row.Descendants<Cell>().ElementAt(3));
                                    string comment = GetCellValue(doc, row.Descendants<Cell>().ElementAt(12));
                                    for (int i = 0; i < 4; i++)
                                    {
                                        value = GetCellValue(doc, row.Descendants<Cell>().ElementAt(i + 8));
                                        amount = ConvertToDecimal(value);
                                        string errorMsg = string.Empty;
                                        if(amount < 0)
                                        {
                                            //금액오류인 경우
                                            amount = 0;
                                            errorMsg = "Error:" + value;
                                        }
                                        if (i == 0) category = "0001";
                                        else if (i == 1) category = "0005";
                                        else if (i == 2) category = "0007";
                                        else if (i == 3) category = "0004";
                                        if (!string.IsNullOrEmpty(category))
                                        {
                                            uploadSRM = new DTO_PAYMENT_UPLOAD_SRM()
                                            {
                                                PROCESS_ID = processID,
                                                SRM_IDX = list.Count + 1,
                                                PARTICIPANT_TYPE = valueKey,
                                                PO_NUMBER = poNumber,
                                                CATEGORY_CODE = category,
                                                CATEGORY_NAME = valueKey,
                                                HCP_CODE = hcpCode,
                                                HCP_NAME = hcpName,
                                                HCO_CODE = hcoCode,
                                                HCO_NAME = hcoName,
                                                AMOUNT = amount,
                                                ERROR_MESSAGE = errorMsg,
                                                COMMENT = comment,
                                                STATUS = "Upload",
                                                IS_DELETED = "N",
                                                CREATOR_ID = userID,
                                                CREATE_DATE = DateTime.Now,
                                                UPDATER_ID = userID,
                                            };
                                            list.Add(uploadSRM);
                                        }

                                    }
                                    break;
                            }
                        }
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

        public string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(cell.CellValue.InnerXml)].InnerText;
            }
            else if (cell.CellValue != null)
            {
                return cell.CellValue.Text;
            }
            else
            {
                return string.Empty;
            }
        }

        public decimal ConvertToDecimal(string str)
        {
            try
            {
                string strValue = str.Replace(",", string.Empty).Trim();
                if (string.IsNullOrEmpty(strValue)) return 0;
                else
                    return Convert.ToDecimal(str.Replace(",", string.Empty).Trim());
            }
            catch
            {
                return -1;
            }
        }

        public void Dispose()
        {
        }
    }
}
