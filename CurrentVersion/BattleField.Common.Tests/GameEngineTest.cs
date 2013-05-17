using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleField.Common.Tests
{
    [TestClass]
    public class GameEngineTest
    {
        [TestMethod]
        public void TestGameEngine1()
        {
            IUserInterface userInterface = new KeyboardInterface();
            IRenderer renderer = new ConsoleRenderer();
            GameField gameField = new GameField(3);
            GameEngine gameEngine = new GameEngine(userInterface, renderer, gameField);
            gameEngine.InitializeField();
            gameEngine.InitializeMines();
            Assert.IsTrue(gameEngine.GetRemainingMines() > 0);
        }
    }
}
