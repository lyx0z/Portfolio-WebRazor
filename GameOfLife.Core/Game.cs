namespace GameOfLife.Core;

public class Game(int width, int height, int aliveCount)
{
    private readonly Board board = new(width, height, aliveCount);

    public bool[,] GetNextFrame()
    {
        board.GenerateNextFrame();
        return board.GetBoard();
    }
}
