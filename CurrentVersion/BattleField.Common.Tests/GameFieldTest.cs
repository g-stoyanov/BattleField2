using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleField.Common.Tests
{
    [TestClass]
    public class GameFieldTest
    {
        [TestMethod]
        public void TestGameFieldConstructor()
        {
            FieldCell fieldCell = new FieldCell();
            byte fieldSize = 3;
            GameField gameField = new GameField(fieldSize);
            Assert.AreEqual(gameField.FieldSize, fieldSize);
        }

        [TestMethod]
        public void TestCountRemainingMines()
        {
            IUserInterface userInterface = new KeyboardInterface();
            IRenderer renderer = new ConsoleRenderer();
            GameField gameField = new GameField(3);
            GameEngine gameEngine = new GameEngine(userInterface, renderer, gameField);
            gameEngine.InitializeField();
            Assert.IsTrue(gameField.CountRemainingMines() == 0);
        }

        [TestMethod]
        public void TestFieldCellIndexator()
        {
            IUserInterface userInterface = new KeyboardInterface();
            IRenderer renderer = new ConsoleRenderer();
            GameField gameField = new GameField(3);
            GameEngine gameEngine = new GameEngine(userInterface, renderer, gameField);
            gameEngine.InitializeField();
            FieldCell fieldCell = new FieldCell();
            Assert.AreEqual(gameField[0, 0].ToString(), fieldCell.ToString());
        }
    }
}