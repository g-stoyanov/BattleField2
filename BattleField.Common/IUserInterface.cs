namespace BattleField.Common
{
    public interface IUserInterface
    {
        void GetCommand(out int positionByX, out int positionByY, out char command);
    }
}
