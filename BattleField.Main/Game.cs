namespace BattleField.Common
{
    using System;

    public class Game
    {
        private static void Main()
        {        
            byte fieldSize;
            string tempFieldSize;
            Console.WriteLine("Welcome to the Battle Field game");
            do
            {
                Console.Write("Enter proper size of board (1-10): ");
                tempFieldSize = Console.ReadLine();
            }
            while ((!byte.TryParse(tempFieldSize, out fieldSize)) || (fieldSize < 1) || (fieldSize > 10));

            IUserInterface userInterface = new KeyboardInterface();
            IRenderer renderer = new ConsoleRenderer();
            GameField gameField = new GameField(fieldSize);
            GameEngine gameEngine = new GameEngine(userInterface, renderer, gameField);

            gameEngine.InitializeField();
            gameEngine.InitializeMines();           
            gameEngine.Render();

            string coordinates;
            int positionByX, positionByY;

            //do
            //{
            //    do
            //    {
            //        Console.Write("Enter coordinates: ");
            //        coordinates = Console.ReadLine();
            //        positionByX = Convert.ToInt32(coordinates.Substring(0, 1));
            //        positionByY = Convert.ToInt32(coordinates.Substring(2));

            //        if ((positionByX < 0) || (positionByY > fieldSize - 1) || (gameField.positions[positionByX, positionByY] == " - "))
            //        {
            //            Console.WriteLine("Invalid Move");
            //        }
            //    }
            //    while ((positionByX < 0) || (positionByY > fieldSize - 1) || (gameField.positions[positionByX, positionByY] == " - "));

            //    gameField.DetonateMine(positionByX, positionByY);
            //    gameField.DisplayField();
            //    gameField.detonatedMines++;
            //}
            //while (gameField.CountRemainingMines() != 0);

            //Console.WriteLine("Game Over. Detonated Mines: " + gameField.detonatedMines);
            //Console.ReadKey();
        }
    }
}
