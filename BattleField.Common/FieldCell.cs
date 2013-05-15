namespace BattleField.Common
{
    using System;

    public class FieldCell
    {
        private const string ExplodeSign = "X";

        private bool isMine;

        private bool isExploded;

        private byte power;

        public FieldCell()
            : this(false, false, 1)
        {
        }

        public FieldCell(bool isMine)
            : this(isMine, false, 1)
        {
        }

        public FieldCell(bool isMine, bool isExploded)
            : this(isMine, isExploded, 1)
        {
        }

        public FieldCell(bool isMine, bool isExploded, byte power)
        {
            this.IsMine = isMine;
            this.IsExploded = isExploded;
            this.Power = power;
        }

        public bool IsMine
        {
            get
            {
                return this.isMine;
            }

            set
            {
                this.isMine = value;
            }
        }

        public byte Power
        {
            get
            {
                if (this.IsMine)
                {
                    return this.power;
                }
                else
                {
                    return 0;
                }
            }

            set
            {
                if (value >= 1 && value <= 5)
                {
                    this.power = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Power must be between 1 and 5.");
                }
            }
        }

        public bool IsExploded
        {
            get
            {
                return this.isExploded;
            }

            set
            {
                this.isExploded = value;
            }
        }

        public override string ToString()
        {
            if (this.IsExploded)
            {
                return ExplodeSign;
            }
            else if (this.IsMine)
            {
                return this.Power.ToString();
            }
            else
            {
                return new string(' ', 1);
            }
        }
    }
}
