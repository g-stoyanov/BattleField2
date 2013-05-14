namespace BattleFields
{
    using System;

    public class BattleField
    {
        public const string ExplodedSign = " X ";
        public static int fieldSize = 0;
        public int detonatedMines = 0;
        public string[,] positions = new string[fieldSize, fieldSize];

        public BattleField()
        {
        }

        public void InitField()
        {
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    this.positions[i, j] = " - ";
                }
            }
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
            for (int i = 0; i < 4 * fieldSize - 3; i++)
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

        public void InitMines()
        { 
            // tuka ne sym siguren kakvo tochno pravq ama pyk raboti
            int minesDownLimit = Convert.ToInt32(0.15 * fieldSize * fieldSize);
            int minesUpperLimit = Convert.ToInt32(0.30 * fieldSize * fieldSize);
            int tempMineXCoordinate;
            int tempMineYCoordinate;
            bool flag = true;
            Random rnd = new Random();

            int minesCount = Convert.ToInt32(rnd.Next(minesDownLimit, minesUpperLimit));
            int[,] minesPositions = new int[minesCount, minesCount];
            Console.WriteLine("mines count is: " + minesCount);

            for (int i = 0; i < minesCount; i++)
            {
                do
                {
                    // tuka cikyla se vyrti dokato flag ne e false
                    // s do-while raboti po dobre
                    tempMineXCoordinate =
                        Convert.ToInt32(rnd.Next(0, fieldSize - 1));
                    tempMineYCoordinate =
                        Convert.ToInt32(rnd.Next(0, fieldSize - 1));
                    if (this.positions[tempMineXCoordinate, tempMineYCoordinate] == " - ")
                    {
                        this.positions[tempMineXCoordinate, tempMineYCoordinate] =
                                                                                " " + Convert.ToString(rnd.Next(1, 6) + " ");
                    }
                    else
                    {
                        flag = false;
                    }
                }
                while (flag);
            }
        }

        /// <summary>
        /// Detonates the mine.
        /// Method for detonate selected field with corresponding mine power.
        /// </summary>
        /// <param name="positionByX">The X coordinate of mine field.</param>
        /// <param name="positionByY">The Y coordinate of mine field.</param>
        public void DetonateMine(int positionByX, int positionByY)
        {
            // Take power of mine
            byte power = Convert.ToByte(this.positions[positionByX, positionByY]);

            // Explode mine with power 1
            Explode(positionByX, positionByY);
            Explode(positionByX - 1, positionByY - 1);
            Explode(positionByX - 1, positionByY + 1);
            Explode(positionByX + 1, positionByY - 1);
            Explode(positionByX + 1, positionByY + 1);

            if (power == 1)
            {
                return;
            }

            // Explode mine with power 2... (1 + 2)
            Explode(positionByX, positionByY - 1);
            Explode(positionByX - 1, positionByY);
            Explode(positionByX + 1, positionByY);
            Explode(positionByX, positionByY + 1);

            if (power == 2)
            {
                return;
            }

            // Explode mine with power 3... (1 + 2 + 3)
            Explode(positionByX - 2, positionByY);
            Explode(positionByX + 2, positionByY);
            Explode(positionByX, positionByY - 2);
            Explode(positionByX, positionByY + 2);

            if (power == 3)
            {
                return;
            }

            // Explode mine with power 4... (1 + 2 + 3 + 4)
            Explode(positionByX - 1, positionByY + 2);
            Explode(positionByX + 1, positionByY + 2);
            Explode(positionByX - 1, positionByY - 2);
            Explode(positionByX + 1, positionByY - 2);
            Explode(positionByX - 2, positionByY - 1);
            Explode(positionByX - 2, positionByY + 1);
            Explode(positionByX + 2, positionByY - 1);
            Explode(positionByX + 2, positionByY + 1);

            if (power == 4)
            {
                return;
            }

            // Explode mine with power 5... (1 + 2 + 3 + 4 + 5)
            Explode(positionByX - 2, positionByY - 2);
            Explode(positionByX + 2, positionByY - 2);
            Explode(positionByX - 2, positionByY + 2);
            Explode(positionByX + 2, positionByY + 2);
        }

        public int PrebroiOstavashtiteMinichki()
        {
            int count = 0;

            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; i < fieldSize; i++)
                {
                    if ((this.positions[i, j] != ExplodedSign) && (this.positions[i, j] != " - "))
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// Checks whether the coordinate is within the field of play.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns>Returns a boolean value as a result of the check.</returns>
        private bool CheckCoord(int coordinate)
        {
            bool result = false;
            if (coordinate >= 0 && coordinate < fieldSize)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Explodes the specified cell with given coordinates.
        /// </summary>
        /// <param name="positionByX">The X coordinate of the cell.</param>
        /// <param name="positionByY">The Y coordinate of the cell.</param>
        private void Explode(int positionByX, int positionByY)
        {
            if (CheckCoord(positionByX) && CheckCoord(positionByY))
            {
                this.positions[positionByX, positionByY] = ExplodedSign;
            }
        }

        private static void Main()
        {
            string tempFieldSize;
            Console.WriteLine("Welcome to the Battle Field game");
            do
            {
                Console.Write("Enter legal size of board: ");
                tempFieldSize = Console.ReadLine();
            }
            while ((!Int32.TryParse(tempFieldSize, out fieldSize)) || (fieldSize < 0) || (fieldSize > 11));

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
            while (gameField.PrebroiOstavashtiteMinichki() != 0);

            Console.WriteLine("Game Over. Detonated Mines: " + gameField.detonatedMines);
            Console.ReadKey();
        }
    }
}
