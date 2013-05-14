namespace ExplodingMines
{
    using System;
    using System.Linq;

    class FieldCell
    {
        private bool isMine;
        private bool isExploded;
        private byte power;

        public bool IsMine
        {
            get 
            { 
                return isMine; 
            }

            set 
            { 
                isMine = value; 
            }
        }

        public byte Power
        {
            get 
            { 
                return power; 
            }

            set 
            {
                power = value; 
            }
        }

        public bool IsExploded
        {
            get 
            {
                return isExploded; 
            }

            set 
            { 
                isExploded = value; 
            }
        }
        
    }
}
