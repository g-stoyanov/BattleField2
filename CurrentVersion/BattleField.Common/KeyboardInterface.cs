namespace BattleField.Common
{
    using System;
    using System.Linq;

    public class KeyboardInterface : IUserInterface
    {
        public void GetCommand(out int positionByX, out int positionByY, out char command)
        {
            command = '0';
            positionByX = -1;
            positionByY = -1;

            string input = Console.ReadLine();

            if (input == "R" || input == "Q" || input == "N")
            {
                command = Convert.ToChar(input);
            }
            else if (input.Length == 3)
            {
                string[] coordinates = input.Split(' ');
                if (coordinates.Length == 2)
                {
                    if (!(int.TryParse(coordinates[0], out positionByX) && int.TryParse(coordinates[1], out positionByY)))
                    {
                        positionByX = -1;
                        positionByY = -1;
                    }                   
                }
            }
        }
    }
}