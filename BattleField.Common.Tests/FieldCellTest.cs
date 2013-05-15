using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleField.Common.Tests
{
    [TestClass]
    public class FieldCellTest
    {
        [TestMethod]
        public void TestToStringIsExploded()
        {
            FieldCell fieldCell = new FieldCell();
            fieldCell.IsExploded = true;
            Assert.AreEqual(fieldCell.ToString(),"X");
        }

        [TestMethod]
        public void TestToStringIsMine()
        {
            FieldCell fieldCell = new FieldCell();
            fieldCell.IsExploded = false;
            fieldCell.IsMine = true;
            fieldCell.Power = 2;
            Assert.AreEqual(fieldCell.ToString(), "2");
        }

        [TestMethod]
        public void TestToStringEmpty()
        {
            FieldCell fieldCell = new FieldCell();
            fieldCell.IsExploded = false;
            fieldCell.IsMine = false;
            Assert.AreEqual(fieldCell.ToString(), string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentOutOfRangeException))]
        public void TestPowerOutOfRange()
        {
            FieldCell fieldCell = new FieldCell();
            fieldCell.IsExploded = false;
            fieldCell.IsMine = true;
            fieldCell.Power = 6;
        }

        [TestMethod]
        public void TestPowerForNotMine()
        {
            FieldCell fieldCell = new FieldCell();
            fieldCell.IsExploded = false;
            fieldCell.IsMine = false;
            Assert.AreEqual(fieldCell.Power, 0);
        }
    }
}
