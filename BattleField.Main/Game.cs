namespace BattleField.Common
{
    using System;

    public class Game
    {
        private static void Main()
        {
            bool exit = false;
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

            while (true)
            {
                gameEngine.Render();
                char command = gameEngine.GetCommand();

                switch (command)
                {
                    case 'D':
                        {
                            gameEngine.DetonateMine();
                        }
                        break;

                    case 'R':
                        {
                            gameEngine.InitializeField();
                            gameEngine.InitializeMines();
                        }
                        break;

                    case 'Q':
                        {
                            exit = true;
                        }
                        break;

                    case 'N':
                        {
                            Main();
                        }
                        break;
                }

                if (exit)
                {
                    break;
                }
            }
        }
    }
}
