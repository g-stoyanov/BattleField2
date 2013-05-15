namespace BattleField.Common
{
    using System;
    using System.Linq;

    public class GameEngine
    {
        private string menu = "R eset, N ew, Q uit";
        private string message = "Game over! Total detonated mines is 100!";
        private byte detonatedMines;
        private GameField gameField;
        private IUserInterface userInterface;
        private IRenderer renderer;

        public GameEngine(IUserInterface userInterface, IRenderer renderer, GameField gameField)
        {
            this.userInterface = userInterface;
            this.renderer = renderer;
            this.gameField = gameField;
        }

        public void InitializeField()
        {
            for (int i = 0; i < this.gameField.FieldSize; i++)
            {
                for (int j = 0; j < this.gameField.FieldSize; j++)
                {
                    this.gameField[i, j] = new FieldCell();
                }
            }
        }

        public void InitializeMines()
        {
            // The number mines should be between 15% and 30% of the cells in the battle field
            int numberOfCells = this.gameField.FieldSize * this.gameField.FieldSize;
            int minesDownLimit = Convert.ToInt32(0.15 * numberOfCells);
            int minesUpperLimit = Convert.ToInt32(0.30 * numberOfCells);
            Random randomNumberGenerator = new Random();
            int minesCount = randomNumberGenerator.Next(minesDownLimit, minesUpperLimit);

            int tempMineXCoordinate;
            int tempMineYCoordinate;
            for (int i = 0; i < minesCount; i++)
            {
                bool mineNotPlaced = true;
                do
                {
                    tempMineXCoordinate = randomNumberGenerator.Next(0, this.gameField.FieldSize - 1);
                    tempMineYCoordinate = randomNumberGenerator.Next(0, this.gameField.FieldSize - 1);
                    if (!this.gameField[tempMineXCoordinate, tempMineYCoordinate].IsMine)
                    {
                        this.gameField[tempMineXCoordinate, tempMineYCoordinate].Power = (byte)randomNumberGenerator.Next(1, 6);
                        this.gameField[tempMineXCoordinate, tempMineYCoordinate].IsMine = true;
                        mineNotPlaced = false;
                    }                     
                }
                while (mineNotPlaced);
            }
        }

        public void Render()
        {
            this.renderer.Render(this.gameField, this.detonatedMines, menu, message);
        }

        /// <summary>
        /// Detonates the mine.
        /// Method for detonate selected field with corresponding mine power.
        /// </summary>
        /// <param name="positionByX">The X coordinate of mine field.</param>
        /// <param name="positionByY">The Y coordinate of mine field.</param>
        public bool DetonateMine(int positionByX, int positionByY)
        {
            if (this.CheckCoord(positionByX) && this.CheckCoord(positionByY) && !gameField[positionByX, positionByY].IsMine)
            {
                return false;
            }

            // Take power of mine
            byte power = Convert.ToByte(this.gameField[positionByX, positionByY]);

            // Explode mine with power 1
            this.Explode(positionByX, positionByY);
            this.Explode(positionByX - 1, positionByY - 1);
            this.Explode(positionByX - 1, positionByY + 1);
            this.Explode(positionByX + 1, positionByY - 1);
            this.Explode(positionByX + 1, positionByY + 1);

            if (power == 1)
            {
                return true;
            }

            // Explode mine with power 2... (1 + 2)
            this.Explode(positionByX, positionByY - 1);
            this.Explode(positionByX - 1, positionByY);
            this.Explode(positionByX + 1, positionByY);
            this.Explode(positionByX, positionByY + 1);

            if (power == 2)
            {
                return true;
            }

            // Explode mine with power 3... (1 + 2 + 3)
            this.Explode(positionByX - 2, positionByY);
            this.Explode(positionByX + 2, positionByY);
            this.Explode(positionByX, positionByY - 2);
            this.Explode(positionByX, positionByY + 2);

            if (power == 3)
            {
                return true;
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
                return true;
            }

            // Explode mine with power 5... (1 + 2 + 3 + 4 + 5)
            this.Explode(positionByX - 2, positionByY - 2);
            this.Explode(positionByX + 2, positionByY - 2);
            this.Explode(positionByX - 2, positionByY + 2);
            this.Explode(positionByX + 2, positionByY + 2);

            return true;
        }

        /// <summary>
        /// Checks whether the coordinate is within the field of play.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns>Returns a boolean value as a result of the check.</returns>
        private bool CheckCoord(int coordinate)
        {
            bool result = false;
            if (coordinate >= 0 && coordinate < this.gameField.FieldSize)
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
                this.gameField[positionByX, positionByY].IsExploded = true;
                this.detonatedMines++;
            }
        }
    }
}