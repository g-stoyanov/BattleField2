namespace BattleField.Common
{
    public interface IRenderer
    {
        string Render(GameField gameField, byte detonatedMines, string menu, string message);
    }
}
