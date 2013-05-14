namespace ExplodingMines
{
    using System;
    using System.Linq;

    class GameField
    {
        private static int fieldSize = 0;
        private int detonatedMines = 0;
        private readonly FieldCell[,] positions = new FieldCell[fieldSize, fieldSize];

        public void InitField()
        {
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    this.positions[i, j] = new FieldCell();
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
                    if (!this.positions[tempMineXCoordinate, tempMineYCoordinate].IsMine)
                    {
                        this.positions[tempMineXCoordinate, tempMineYCoordinate].Power = (byte)rnd.Next(1, 6);
                    }
                    else
                    {
                        flag = false;
                    }
                }
                while (flag);
            }
        }

        public int CountRemainingMines()
        {
            int count = 0;

            for (int row = 0; row < fieldSize; row++)
            {
                for (int column = 0; column < fieldSize; column++)
                {
                    if ((!this.positions[row, column].IsExploded) && (this.positions[row, column].IsMine))
                    {
                        count++;
                    }
                }
            }

            return count;
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
            this.Explode(positionByX, positionByY);
            this.Explode(positionByX - 1, positionByY - 1);
            this.Explode(positionByX - 1, positionByY + 1);
            this.Explode(positionByX + 1, positionByY - 1);
            this.Explode(positionByX + 1, positionByY + 1);

            if (power == 1)
            {
                return;
            }

            // Explode mine with power 2... (1 + 2)
            this.Explode(positionByX, positionByY - 1);
            this.Explode(positionByX - 1, positionByY);
            this.Explode(positionByX + 1, positionByY);
            this.Explode(positionByX, positionByY + 1);

            if (power == 2)
            {
                return;
            }

            // Explode mine with power 3... (1 + 2 + 3)
            this.Explode(positionByX - 2, positionByY);
            this.Explode(positionByX + 2, positionByY);
            this.Explode(positionByX, positionByY - 2);
            this.Explode(positionByX, positionByY + 2);

            if (power == 3)
            {
                return;
            }

            // Explode mine with power 4... (1 + 2 + 3 + 4)
            this.Explode(positionByX - 1, positionByY + 2);
            this.Explode(positionByX + 1, positionByY + 2);
            this.Explode(positionByX - 1, positionByY - 2);
            this.Explode(positionByX + 1, positionByY - 2);
            this.Explode(positionByX - 2, positionByY - 1);
            this.Explode(positionByX - 2, positionByY + 1);
            this.Explode(positionByX + 2, positionByY - 1);
            this.Explode(positionByX + 2, positionByY + 1);

            if (power == 4)
            {
                return;
            }

            // Explode mine with power 5... (1 + 2 + 3 + 4 + 5)
            this.Explode(positionByX - 2, positionByY - 2);
            this.Explode(positionByX + 2, positionByY - 2);
            this.Explode(positionByX - 2, positionByY + 2);
            this.Explode(positionByX + 2, positionByY + 2);
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
            if (this.CheckCoord(positionByX) && this.CheckCoord(positionByY))
            {
                this.positions[positionByX, positionByY].IsExploded = true;
            }
        }
    }
}
