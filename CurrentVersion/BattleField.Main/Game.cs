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

            Console.Clear();
            Console.WriteLine("Welcome to the Battle Field game");
            do
            {
                Console.Write("Enter proper size of board (2-10): ");
                tempFieldSize = Console.ReadLine();
            }
            while ((!byte.TryParse(tempFieldSize, out fieldSize)) || (fieldSize < 2) || (fieldSize > 10));

            IUserInterface userInterface = new KeyboardInterface();
            IRenderer renderer = new ConsoleRenderer();
            GameField gameField = new GameField(fieldSize);
            GameEngine gameEngine = new GameEngine(userInterface, renderer, gameField);

            gameEngine.InitializeField();
            gameEngine.InitializeMines();

            int remainingMines = int.MaxValue;
            while (true)
            {            
                gameEngine.Render();
                char command = gameEngine.GetCommand();
                if (remainingMines == 0)
                {
                    while (command != 'Q' && command != 'R' && command != 'N')
                    {
                        gameEngine.Render();
                        char.TryParse(Console.ReadLine(), out command);
                    }                  
                }
                
                switch (command)
                {
                    case 'D':
                        {
                            gameEngine.DetonateMine();
                            remainingMines = gameEngine.GetRemainingMines();                       
                        }
                        break;

                    case 'R':
                        {
                            gameEngine.InitializeField();
                            gameEngine.InitializeMines();
                            remainingMines = gameEngine.GetRemainingMines();
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
