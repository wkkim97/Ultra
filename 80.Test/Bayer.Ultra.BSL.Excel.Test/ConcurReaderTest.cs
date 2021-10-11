using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bayer.Ultra.BSL.Excel.Eao;

namespace Bayer.Ultra.BSL.Excel.Test
{
    [TestClass]
    public class ConcurReaderTest
    {
        [TestMethod]
        public void TestReadPaymentConcur()
        {
            ConcurReader reader = new ConcurReader();
            reader.ReadPaymentConcur("BKKWK", @"C:\Temp\upload\attach\BKKWK\PaymentConcur\20171203_110830\1.CONCUR - Attendee Details-HCP_0695.xlsx");
        }
    }
}
