namespace BattleField.Common
{
    using System;
    using System.Linq;

    public class GameField
    {
        private byte fieldSize;
        private readonly FieldCell[,] gameField;

        public GameField(byte fieldSize)
        {
            this.fieldSize = fieldSize;
            this.gameField = new FieldCell[FieldSize, FieldSize];
        }

        public FieldCell this[int row, int column]
        {
            get
            {
                return this.gameField[row, column];
            }

            set
            {
                this.gameField[row, column] = value;
            }
        }

        public byte FieldSize
        {
            get
            {
                return fieldSize;
            }

            private set
            {
            }
        }

        public int CountRemainingMines()
        {
            int count = 0;

            for (int row = 0; row < fieldSize; row++)
            {
                for (int column = 0; column < fieldSize; column++)
                {
                    if ((!this.gameField[row, column].IsExploded) && (this.gameField[row, column].IsMine))
                    {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}
