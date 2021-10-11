using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Bayer.Ultra.Framework.Config;
using Bayer.Ultra.Framework.Common.Dto.Approval;

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using Bayer.Ultra.Core;
using Bayer.Ultra.Framework.Common.Dto.Report;
using System.Data;
using System.Reflection;

namespace Bayer.Ultra.BSL.Excel.Dao
{
    public class FileHandler : Eao.BaseReader, IDisposable
    {
        private string ATTACH_PATH = WebSiteConfigHandler.WebServer.UploadFile.Attach.PhysicalPath; //@"C:\Temp\upload\attach";

        const string WORKSHEET_NAME_SRM = "ExpenseReport _final";  // Worksheet Name
        const string WORKSHEET_NAME_ATTENDEES = "AttendeesList";  // Worksheet Name
        

        private string strSourceFileName = string.Empty;
        private string strDestPathCombine = string.Empty;
        private string strDestFileName = string.Empty;

        private void ExcelFilePathNameCombine(string fileName, string userId, string strProcessID, string strType)
        {
            string SAMPLE_PATH = "Sample";
            string SAMPLE_FILE = string.Empty;
            string MIDDEL_PATH = string.Empty;
            string DNLOAD_PATH = string.Empty;

            if (strType == "Attendees")
            {
                SAMPLE_FILE = "AttendeesList.xlsx";
                MIDDEL_PATH = "PaymentAttendees";
                DNLOAD_PATH = "AttendeesList";
            }
            else
            {
                SAMPLE_FILE = "SRM_Report.xlsx";
                MIDDEL_PATH = "PaymentSRM";
                DNLOAD_PATH = "Participants";
            }

            // sample 폴더에서 Excel 파일 복사
            strSourceFileName = Path.Combine(ATTACH_PATH, MIDDEL_PATH, SAMPLE_PATH, SAMPLE_FILE);
            strDestPathCombine = Path.Combine(ATTACH_PATH, Consts.FILES_EVENT_ROOT_DIR, strProcessID, DNLOAD_PATH);
            strDestFileName = Path.Combine(strDestPathCombine, fileName);
        }

        private void ExcelFileCopy()
        {
            // 다운로드 경로 생성
            if (!Directory.Exists(strDestPathCombine)) Directory.CreateDirectory(strDestPathCombine);

            // Excel File 복사, 기존 엑셀파일이 존재하는 경우는 덮어쓰기 함.
            File.Copy(strSourceFileName, strDestFileName, true);
        }


