namespace BattleFields
{
    using System;

    public class BattleField
    {
        private const string ExplodedSign = " X ";

        public BattleField()
        {
        }

        

        public void DisplayField()
        {
            // top side numbers
            Console.Write("   ");
            for (int i = 0; i < fieldSize; i++)
            {
                Console.Write(" " + i.ToString() + "  ");
            }

            Console.WriteLine(string.Empty);

            Console.Write("    ");
            for (int i = 0; i < (4 * fieldSize) - 3; i++)
            {
                Console.Write("-");
            }

            Console.WriteLine(string.Empty);

            // top side numbers
            Console.WriteLine(string.Empty);

            for (int i = 0; i < fieldSize; i++)
            {
                // left side numbers
                Console.Write(i.ToString() + "|");

                for (int j = 0; j < fieldSize; j++)
                {
                    Console.Write(" " + this.positions[i, j].ToString());
                }

                for (int k = 0; k < 3; k++)
                {
                    Console.WriteLine(string.Empty);
                }
            }
        }

        

        

        

        private static void Main()
        {
            string tempFieldSize;
            Console.WriteLine("Welcome to the Battle Field game");
            do
            {
                Console.Write("Enter proper size of board (1-10): ");
                tempFieldSize = Console.ReadLine();
            }
            while ((!int.TryParse(tempFieldSize, out fieldSize)) || (fieldSize < 1) || (fieldSize > 10));

            BattleField gameField = new BattleField();
            gameField.InitField();
            gameField.InitMines();
            gameField.DisplayField();

            string coordinates;
            int positionByX, positionByY;

            do
            {
                do
                {
                    Console.Write("Enter coordinates: ");
                    coordinates = Console.ReadLine();
                    positionByX = Convert.ToInt32(coordinates.Substring(0, 1));
                    positionByY = Convert.ToInt32(coordinates.Substring(2));

                    if ((positionByX < 0) || (positionByY > fieldSize - 1) || (gameField.positions[positionByX, positionByY] == " - "))
                    {
                        Console.WriteLine("Invalid Move");
                    }
                }
                while ((positionByX < 0) || (positionByY > fieldSize - 1) || (gameField.positions[positionByX, positionByY] == " - "));

                gameField.DetonateMine(positionByX, positionByY);
                gameField.DisplayField();
                gameField.detonatedMines++;
            }
            while (gameField.CountRemainingMines() != 0);

            Console.WriteLine("Game Over. Detonated Mines: " + gameField.detonatedMines);
            Console.ReadKey();
        }

        
    }
}