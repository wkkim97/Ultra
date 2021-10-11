using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bayer.Ultra.BSL.Excel.Dao;
using System.Collections.Generic;
using Bayer.Ultra.Framework.Common.Dto.Approval;

namespace Bayer.Ultra.BSL.Excel.Test
{
    [TestClass]
    public class SRMReaderTest
    {
        [TestMethod]
        public void TestReadPaymentSRM()
        {
            SRMReader reader = new SRMReader();
            List<DTO_PAYMENT_UPLOAD_SRM> list = reader.ReadPaymentSRM("P000000001", "BKKWK", @"C:\Temp\Read_SRM_Report.xlsx");
        }
    }
}
