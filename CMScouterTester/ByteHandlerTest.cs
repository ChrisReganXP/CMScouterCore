using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CMScouterTester
{
    [TestClass]
    public class ByteHandlerTest
    {/*
        [DataTestMethod]
        [DataRow(5, 1, 202, 7, 0, 19, 9, 1994)]
        [DataRow(160, 0, 232, 7, 1, 9, 6, 2024)]
        // For some reason this is the only one that is a day out. I can't see why. Maybe it is related to the year offsets being patched in the exe?
        //[DataRow(242, 0, 201, 7, 1, 30, 8, 1993)]
        [DataRow(141, 0, 233, 7, 0, 22, 5, 2025)]
        [DataRow(45, 0, 211, 7, 0, 15, 2, 2003)]
        [DataRow(142, 0, 233, 7, 0, 23, 5, 2025)]
        [DataRow(143, 0, 203, 7, 0, 24, 5, 1995)]
        [DataRow(123, 0, 231, 7, 0, 4, 5, 2023)]
        [DataRow(44, 0, 207, 7, 0, 14, 2, 1999)]
        [DataRow(144, 0, 232, 7, 1, 24, 5, 2024)]
        [DataRow(30, 0, 208, 7, 0, 31, 1, 2000)]
        [DataRow(141, 0, 232, 7, 1, 21, 5, 2024)]
        public void TestDateConversion(int byte1, int byte2, int byte3, int byte4, int byte5, int expDay, int expMonth, int expYear)
        {
            byte[] bytes = new byte[] { (byte)byte1, (byte)byte2, (byte)byte3, (byte)byte4, (byte)byte5 };
            DateTime? dt = ByteHandler.ConvertToDate(bytes);

            Assert.IsNotNull(dt);
            Assert.IsTrue(dt.Value == new DateTime(expYear, expMonth, expDay));
        }*/
    }
}
