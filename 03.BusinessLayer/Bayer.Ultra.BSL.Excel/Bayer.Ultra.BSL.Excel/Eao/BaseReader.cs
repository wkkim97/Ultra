using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Excel.Eao
{
    public class BaseReader : IDisposable
    {
        // Given a worksheet, a column name, and a row index, 
        // gets the cell at the specified column and 
        protected static Cell GetCell(Worksheet worksheet, string columnName, uint rowIndex) //static
        {
            Row row = GetRow(worksheet, rowIndex);

            if (row == null) return null;

            return row.Elements<Cell>().Where(c => string.Compare(c.CellReference.Value, columnName + rowIndex, true) == 0).First();
        }

        protected Cell GetCell(Row row, string columnName)
        {
            
            return row.Elements<Cell>().Where(c => string.Compare(c.CellReference.Value, columnName + row.RowIndex, true) == 0).FirstOrDefault();
        }

        // Given a worksheet and a row index, return the row.
        protected static Row GetRow(Worksheet worksheet, uint rowIndex)  //static
        {
            return worksheet.GetFirstChild<SheetData>().Elements<Row>().Where(r => r.RowIndex == rowIndex).FirstOrDefault();    //.First();
        }

        protected string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;

            if(cell == null)
            {
                return string.Empty;
            }
            else if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
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

        protected DateTime? ConvertToDatetime(string str)
        {
            try
            {
                return Convert.ToDateTime(str);
            }
            catch
            {
                return null;
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

        public double ConvertToDouble(string str)
        {
            try
            {
                return Convert.ToDouble(str);
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
