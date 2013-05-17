using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleField.Common.Tests
{
    [TestClass]
    public class ConsoleRendererTest
    {
        [TestMethod]
        public void TestRender()
        {
            IUserInterface userInterface = new KeyboardInterface();
            IRenderer renderer = new ConsoleRenderer();
            GameField gameField = new GameField(3);
            GameEngine gameEngine = new GameEngine(userInterface, renderer, gameField);
            
            string message = string.Empty;
            string menu = "R eset, N ew, Q uit";
            byte detonatedMines = 0;
            string expected = gameEngine.Render();
            string actual = renderer.Render(gameField, detonatedMines, menu, message);

            Assert.AreEqual(expected, actual);
        }
    }
}