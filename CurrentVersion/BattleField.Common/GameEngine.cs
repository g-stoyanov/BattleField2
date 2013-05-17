namespace BattleField.Common
{
    using System;
    using System.Linq;

    public class GameEngine
    {
        private int xCoordinate = -1;
        private int yCoordinate = -1;
        private char command = '0';
        private string message = string.Empty;
        private string menu = "R eset, N ew, Q uit";
        private byte detonatedMines = 0;
        private readonly GameField gameField;
        private readonly IUserInterface userInterface;
        private readonly IRenderer renderer;

        public GameEngine(IUserInterface userInterface, IRenderer renderer, GameField gameField)
        {
            this.userInterface = userInterface;
            this.renderer = renderer;
            this.gameField = gameField;
        }

        public void InitializeField()
        {
            detonatedMines = 0;
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

        public int GetRemainingMines()
        {
            int countMines = 0;
            for (int row = 0; row < gameField.FieldSize; row++)
            {
                for (int col = 0; col < gameField.FieldSize; col++)
                {
                    if (gameField[row, col].IsMine && !gameField[row, col].IsExploded)
                    {
                        countMines++;
                    }
                }
            }

            if (countMines == 0)
            {
                this.message = string.Format("Game over your points is {0}", detonatedMines);
            }
            else
            {
                this.message = string.Format("Remaining mines {0}", countMines);
            }

            return countMines;
        }

        public string Render()
        {
            return this.renderer.Render(this.gameField, this.detonatedMines, this.menu, this.message);
        }

        public char GetCommand()
        {
            if (GetRemainingMines() == 0)
            {
                this.message = string.Format("Game over your points is {0}", detonatedMines);

                return 'D';
            }

            this.userInterface.GetCommand(out this.xCoordinate, out this.yCoordinate, out this.command);
            if (this.command == '0' && this.xCoordinate == -1 && this.yCoordinate == -1)
            {
                this.message = "Invalid Command!";
            }
            else if (this.command != '0')
            {
                this.message = string.Empty;
            }
            else if (!this.gameField[this.xCoordinate, this.yCoordinate].IsMine)
            {
                this.message = string.Format("Cell {0} - {1} is not mine!", this.xCoordinate, this.yCoordinate);
            }
            else if (this.gameField[this.xCoordinate, this.yCoordinate].IsExploded)
            {
                this.message = string.Format("Cell {0} - {1} is already exploded!", this.xCoordinate, this.yCoordinate);
            }
            else if (
                this.xCoordinate > -1 &&
                this.xCoordinate < this.gameField.FieldSize &&
                this.yCoordinate > -1 &&
                this.yCoordinate < this.gameField.FieldSize)
            {
                return 'D';
            }

            return this.command;
        }

        /// <summary>
        /// Detonates the mine.
        /// Method for detonate selected field with corresponding mine power.
        /// </summary>
        /// <param name="this.xCoordinate">The X coordinate of mine field.</param>
        /// <param name="this.yCoordinate">The Y coordinate of mine field.</param>
        public bool DetonateMine()
        {
            if ((this.CheckCoord(this.xCoordinate) && this.CheckCoord(this.yCoordinate) && !gameField[this.xCoordinate, this.yCoordinate].IsMine) || gameField[this.xCoordinate, this.yCoordinate].IsExploded)
            {
                return false;
            }

            this.detonatedMines++;

            // Take power of mine
            byte power = this.gameField[this.xCoordinate, this.yCoordinate].Power;

            // Explode mine with power 1
            this.Explode(this.xCoordinate, this.yCoordinate);
            this.Explode(this.xCoordinate - 1, this.yCoordinate - 1);
            this.Explode(this.xCoordinate - 1, this.yCoordinate + 1);
            this.Explode(this.xCoordinate + 1, this.yCoordinate - 1);
            this.Explode(this.xCoordinate + 1, this.yCoordinate + 1);

            if (power == 1)
            {
                return true;
            }

            // Explode mine with power 2... (1 + 2)
            this.Explode(this.xCoordinate, this.yCoordinate - 1);
            this.Explode(this.xCoordinate - 1, this.yCoordinate);
            this.Explode(this.xCoordinate + 1, this.yCoordinate);
            this.Explode(this.xCoordinate, this.yCoordinate + 1);

            if (power == 2)
            {
                return true;
            }

            // Explode mine with power 3... (1 + 2 + 3)
            this.Explode(this.xCoordinate - 2, this.yCoordinate);
            this.Explode(this.xCoordinate + 2, this.yCoordinate);
            this.Explode(this.xCoordinate, this.yCoordinate - 2);
            this.Explode(this.xCoordinate, this.yCoordinate + 2);

            if (power == 3)
            {
                return true;
            }

            // Explode mine with power 4... (1 + 2 + 3 + 4)
            this.Explode(this.xCoordinate - 1, this.yCoordinate + 2);
            this.Explode(this.xCoordinate + 1, this.yCoordinate + 2);
            this.Explode(this.xCoordinate - 1, this.yCoordinate - 2);
            this.Explode(this.xCoordinate + 1, this.yCoordinate - 2);
            this.Explode(this.xCoordinate - 2, this.yCoordinate - 1);
            this.Explode(this.xCoordinate - 2, this.yCoordinate + 1);
            this.Explode(this.xCoordinate + 2, this.yCoordinate - 1);
            this.Explode(this.xCoordinate + 2, this.yCoordinate + 1);

            if (power == 4)
            {
                return true;
            }

            // Explode mine with power 5... (1 + 2 + 3 + 4 + 5)
            this.Explode(this.xCoordinate - 2, this.yCoordinate - 2);
            this.Explode(this.xCoordinate + 2, this.yCoordinate - 2);
            this.Explode(this.xCoordinate - 2, this.yCoordinate + 2);
            this.Explode(this.xCoordinate + 2, this.yCoordinate + 2);

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
        /// <param name="this.xCoordinate">The X coordinate of the cell.</param>
        /// <param name="this.yCoordinate">The Y coordinate of the cell.</param>
        private void Explode(int xCoordinate, int yCoordinate)
        {
            if (this.CheckCoord(xCoordinate) && this.CheckCoord(yCoordinate))
            {
                this.gameField[xCoordinate, yCoordinate].IsExploded = true;
            }
        }
    }
}