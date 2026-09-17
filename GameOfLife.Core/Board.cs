namespace GameOfLife.Core;

internal class Board(int width, int height, int aliveCount)
{
    private bool[,] board = GenerateRandomBoard(width, height, aliveCount);

    private int BoardHeight => board.GetLength(0);

    private int BoardWidth => board.GetLength(1);

    private static bool[,] GenerateRandomBoard(int width, int height, int aliveCount)
    {
        var random = new Random();
        var board = new bool[height, width];
        var placed = 0;

        while (placed < aliveCount)
        {
            var randomHeight = random.Next(height);
            var randomWidth = random.Next(width);
            if (!board[randomHeight, randomWidth])
            {
                board[randomHeight, randomWidth] = true;
                placed++;
            }
        }

        return board;
    }

    public void Randomize()
    {
        board = GenerateRandomBoard(width, height, aliveCount);
    }

    public void GenerateNextFrame()
    {
        var nextGen = new bool[BoardHeight, BoardWidth];

        for (var height = 0; height < BoardHeight; height++)
        {
            for (var width = 0; width < BoardWidth; width++)
            {
                nextGen[height, width] = Rules.IsAliveNextFrame(
                    board[height, width],
                    GetAliveNeighborAmount(height, width)
                );
            }
        }

        board = nextGen;
    }

    private int GetAliveNeighborAmount(int height, int width)
    {
        var neighbourAmount = 0;

        for (var heightOffset = -1; heightOffset <= 1; heightOffset++)
        {
            for (var widthOffset = -1; widthOffset <= 1; widthOffset++)
            {
                if (heightOffset == 0 && widthOffset == 0)
                {
                    continue;
                }

                var neighbourHeight = height + heightOffset;
                var neighbourWidth = width + widthOffset;

                if (IsOutOfBounds(neighbourHeight, neighbourWidth))
                {
                    continue;
                }

                if (board[neighbourHeight, neighbourWidth])
                {
                    neighbourAmount++;
                }
            }
        }

        return neighbourAmount;
    }

    private bool IsOutOfBounds(int neighbourHeight, int neighbourWidth)
    {
        return neighbourHeight < 0
            || neighbourHeight >= BoardHeight
            || neighbourWidth < 0
            || neighbourWidth >= BoardWidth;
    }

    public bool[,] GetBoard()
    {
        return board;
    }
}