        public string CreateExcelFileSRM(string fileName, string userId, string strProcessID, List<DTO_MODULE_PARTICIPANTS> dtoParticipantsList)
        {
            const string FORMULA_CELL_COLUMN_NAME = "H";             // 한 로우에 값을 합산할 식(=SUM)을 넣는 셀 컬럼명
            const string FORMULA_CELL_ROW = "4";             // 각 로우에 합한 값(Total Cost)의 식(=SUM)을 넣는 Row 위치
            const string FORMULA_RANGE_FROM_CELL_COLUMN_NAME = "I";  // 한 로우에 값을 합산할 셀의 처음
            const string FORMULA_RANGE_TO_CELL_COLUMN_NAME = "L";    // 한 로우에 값을 합산할 셀의 마지막

            const int LIST_ROW_START_INDEX = 12;
            const int SAMPLE_LIST_DEFAULT_ROWS = 1;   // 샘플에 있는 참여자 리스트 갯수 (참여자가 더 많은 경우 이 값을 기준으로 ROW를 늘려준다.)

            try
            {
                // 다운로드 경로 생성 및 
                ExcelFilePathNameCombine(fileName, userId, strProcessID, "SRM");
                // 샘플 Excel File 복사
                ExcelFileCopy();

                int intParticipantCount = dtoParticipantsList.Where(P => P.IS_DELETED != "Y").Where(P => P.IS_ATTENDED == "Y").Where(P => P.PARTICIPANT_TYPE != "Employee").Count();

                //int intRowCount = dtoParticipantsList.Count;
                int intRowCount = dtoParticipantsList.Where(P => P.IS_DELETED != "Y").Count();




                // 참여자 수 많큼 엑셀에 ROW를 생성
                if (File.Exists(strDestFileName) && (intRowCount > SAMPLE_LIST_DEFAULT_ROWS))
                {
                    using (SpreadsheetDocument spreadSheet = SpreadsheetDocument.Open(strDestFileName, true))
                    {
                        WorksheetPart worksheetPart = GetWorksheetPartByName(spreadSheet, WORKSHEET_NAME_SRM);
                        Worksheet worksheet = worksheetPart.Worksheet;
                        SheetData sheetData = worksheet.GetFirstChild<SheetData>();

                        uint startIndex = LIST_ROW_START_INDEX;
                        uint indexCount = Convert.ToUInt32(intRowCount - SAMPLE_LIST_DEFAULT_ROWS);

                        // H12:H12 + row
                        Row GetRow = worksheet.GetFirstChild<SheetData>().Elements<Row>().Where(r => r.RowIndex == FORMULA_CELL_ROW).First();
                        Cell GetCell = GetRow.Elements<Cell>().Where(c => string.Compare(c.CellReference.Value, FORMULA_CELL_COLUMN_NAME + FORMULA_CELL_ROW, true) == 0).First();
                        GetCell.CellFormula = new CellFormula(String.Format("SUM({0}{1}:{0}{2})", FORMULA_CELL_COLUMN_NAME, LIST_ROW_START_INDEX, LIST_ROW_START_INDEX + intRowCount - 1));

                        for (uint rowIndex = startIndex; rowIndex < startIndex + indexCount; rowIndex++)
                        {
                            Row refRow = sheetData.Elements<Row>().Where(r => rowIndex == r.RowIndex).First();
                            Row InsRow = null;

                            InsRow = CopyToLine(refRow, rowIndex, sheetData);
                            Cell formulaCell = InsertCellInWorksheet(FORMULA_CELL_COLUMN_NAME, rowIndex + 1, worksheetPart);
                            formulaCell.CellFormula = new CellFormula(String.Format("SUM({1}{0}:{2}{0})", rowIndex + 1, FORMULA_RANGE_FROM_CELL_COLUMN_NAME, FORMULA_RANGE_TO_CELL_COLUMN_NAME));
                        }

                        worksheetPart.Worksheet.Save();

                        // for recacluation of formula
                        spreadSheet.WorkbookPart.Workbook.CalculationProperties.ForceFullCalculation = true;
                        spreadSheet.WorkbookPart.Workbook.CalculationProperties.FullCalculationOnLoad = true;

                        spreadSheet.Close();
                    }
                }

                // 다운로드할 엑셀 파일에 참여자 리스트를 넣는다.
                ExcelDataWrite_ParticipantsList(dtoParticipantsList, strDestFileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return strDestFileName;
        }


        //
        public string CreateExcelFileAttendeesList(string fileName, string userId, string strProcessID, List<EventAgendaSummaryDto> dtoAgendaRoleSummary, List<DTO_MODULE_PARTICIPANTS> dtoParticipantsList, DTO_EVENT_ATTENDEES_LIST_INFO dtoEventAttendeesListInfo)
        {
            const int LIST_AGENDA_START_INDEX = 11;
            const int LIST_PARTICIPANTS_START_INDEX = 26;   // 참석자 리스트가 시작되는 Row 위치
            const int SAMPLE_LIST_DEFAULT_ROWS = 1;   // 샘플에 있는 참여자 리스트 갯수 (참여자가 더 많은 경우 이 값을 기준으로 ROW를 늘려준다.)

            try
            {
                String username_id = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                // 다운로드 경로 생성 및 
                ExcelFilePathNameCombine(fileName, userId, strProcessID, "Attendees");
                // 샘플 Excel File 복사
                ExcelFileCopy();

                int intAgendaCount = dtoAgendaRoleSummary.Count;
                

                // 아젠다 만큼 엑셀에 ROW를 생성
                if (File.Exists(strDestFileName)) // && (intAgendaCount > 0))
                {
                     username_id = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    using (SpreadsheetDocument spreadSheet = SpreadsheetDocument.Open(strDestFileName, true))
                    {
                        WorksheetPart worksheetPart = GetWorksheetPartByName(spreadSheet, WORKSHEET_NAME_ATTENDEES);
                        Worksheet worksheet = worksheetPart.Worksheet;
                        SheetData sheetData = worksheet.GetFirstChild<SheetData>();

                        uint startIndex = LIST_AGENDA_START_INDEX;
                        

                        string strVal = string.Empty;
                        MergeCells mergeCells = worksheet.Elements<MergeCells>().First();

                        strVal = String.Format("B{0}:C{0}", startIndex);
                        MergeCell mergeCell1 = new MergeCell() { Reference = new StringValue(strVal) };
                        mergeCells.Append(mergeCell1);

                        strVal = String.Format("E{0}:G{0}", startIndex);
                        MergeCell mergeCell2 = new MergeCell() { Reference = new StringValue(strVal) };
                        mergeCells.Append(mergeCell2);

                        strVal = String.Format("H{0}:I{0}", startIndex);
                        MergeCell mergeCell3 = new MergeCell() { Reference = new StringValue(strVal) };
                        mergeCells.Append(mergeCell3);

                        if (intAgendaCount > 1)
                        {
                            uint indexCount = Convert.ToUInt32(intAgendaCount - SAMPLE_LIST_DEFAULT_ROWS);

                            for (uint rowIndex = startIndex; rowIndex < startIndex + indexCount; rowIndex++)
                            {
                                Row refRow = sheetData.Elements<Row>().Where(r => rowIndex == r.RowIndex).First();
                                Row InsRow = null;

                                InsRow = CopyToLine(refRow, rowIndex, sheetData);

                                strVal = String.Format("B{0}:C{0}", rowIndex + 1);
                                mergeCell1 = new MergeCell() { Reference = new StringValue(strVal) };
                                mergeCells.Append(mergeCell1);

                                strVal = String.Format("E{0}:G{0}", rowIndex + 1);
                                mergeCell2 = new MergeCell() { Reference = new StringValue(strVal) };
                                mergeCells.Append(mergeCell2);

                                strVal = String.Format("H{0}:I{0}", rowIndex + 1);
                                mergeCell3 = new MergeCell() { Reference = new StringValue(strVal) };
                                mergeCells.Append(mergeCell3);
                            }
                        }

                        worksheetPart.Worksheet.Save();

                        // for recacluation of formula
                        spreadSheet.WorkbookPart.Workbook.CalculationProperties.ForceFullCalculation = true;
                        spreadSheet.WorkbookPart.Workbook.CalculationProperties.FullCalculationOnLoad = true;

                        spreadSheet.Close();
                    }
                }

                //int intParticipantCount = dtoParticipantsList.Where(P => P.IS_DELETED != "Y").Where(P => P.IS_ATTENDED == "Y").Where(P => P.PARTICIPANT_TYPE != "Employee").Count();
                int intParticipantCount = dtoParticipantsList.Where(P => P.IS_DELETED != "Y").Where(P => P.IS_ATTENDED == "Y").Count();

                if (intAgendaCount == 0) intAgendaCount = 1;

                // 참여자 수 많큼 엑셀에 ROW를 생성
                if (File.Exists(strDestFileName)) // && (intParticipantCount > 0))
                {
                    using (SpreadsheetDocument spreadSheet = SpreadsheetDocument.Open(strDestFileName, true))
                    {
                        WorksheetPart worksheetPart = GetWorksheetPartByName(spreadSheet, WORKSHEET_NAME_ATTENDEES);
                        Worksheet worksheet = worksheetPart.Worksheet;
                        SheetData sheetData = worksheet.GetFirstChild<SheetData>();

                        uint startIndex = Convert.ToUInt32(LIST_PARTICIPANTS_START_INDEX + intAgendaCount - 1);

                        if (intParticipantCount == 0) intParticipantCount = 1;
                        uint indexCount = Convert.ToUInt32(intParticipantCount - SAMPLE_LIST_DEFAULT_ROWS);

                        for (uint rowIndex = startIndex; rowIndex < startIndex + indexCount; rowIndex++)
                        {
                            Row refRow = sheetData.Elements<Row>().Where(r => rowIndex == r.RowIndex).First();
                            Row InsRow = null;

                            InsRow = CopyToLine(refRow, rowIndex, sheetData);
                        }

                        string strVal = string.Empty;
                        MergeCells mergeCells = worksheet.Elements<MergeCells>().First();

                        for (uint rowIndex = startIndex - 1; rowIndex < startIndex + indexCount + 1; rowIndex++)
                        {
                            strVal = String.Format("C{0}:D{0}", rowIndex);
                            MergeCell mergeCell1 = new MergeCell() { Reference = new StringValue(strVal) };
                            mergeCells.Append(mergeCell1);
                            strVal = String.Format("G{0}:H{0}", rowIndex);
                            MergeCell mergeCell2 = new MergeCell() { Reference = new StringValue(strVal) };
                            mergeCells.Append(mergeCell2);
                            strVal = String.Format("E{0}:F{0}", rowIndex);
                            MergeCell mergeCell3 = new MergeCell() { Reference = new StringValue(strVal) };
                            mergeCells.Append(mergeCell3);
                        }

                        worksheetPart.Worksheet.Save();

                        // for recacluation of formula
                        spreadSheet.WorkbookPart.Workbook.CalculationProperties.ForceFullCalculation = true;
                        spreadSheet.WorkbookPart.Workbook.CalculationProperties.FullCalculationOnLoad = true;

                        spreadSheet.Close();
                    }
                }

                uint uintStartRow_Participant = Convert.ToUInt32(LIST_PARTICIPANTS_START_INDEX + intAgendaCount - 1);
                // 다운로드할 엑셀 파일에 참여자 리스트를 넣는다.
                ExcelDataWrite_AttendeesList(dtoAgendaRoleSummary, dtoParticipantsList, dtoEventAttendeesListInfo, strDestFileName, LIST_AGENDA_START_INDEX, uintStartRow_Participant);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return strDestFileName;
        }

        public void ExcelDataWrite_ParticipantsList(List<DTO_MODULE_PARTICIPANTS> dtoParticipantsList, string docName)
        {
            try
            {
                using (SpreadsheetDocument spreadSheet = SpreadsheetDocument.Open(docName, true))
                {
                    WorksheetPart worksheetPart = GetWorksheetPartByName(spreadSheet, WORKSHEET_NAME_SRM);

                    if (worksheetPart != null)
                    {
                        uint uintListStartRow = 12;
                        string[] excelColumnName = { "A", "B", "C", "D", "E", "F", "G" };

                        uint currentIndex = 0;
                        Cell cell = null;

                        foreach (DTO_MODULE_PARTICIPANTS dtoParticipant in dtoParticipantsList)
                        {
                            if (dtoParticipant.IS_DELETED != "Y")
                            {
                                foreach (string strColName in excelColumnName)
                                {
                                    string strValue = string.Empty;
                                    switch (strColName)
                                    {
                                        case "A":  //[PARTICIPANT_TYPE]
                                            strValue = dtoParticipant.PARTICIPANT_TYPE;
                                            break;
                                        case "B":  //[HCP_NAME]
                                            strValue = dtoParticipant.HCP_NAME;
                                            break;
                                        case "C":  //[HCP_CODE]
                                            strValue = dtoParticipant.HCP_CODE;
                                            break;
                                        case "D":  //[HCO_NAME] + [SPECIALTY_NAME]
                                            strValue = dtoParticipant.HCO_NAME + "/" + dtoParticipant.SPECIALTY_NAME;
                                            break;
                                        case "E":  //[HCO_CODE]
                                            strValue = dtoParticipant.HCO_CODE;
                                            break;
                                        case "F":  //[ROLE]
                                            strValue = dtoParticipant.ROLE;
                                            break;
                                        case "G":  //[IS_ATTENDED]
                                            strValue = dtoParticipant.IS_ATTENDED;
                                            break;
                                    }

                                    cell = GetCell(worksheetPart.Worksheet, strColName, currentIndex + uintListStartRow);
                                    cell.CellValue = new CellValue(strValue);
                                    cell.DataType = new EnumValue<CellValues>(CellValues.String); //.Number);
                                }
                                currentIndex++;
                            }
                        }
                        // Save the worksheet.
                        worksheetPart.Worksheet.Save();
                        spreadSheet.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ExcelDataWrite_AttendeesList(List<EventAgendaSummaryDto> dtoAgendaRoleSummary, List<DTO_MODULE_PARTICIPANTS> dtoParticipantsList, DTO_EVENT_ATTENDEES_LIST_INFO dtoEventAttendeesListInfo, string docName, uint uintStartRow_Agenda, uint uintStartRow_Participant)
        {
            try
            {
                using (SpreadsheetDocument spreadSheet = SpreadsheetDocument.Open(docName, true))
                {
                    WorksheetPart worksheetPart = GetWorksheetPartByName(spreadSheet, WORKSHEET_NAME_ATTENDEES);

                    if (worksheetPart != null)
                    {
                        //uint uintListStartRow = uintStartRow_Participant;
                        string[] excelColumnName_Agenda = { "B", "D", "E", "H" };
                        string[] excelColumnName_Participants = { "B", "C", "E", "G", "I" };

                        uint currentIndex = 0;
                        Cell cell = null;

                        // 행사명
                        cell = GetCell(worksheetPart.Worksheet, "F", 4);
                        cell.CellValue = new CellValue(dtoEventAttendeesListInfo.SUBJECT);
                        cell.DataType = new EnumValue<CellValues>(CellValues.String);

                        // 행사일시
                        cell = GetCell(worksheetPart.Worksheet, "F", 5);
                        cell.CellValue = new CellValue(dtoEventAttendeesListInfo.START_TIME);
                        cell.DataType = new EnumValue<CellValues>(CellValues.String);

                        // 행사장소
                        cell = GetCell(worksheetPart.Worksheet, "F", 6);
                        cell.CellValue = new CellValue(dtoEventAttendeesListInfo.ADDRESS_OF_VENUE);
                        cell.DataType = new EnumValue<CellValues>(CellValues.String);

                        // 담당자
                        cell = GetCell(worksheetPart.Worksheet, "F", 7);
                        cell.CellValue = new CellValue(dtoEventAttendeesListInfo.REQUESTER_NAME + " / " + dtoEventAttendeesListInfo.REQUESTER_DEPT);
                        cell.DataType = new EnumValue<CellValues>(CellValues.String);

                        // 관련 제품명
                        cell = GetCell(worksheetPart.Worksheet, "F", 8);
                        cell.CellValue = new CellValue(dtoEventAttendeesListInfo.PRODUCT_NAME);
                        cell.DataType = new EnumValue<CellValues>(CellValues.String);

                        // 아젠다 리스트
                        foreach (EventAgendaSummaryDto dtoAgenda in dtoAgendaRoleSummary)
                        {
                            foreach (string strColName in excelColumnName_Agenda)
                            {
                                string strValue = string.Empty;
                                switch (strColName)
                                {
                                    case "B":  // 시작시간
                                        strValue = dtoAgenda.START_DATE + " " + dtoAgenda.START_TIME.Split('(')[0].ToString();
                                        break;
                                    case "D":  // 소요시간
                                        strValue = dtoAgenda.START_TIME.Split('(')[1].Replace(")", "").ToString();
                                        break;
                                    case "E":  // 주제
                                        strValue = dtoAgenda.SUBJECT;
                                        break;
                                    case "H":  // 진행자
                                        strValue = dtoAgenda.ROLES;
                                        break;
                                }

                                cell = GetCell(worksheetPart.Worksheet, strColName, currentIndex + uintStartRow_Agenda);
                                cell.CellValue = new CellValue(strValue);
                                cell.DataType = new EnumValue<CellValues>(CellValues.String); //.Number);
                            }
                            currentIndex++;
                        }


                        currentIndex = 0;

                        // 참석자 리스트
                        foreach (DTO_MODULE_PARTICIPANTS dtoParticipant in dtoParticipantsList)
                        {
                            //if (dtoParticipant.IS_DELETED != "Y" && dtoParticipant.IS_ATTENDED == "Y" && dtoParticipant.PARTICIPANT_TYPE != "Employee")
                            //attend list 는 모든 participants 가 나와야 된다(included employee)
                                if (dtoParticipant.IS_DELETED != "Y" && dtoParticipant.IS_ATTENDED == "Y" )
                                {
                                foreach (string strColName in excelColumnName_Participants)
                                {
                                    string strValue = string.Empty;
                                    switch (strColName)
                                    {
                                        case "B":  // No.
                                            strValue = (currentIndex + 1).ToString();
                                            break;
                                        case "E":  // 성명
                                            strValue = dtoParticipant.HCP_NAME;
                                            break;
                                        case "G":  // 병의원 / 소속기관
                                            strValue = dtoParticipant.HCO_NAME+"\r\n("+ dtoParticipant.SPECIALTY_NAME+")";
                                            break;
                                        //case "G":  // 진료과명
                                        //    strValue = dtoParticipant.SPECIALTY_NAME;
                                        //    break;
                                        case "C":  // 서명
                                            strValue = "";
                                            break;
                                        case "I":  // 비고
                                            strValue = "";
                                            break;


                                       // case "B":  // No.
                                       //    strValue = (currentIndex + 1).ToString();
                                       //    break;
                                       //case "C":  // 성명
                                       //    strValue = dtoParticipant.HCP_NAME;
                                       //    break;
                                       //case "D":  // 병의원 / 소속기관
                                       //    strValue = dtoParticipant.HCO_NAME;
                                       //    break;
                                       //case "G":  // 진료과명
                                       //    strValue = dtoParticipant.SPECIALTY_NAME;
                                       //    break;
                                       //case "H":  // 서명
                                       //    strValue = "";
                                       //    break;
                                       //case "I":  // 비고
                                       //    strValue = "";
                                       //    break;
                                    
                                    }

                                    cell = GetCell(worksheetPart.Worksheet, strColName, currentIndex + uintStartRow_Participant);
                                    cell.CellValue = new CellValue(strValue);
                                    cell.DataType = new EnumValue<CellValues>(CellValues.String); //.Number);
                                }
                                currentIndex++;
                            }
                            
                        }
                        // Save the worksheet.
                        worksheetPart.Worksheet.Save();
                        spreadSheet.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        private static WorksheetPart GetWorksheetPartByName(SpreadsheetDocument document, string sheetName) //static
        {
            IEnumerable<Sheet> sheets = document.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>().Where(s => s.Name == sheetName);

            if (sheets.Count() == 0)
            {
                // The specified worksheet does not exist.
                return null;
            }

            string relationshipId = sheets.First().Id.Value;
            WorksheetPart worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(relationshipId);
            return worksheetPart;
        }


        internal static Row CopyToLine(Row refRow, uint rowIndex, SheetData sheetData)  //internal 
        {
            uint newRowIndex;
            var newRow = (Row)refRow.CloneNode(true);
            // Loop through all the rows in the worksheet with higher row 
            // index values than the one you just added. For each one,
            // increment the existing row index.

            //rowIndex = rowIndex - 1;

            IEnumerable<Row> rows = sheetData.Descendants<Row>().Where(r => r.RowIndex.Value >= rowIndex);
            foreach (Row row in rows)
            {
                newRowIndex = System.Convert.ToUInt32(row.RowIndex.Value + 1);

                foreach (Cell cell in row.Elements<Cell>())
                {
                    // Update the references for reserved cells.
                    string cellReference = cell.CellReference.Value;
                    cell.CellReference = new StringValue(cellReference.Replace(row.RowIndex.Value.ToString(), newRowIndex.ToString()));
                }
                // Update the row index.
                row.RowIndex = new UInt32Value(newRowIndex);
            }

            sheetData.InsertBefore(newRow, refRow);
            return newRow;
        }

        private static Cell InsertCellInWorksheet(string columnName, uint rowIndex, WorksheetPart worksheetPart)  //static
        {
            Worksheet worksheet = worksheetPart.Worksheet;
            SheetData sheetData = worksheet.GetFirstChild<SheetData>();
            string cellReference = columnName + rowIndex;

            // If the worksheet does not contain a row with the specified row index, insert one.
            Row row = row = sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).First();

            // If there is not a cell with the specified column name, insert one.
            if (row.Elements<Cell>().Where(c => c.CellReference.Value == columnName + rowIndex).Count() > 0)
            {
                return row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).First();
            }
            else
            {
                // Cells must be in sequential order according to CellReference. Determine where to insert the new cell.
                Cell refCell = null;
                foreach (Cell cell in row.Elements<Cell>())
                {
                    if (cell.CellReference.Value.Length == cellReference.Length)
                    {
                        if (string.Compare(cell.CellReference.Value, cellReference, true) > 0)
                        {
                            refCell = cell;
                            break;
                        }
                    }
                }

                Cell newCell = new Cell() { CellReference = cellReference };
                row.InsertBefore(newCell, refCell);

                worksheet.Save();
                return newCell;
            }
        }

        public static bool CreateExcelDocument<T>(List<T> source, List<ReportColumnsDto> columns, string xlsxFilePath)
        {
            DataSet ds = new DataSet();
            try
            {
                ds.Tables.Add(ListToDataTable(source, columns));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return CreateExcelDocument(ds, columns, xlsxFilePath );
        }

        public static bool CreateExcelDocument<T>(List<T> source, string xlsxFilePath)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(ListToDataTable(source));

            return CreateExcelDocument(ds, null, xlsxFilePath);
        }
       
        public static bool CreateExcelDocument(DataSet source,  List<ReportColumnsDto> columns, string excelFilename)
        {
            try
            {
                using (SpreadsheetDocument document = SpreadsheetDocument.Create(excelFilename, SpreadsheetDocumentType.Workbook))
                {
                    
                    WriteExcelFile(source, document,columns);
                }
             
                return true;
            }
            catch (Exception ex)
            { 
                return false;
            }
        }
        public static bool AddExcelDocument<T>(List<T> source, List<ReportColumnsDto> columns, string xlsxFilePath)
        {
            DataSet ds = new DataSet();
            try
            {
                ds.Tables.Add(ListToDataTable(source, columns));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return AddExcelDocument(ds, columns, xlsxFilePath);
        }

        public static bool AddExcelDocument(DataSet source, List<ReportColumnsDto> columns, string excelFilename)
        {
            try
            {
                //InsertWorksheet(excelFilename);
                AddWriteExcelFile(source, SpreadsheetDocument.Open(excelFilename, true ), columns);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private static void AddWriteExcelFile(DataSet ds, SpreadsheetDocument spreadsheet, List<ReportColumnsDto> columns)
        {
            //  Create the Excel file contents.  This function is used when creating an Excel file either writing 
            //  to a file, or writing to a MemoryStream.


            //  Loop through each of the DataTables in our DataSet, and create a new Excel Worksheet for each.
            uint worksheetNumber = 1;

            foreach (DataTable dt in ds.Tables)
            {
                //  For each worksheet you want to create
                string workSheetID = "rId" + worksheetNumber.ToString();
                string worksheetName = dt.TableName;

                WorksheetPart newWorksheetPart = spreadsheet.WorkbookPart.AddNewPart<WorksheetPart>();
                newWorksheetPart.Worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet();

                // create sheet data
                newWorksheetPart.Worksheet.AppendChild(new DocumentFormat.OpenXml.Spreadsheet.SheetData());

                // save worksheet
                WriteDataTableToExcelWorksheet(dt, newWorksheetPart, columns);
                newWorksheetPart.Worksheet.Save();


                // create the worksheet to workbook relation
                //if (worksheetNumber == 1)
                //    spreadsheet.WorkbookPart.Workbook.AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Sheets());

                if (spreadsheet.WorkbookPart.Workbook.Sheets == null)
                {
                    spreadsheet.WorkbookPart.Workbook.AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Sheets());
                }
                else
                {
                    worksheetNumber = Convert.ToUInt32(spreadsheet.WorkbookPart.Workbook.Sheets.Count() + 1);
                }

                spreadsheet.WorkbookPart.Workbook.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.Sheets>().AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Sheet()
                {
                    Id = spreadsheet.WorkbookPart.GetIdOfPart(newWorksheetPart),
                    SheetId = (uint)worksheetNumber,
                    Name = dt.TableName + worksheetNumber
                });

                worksheetNumber++;
            }

            spreadsheet.WorkbookPart.Workbook.Save();
            spreadsheet.Close();
        }


        private static void WriteExcelFile(DataSet ds, SpreadsheetDocument spreadsheet, List<ReportColumnsDto> columns)
        {
            //  Create the Excel file contents.  This function is used when creating an Excel file either writing 
            //  to a file, or writing to a MemoryStream.
            
            spreadsheet.AddWorkbookPart();
            spreadsheet.WorkbookPart.Workbook = new DocumentFormat.OpenXml.Spreadsheet.Workbook();
            //  My thanks to James Miera for the following line of code (which prevents crashes in Excel 2010)
            spreadsheet.WorkbookPart.Workbook.Append(new BookViews(new WorkbookView()));
            

            //  If we don't add a "WorkbookStylesPart", OLEDB will refuse to connect to this .xlsx file !
            WorkbookStylesPart workbookStylesPart = spreadsheet.WorkbookPart.AddNewPart<WorkbookStylesPart>("rIdStyles");
            //Stylesheet stylesheet = new Stylesheet();
            workbookStylesPart.Stylesheet = GenerateStylesheet();
            workbookStylesPart.Stylesheet.Save();
 
            //  Loop through each of the DataTables in our DataSet, and create a new Excel Worksheet for each.
            uint worksheetNumber = 1;
           
            foreach (DataTable dt in ds.Tables)
            {
                //  For each worksheet you want to create
                string workSheetID = "rId" + worksheetNumber.ToString();
                string worksheetName = dt.TableName;

                WorksheetPart newWorksheetPart = spreadsheet.WorkbookPart.AddNewPart<WorksheetPart>();
                newWorksheetPart.Worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet();

                // create sheet data
                newWorksheetPart.Worksheet.AppendChild(new DocumentFormat.OpenXml.Spreadsheet.SheetData());

                // save worksheet
                WriteDataTableToExcelWorksheet(dt, newWorksheetPart, columns);
                newWorksheetPart.Worksheet.Save();


                // create the worksheet to workbook relation
                //if (worksheetNumber == 1)
                //    spreadsheet.WorkbookPart.Workbook.AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Sheets());

                if (spreadsheet.WorkbookPart.Workbook.Sheets == null)
                {
                    spreadsheet.WorkbookPart.Workbook.AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Sheets());
                }
                else
                {
                    worksheetNumber = Convert.ToUInt32(spreadsheet.WorkbookPart.Workbook.Sheets.Count() + 1);
                }

                spreadsheet.WorkbookPart.Workbook.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.Sheets>().AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Sheet()
                {
                    Id = spreadsheet.WorkbookPart.GetIdOfPart(newWorksheetPart),
                    SheetId = (uint)worksheetNumber,
                    Name = dt.TableName+ worksheetNumber
                });

                worksheetNumber++;
            }
            
            spreadsheet.WorkbookPart.Workbook.Save();
        }

        public static void InsertWorksheet(string docName)
        {
            // Open the document for editing.
            using (SpreadsheetDocument spreadSheet = SpreadsheetDocument.Open(docName, true))
            {
                // Add a blank WorksheetPart.
                WorksheetPart newWorksheetPart = spreadSheet.WorkbookPart.AddNewPart<WorksheetPart>();
                newWorksheetPart.Worksheet = new Worksheet(new SheetData());

                Sheets sheets = spreadSheet.WorkbookPart.Workbook.GetFirstChild<Sheets>();
                string relationshipId = spreadSheet.WorkbookPart.GetIdOfPart(newWorksheetPart);

                // Get a unique ID for the new worksheet.
                uint sheetId = 1;
                if (sheets.Elements<Sheet>().Count() > 0)
                {
                    sheetId = sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;
                }

                // Give the new worksheet a name.
                string sheetName = "Sheet" + sheetId;

                // Append the new worksheet and associate it with the workbook.
                Sheet sheet = new Sheet() { Id = relationshipId, SheetId = sheetId, Name = sheetName };
                sheets.Append(sheet);
            }
        }


        private static void WriteDataTableToExcelWorksheet(DataTable dt, WorksheetPart worksheetPart,  List<ReportColumnsDto> columns)
        {
            var worksheet = worksheetPart.Worksheet;
            var sheetData = worksheet.GetFirstChild<SheetData>();
 
            string cellValue = "";

            //  Create a Header Row in our Excel file, containing one header for each Column of data in our DataTable.
            //
            //  We'll also create an array, showing which type each column of data is (Text or Numeric), so when we come to write the actual
            //  cells of data, we'll know if to write Text values or Numeric cell values.
            int numberOfColumns = dt.Columns.Count;
 
            bool[] IsNumericColumn = new bool[numberOfColumns];
 
            string[] excelColumnNames = new string[numberOfColumns];
            for (int n = 0; n < numberOfColumns; n++)
            {
                excelColumnNames[n] = GetExcelColumnName(n); 
            }
            //worksheet.AppendChild(sheetCol);

            //
            //  Create the Header row in our Excel Worksheet
            //
            uint rowIndex = 1;

            var headerRow = new Row { RowIndex = rowIndex };  // add a row at the top of spreadsheet
           
            sheetData.Append(headerRow);
             
            for (int colInx = 0; colInx < numberOfColumns; colInx++)
            {
                DataColumn col = dt.Columns[colInx];
                if(columns != null )
                {
                    ReportColumnsDto cItem = columns.Find(x => x.Field == col.ColumnName);
                    AppendHeaderCell(excelColumnNames[colInx] + "1", cItem.Title, headerRow);
                    IsNumericColumn[colInx] = (col.DataType.FullName == "System.Decimal") || (col.DataType.FullName == "System.Int32");
                }
                else
                {
                    AppendHeaderCell(excelColumnNames[colInx] + "1", col.ColumnName, headerRow);
                    IsNumericColumn[colInx] = (col.DataType.FullName == "System.Decimal") || (col.DataType.FullName == "System.Int32");
                } 
            }
            
            //
            //  Now, step through each row of data in our DataTable...
            //
            double cellNumericValue = 0;
            foreach (DataRow dr in dt.Rows)
            {
                // ...create a new row, and append a set of this row's data to it.
                ++rowIndex;
                var newExcelRow = new Row { RowIndex = rowIndex };  // add a row at the top of spreadsheet
                sheetData.Append(newExcelRow);

                for (int colInx = 0; colInx < numberOfColumns; colInx++)
                {
                    cellValue = dr.ItemArray[colInx].ToString();

                    // Create cell with data
                    if (IsNumericColumn[colInx])
                    {
                        //  For numeric cells, make sure our input data IS a number, then write it out to the Excel file.
                        //  If this numeric value is NULL, then don't write anything to the Excel file.
                        cellNumericValue = 0;
                        if (double.TryParse(cellValue, out cellNumericValue))
                        {
                            cellValue = cellNumericValue.ToString();
                            AppendNumericCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow);
                        }
                    }
                    else
                    {
                        //  For text cells, just write the input data straight out to the Excel file.
                        AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow);
                    }
                }
            }

        }

        private static Columns AutoSize(SheetData sheetData)
        {
            var maxColWidth = GetMaxCharacterWidth(sheetData);

            Columns columns = new Columns();
            //this is the width of my font - yours may be different
            double maxWidth = 7;
            foreach (var item in maxColWidth)
            {
                //width = Truncate([{Number of Characters} * {Maximum Digit Width} + {5 pixel padding}]/{Maximum Digit Width}*256)/256
                double width = Math.Truncate((item.Value * maxWidth + 5) / maxWidth * 256) / 256;

                //pixels=Truncate(((256 * {width} + Truncate(128/{Maximum Digit Width}))/256)*{Maximum Digit Width})
                double pixels = Math.Truncate(((256 * width + Math.Truncate(128 / maxWidth)) / 256) * maxWidth);

                //character width=Truncate(({pixels}-5)/{Maximum Digit Width} * 100+0.5)/100
                double charWidth = Math.Truncate((pixels - 5) / maxWidth * 100 + 0.5) / 100;

                Column col = new Column() { BestFit = true, Min = (UInt32)(item.Key + 1), Max = (UInt32)(item.Key + 1), CustomWidth = true, Width = (DoubleValue)width };
                columns.Append(col);
            }

            return columns;
        }

        private static Dictionary<int, int> GetMaxCharacterWidth(SheetData sheetData)
        {
            //iterate over all cells getting a max char value for each column
            Dictionary<int, int> maxColWidth = new Dictionary<int, int>();
            var rows = sheetData.Elements<Row>();
            UInt32[] numberStyles = new UInt32[] { 5, 6, 7, 8 }; //styles that will add extra chars
            UInt32[] boldStyles = new UInt32[] { 1, 2, 3, 4, 6, 7, 8 }; //styles that will bold
            foreach (var r in rows)
            {
                var cells = r.Elements<Cell>().ToArray();

                //using cell index as my column
                for (int i = 0; i < cells.Length; i++)
                {
                    var cell = cells[i];
                    var cellValue = cell.CellValue == null ? string.Empty : cell.CellValue.InnerText;
                    var cellTextLength = cellValue.Length;

                    if (cell.StyleIndex != null && numberStyles.Contains(cell.StyleIndex))
                    {
                        int thousandCount = (int)Math.Truncate((double)cellTextLength / 4);

                        //add 3 for '.00' 
                        cellTextLength += (3 + thousandCount);
                    }

                    if (cell.StyleIndex != null && boldStyles.Contains(cell.StyleIndex))
                    {
                        //add an extra char for bold - not 100% acurate but good enough for what i need.
                        cellTextLength += 1;
                    }

                    if (maxColWidth.ContainsKey(i))
                    {
                        var current = maxColWidth[i];
                        if (cellTextLength > current)
                        {
                            maxColWidth[i] = cellTextLength;
                        }
                    }
                    else
                    {
                        maxColWidth.Add(i, cellTextLength);
                    }
                }
            }

            return maxColWidth;
        }


        public static DataTable ListToDataTable<T>(List<T> list)
        {
            return ListToDataTable<T>(list, null);
        }

        public static DataTable ListToDataTable<T>(List<T> list, List<ReportColumnsDto> columns)
        {
            DataTable dt = new DataTable();

            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                if(columns != null)
                {
                    if (columns.Exists(x => x.Field == info.Name))
                    {
                        ReportColumnsDto c = columns.Find(x => x.Field == info.Name);
                        dt.Columns.Add(new DataColumn(c.Field, GetNullableType(info.PropertyType)));
                    }
                }
                else
                {
                    dt.Columns.Add(new DataColumn(info.Name, GetNullableType(info.PropertyType)));
                }
                
            }
         
            foreach (T t in list)
            {
                DataRow row = dt.NewRow();
                if( columns != null)
                {
                    foreach (PropertyInfo info in typeof(T).GetProperties())
                    {
                        if (columns != null && columns.Exists(x => x.Field == info.Name))
                        {
                            if (!IsNullableType(info.PropertyType))
                                row[info.Name] = info.GetValue(t, null);
                            else
                                row[info.Name] = (info.GetValue(t, null) ?? DBNull.Value);
                        }
                    }
                }
                else
                {
                    foreach (PropertyInfo info in typeof(T).GetProperties())
                    { 
                        if (!IsNullableType(info.PropertyType))
                            row[info.Name] = info.GetValue(t, null);
                        else
                            row[info.Name] = (info.GetValue(t, null) ?? DBNull.Value);
                    }
                }
               
                dt.Rows.Add(row);
            }
            return dt;
        }

        private static Type GetNullableType(Type t)
        {
            Type returnType = t;
            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                returnType = Nullable.GetUnderlyingType(t);
            }
            return returnType;
        }

        private static bool IsNullableType(Type type)
        {
            return (type == typeof(string) ||
                    type.IsArray ||
                    (type.IsGenericType &&
                     type.GetGenericTypeDefinition().Equals(typeof(Nullable<>))));
        }

        private static void AppendHeaderCell(string cellReference, string cellStringValue, Row excelRow)
        {
            //  Add a new Excel Cell to our Row 
            Cell cell = new Cell() { CellReference = cellReference, DataType = CellValues.String };
            CellValue cellValue = new CellValue();
            cellValue.Text = cellStringValue;
            cell.StyleIndex = 2;
            cell.Append(cellValue);
            excelRow.Append(cell);
        }

        private static void AppendTextCell(string cellReference, string cellStringValue, Row excelRow)
        {
            //  Add a new Excel Cell to our Row 
            Cell cell = new Cell() { CellReference = cellReference, DataType = CellValues.String };
            CellValue cellValue = new CellValue();
            cellValue.Text = cellStringValue;
            cell.StyleIndex = 1;
            cell.Append(cellValue);
            excelRow.Append(cell);
        }

        private static void AppendNumericCell(string cellReference, string cellStringValue, Row excelRow)
        {
            //  Add a new Excel Cell to our Row 
            Cell cell = new Cell() { CellReference = cellReference };
            CellValue cellValue = new CellValue();
            cellValue.Text = cellStringValue;
            cell.StyleIndex = 1;
            cell.Append(cellValue);
            excelRow.Append(cell);
        }

        private static string GetExcelColumnName(int columnIndex)
        {
            //  Convert a zero-based column index into an Excel column reference  (A, B, C.. Y, Y, AA, AB, AC... AY, AZ, B1, B2..)
            //
            //  eg  GetExcelColumnName(0) should return "A"
            //      GetExcelColumnName(1) should return "B"
            //      GetExcelColumnName(25) should return "Z"
            //      GetExcelColumnName(26) should return "AA"
            //      GetExcelColumnName(27) should return "AB"
            //      ..etc..
            //
            if (columnIndex < 26)
                return ((char)('A' + columnIndex)).ToString();

            char firstChar = (char)('A' + (columnIndex / 26) - 1);
            char secondChar = (char)('A' + (columnIndex % 26));

            return string.Format("{0}{1}", firstChar, secondChar);
        }

        private static Stylesheet GenerateStylesheet()
        {
            Stylesheet styleSheet = null;

            Fonts fonts = new Fonts(
                new Font( // Index 0 - default
                    new FontSize() { Val = 10 }

                ),
                new Font( // Index 1 - header
                    new FontSize() { Val = 10 },
                    new Bold(),
                    new Color() { Rgb = "FFFFFF" }

                ));

            Fills fills = new Fills(
                    new Fill(new PatternFill() { PatternType = PatternValues.None }), // Index 0 - default
                    new Fill(new PatternFill() { PatternType = PatternValues.Gray0625}), // Index 1 - default
                    new Fill(new PatternFill(new ForegroundColor { Rgb = new HexBinaryValue() { Value = "66666666" } })
                    { PatternType = PatternValues.Solid }) // Index 2 - header
                );

            Borders borders = new Borders(
                    new Border(), // index 0 default
                    new Border( // index 1 black border
                        new LeftBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new RightBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new TopBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new BottomBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new DiagonalBorder())
                );

            CellFormats cellFormats = new CellFormats(
                    new CellFormat(), // default
                    new CellFormat { FontId = 0, FillId = 0, BorderId = 1, ApplyBorder = true }, // body
                    new CellFormat { FontId = 1, FillId = 2, BorderId = 1, ApplyFill = true } // header
                );

            styleSheet = new Stylesheet(fonts, fills, borders, cellFormats);

            return styleSheet;
        }

        public void Dispose()
        {
            //자원해제        
            
        }
    }
}
