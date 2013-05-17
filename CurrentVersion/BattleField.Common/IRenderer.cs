namespace BattleField.Common
{
    public interface IRenderer
    {
        void Render(GameField gameField, byte detonatedMines, string menu, string message);
    }
}
