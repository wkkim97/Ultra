using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Report
{
    public  class ReportColumnsDto
    {
        public string Title { get; set; }
        public string Field { get; set; } 
        public string CellValue { get; set; } 

        public bool IsNumeric { get; set; }

    }
}
