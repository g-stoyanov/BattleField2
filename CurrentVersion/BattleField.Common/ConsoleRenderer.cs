namespace BattleField.Common
{
    using System;
    using System.Linq;

    public class ConsoleRenderer : IRenderer
    {
        // Please I want to code this class!!! I have some ideas to implement here!!!
        // Joro, 10x

        public void Render(GameField gameField, byte detonatedMines, string menu, string message)
        {
            Console.Title = string.Format("Battle Field Game - field size: {0}", gameField.FieldSize);
            Console.WindowHeight = 35;
            Console.WindowWidth = 60;
            Console.BufferHeight = 35;
            Console.BufferWidth = 60;

            Console.Clear();
            Console.Write(new string(' ', 3));
            for (int i = 0; i < gameField.FieldSize; i++)
            {
                Console.Write("  {0} ", i);
            }

            Console.Write("\n   \u250c");
            for (int i = 0; i < gameField.FieldSize; i++)
            {
                Console.Write("\u2500\u2500\u2500");
                if (i == gameField.FieldSize - 1)
                {
                    Console.Write("\u2510");
                }
                else
                {
                    Console.Write("\u252c");
                }
            }

            Console.WriteLine();
            for (int row = 0; row < gameField.FieldSize * 2 - 1; row++)
            {
                bool rowIsEven = row % 2 == 0;
                if (rowIsEven)
                {
                    Console.Write(" {0} \u2502", row / 2);
                }
                else
                {
                    Console.Write("   \u251c");
                }
                
                for (int col = 0; col < gameField.FieldSize - 1; col++)
                {
                    if (rowIsEven)
                    {
                        Console.Write(" {0} \u2502", gameField[row / 2, col]);
                    }
                    else
                    {
                        Console.Write("\u2500\u2500\u2500\u253c");
                    }
                }

                if (rowIsEven)
                {
                    Console.Write(" {0} ", gameField[row / 2, gameField.FieldSize - 1]);
                }
                else
                {
                    Console.Write("\u2500\u2500\u2500");
                }

                if (rowIsEven)
                {
                    Console.Write("\u2502 {0}\n", row / 2);
                }
                else
                {
                    Console.Write("\u2524\n");
                }
            }

            Console.Write("   \u2514");
            for (int i = 0; i < gameField.FieldSize; i++)
            {
                Console.Write("\u2500\u2500\u2500");
                if (i == gameField.FieldSize - 1)
                {
                    Console.Write("\u2518\n");
                }
                else
                {
                    Console.Write("\u2534");
                }
            }

            Console.Write(new string(' ', 3));
            for (int i = 0; i < gameField.FieldSize; i++)
            {
                Console.Write("  {0} ", i);
            }

            Console.WriteLine("\n\n\nDetonated mines: {0}", detonatedMines);
            Console.WriteLine("\nGame menu: {0}", menu);
            Console.WriteLine("\nGame message: {0}", message);
            Console.Write("\nPlease input command: ");
        }
    }
}
