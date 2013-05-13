using System;

namespace BattleFields
{
    class BattleField
    {
        public const string ExplodedSign = " X ";
        public static int fieldSize = 0;
        public int detonatedMines = 0;
        public string[,] pozicii = new string[fieldSize, fieldSize];

        public BattleField()
        {
        }

        private bool CheckCoord(int coord)
        {
            bool result = false;
            if (coord >= 0 && coord < fieldSize)
            {
                result = true;
            }

            return result;
        }

        private void Explode(int XCoord, int YCoord)
        {
            if (CheckCoord(XCoord) && CheckCoord(YCoord))
            {
                this.pozicii[XCoord, YCoord] = ExplodedSign;
            }
        }

        public void InitField()
        {
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    this.pozicii[i, j] = " - ";
                }
            }
        }

        public void DisplayField()
        {
            //top side numbers
            Console.Write("   ");
            for (int i = 0; i < fieldSize; i++)
            {
                Console.Write(" " + i.ToString() + "  ");
            }
            Console.WriteLine("");

            Console.Write("    ");
            for (int i = 0; i < 4 * fieldSize - 3; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("");
            //top side numbers

            Console.WriteLine("");

            for (int i = 0; i < fieldSize; i++)
            {
                //left side numbers
                Console.Write(i.ToString() + "|");
                for (int j = 0; j < fieldSize; j++)
                {
                    Console.Write(" " + this.pozicii[i, j].ToString());
                }
                Console.WriteLine(""); Console.WriteLine(""); Console.WriteLine("");
            }
        }

        public void InitMines()
        { //tuka ne sym siguren kakvo tochno pravq ama pyk raboti
            int minesDownLimit = Convert.ToInt32(0.15 * fieldSize * fieldSize);
            int minesUpperLimit = Convert.ToInt32(0.30 * fieldSize * fieldSize);
            int tempMineXCoordinate;
            int tempMineYCoordinate;
            bool flag = true;
            Random rnd = new Random();

            int minesCount = Convert.ToInt32(rnd.Next(minesDownLimit, minesUpperLimit));
            int[,] minesPositions =
                new int[minesCount, minesCount];
            Console.WriteLine("mines count is: " + minesCount);

            for (int i = 0; i < minesCount; i++)
            {
                do
                {
                    //tuka cikyla se vyrti dokato flag ne e false
                    //s do-while raboti po dobre
                    tempMineXCoordinate =
                        Convert.ToInt32(rnd.Next(0, fieldSize - 1));
                    tempMineYCoordinate =
                        Convert.ToInt32(rnd.Next(0, fieldSize - 1));
                    if (this.pozicii[tempMineXCoordinate, tempMineYCoordinate] == " - ")
                        this.pozicii[tempMineXCoordinate, tempMineYCoordinate] =
                                                                                " " + Convert.ToString(rnd.Next(1, 6) + " ");
                    else
                        flag = false;
                }
                while (flag);
            }
        }

        //tuka sa mogyshtite metodi za gyrmejite

        public void DetonateMine(int XCoord, int YCoord, byte power)
        {
            // Explode mine with power 1
            Explode(XCoord, YCoord);
            Explode(XCoord - 1, YCoord - 1);
            Explode(XCoord - 1, YCoord + 1);
            Explode(XCoord + 1, YCoord - 1);
            Explode(XCoord + 1, YCoord + 1);
            if (power == 1)
            {
                return;
            }

            // Explode mine with power 2... (1 + 2)
            Explode(XCoord, YCoord - 1);
            Explode(XCoord - 1, YCoord);
            Explode(XCoord + 1, YCoord);
            Explode(XCoord, YCoord + 1);
            if (power == 2)
            {
                return;
            }

            // Explode mine with power 3... (1 + 2 + 3)
            Explode(XCoord - 2, YCoord);
            Explode(XCoord + 2, YCoord);
            Explode(XCoord, YCoord - 2);
            Explode(XCoord, YCoord + 2);
            if (power == 3)
            {
                return;
            }

            // Explode mine with power 4... (1 + 2 + 3 + 4)
            Explode(XCoord - 1, YCoord + 2);
            Explode(XCoord + 1, YCoord + 2);
            Explode(XCoord - 1, YCoord - 2);
            Explode(XCoord + 1, YCoord - 2);
            Explode(XCoord - 2, YCoord -1);
            Explode(XCoord - 2, YCoord + 1);
            Explode(XCoord + 2, YCoord - 1);
            Explode(XCoord + 2, YCoord + 1);
            if (power == 4)
            {
                return;
            }

            // Explode mine with power 5... (1 + 2 + 3 + 4 + 5)
            Explode(XCoord - 2, YCoord - 2);
            Explode(XCoord + 2, YCoord - 2);
            Explode(XCoord - 2, YCoord + 2);
            Explode(XCoord + 2, YCoord + 2);
        }

        //tuka se izbira kva bomba da grymne
        public void DetonateMine(int XCoord, int YCoord)
        {
            switch (Convert.ToInt32(this.pozicii[XCoord, YCoord]))
            {
                case 1:
                    this.DetonateMine(XCoord, YCoord, 1);
                    break;
                case 2:
                    this.DetonateMine(XCoord, YCoord, 2);
                    break;
                case 3:
                    this.DetonateMine(XCoord, YCoord, 3);
                    break;
                case 4:
                    this.DetonateMine(XCoord, YCoord, 4);
                    break;
                case 5:
                    this.DetonateMine(XCoord, YCoord, 5);
                    break;
            }
        }

        public int PrebroiOstavashtiteMinichki()
        {
            int count = 0;

            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; i < fieldSize; i++)
                {
                    if ((this.pozicii[i, j] != ExplodedSign) && (this.pozicii[i, j] != " - "))
                        count++;
                }
            }

            return count;
        }

        public static void Main()
        {
            string tempFieldSize;
            Console.WriteLine("Welcome to the Battle Field game");
            do
            {
                Console.Write("Enter legal size of board: ");
                tempFieldSize = Console.ReadLine();
            }
            while ((!Int32.TryParse(tempFieldSize, out fieldSize)) || (fieldSize < 0) || (fieldSize > 11));

            BattleField bf = new BattleField();
            bf.InitField();
            bf.InitMines();
            bf.DisplayField();

            string coordinates;
            int XCoord, YCoord;

            do
            {
                do
                {
                    Console.Write("Enter coordinates: ");
                    coordinates = Console.ReadLine();
                    XCoord = Convert.ToInt32(coordinates.Substring(0, 1));
                    YCoord = Convert.ToInt32(coordinates.Substring(2));

                    if ((XCoord < 0) || (YCoord > fieldSize - 1) || (bf.pozicii[XCoord, YCoord] == " - "))
                        Console.WriteLine("Invalid Move");
                }
                while ((XCoord < 0) || (YCoord > fieldSize - 1) || (bf.pozicii[XCoord, YCoord] == " - "));

                bf.DetonateMine(XCoord, YCoord);
                bf.DisplayField();
                bf.detonatedMines++;
            }
            while (bf.PrebroiOstavashtiteMinichki() != 0);

            Console.WriteLine("Game Over. Detonated Mines: " + bf.detonatedMines);
            Console.ReadKey();
        }
    }
}