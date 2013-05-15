namespace BattleField.Common
{
    using System;
    using System.Linq;

    public class ConsoleRenderer : IRenderer
    {
        // Please I want to code this class!!! I have some ideas to implement here!!!
        // Joro, 10x

        public void Render(GameField gameField, byte detonatedMines)
        {
            // top side numbers
            Console.Write("   ");
            for (int i = 0; i < gameField.FieldSize; i++)
            {
                Console.Write(" " + i.ToString() + "  ");
            }

            Console.WriteLine(string.Empty);

            Console.Write("    ");
            for (int i = 0; i < (4 * gameField.FieldSize) - 3; i++)
            {
                Console.Write("-");
            }

            Console.WriteLine(string.Empty);

            // top side numbers
            Console.WriteLine(string.Empty);

            for (int i = 0; i < gameField.FieldSize; i++)
            {
                // left side numbers
                Console.Write(i.ToString() + "|");

                for (int j = 0; j < gameField.FieldSize; j++)
                {
                    Console.Write(" " + gameField[i, j].ToString());
                }

                for (int k = 0; k < 3; k++)
                {
                    Console.WriteLine(string.Empty);
                }
            }
        }
    }
}
