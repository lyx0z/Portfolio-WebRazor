namespace GameOfLife.Core;

public static class Rules
{
    public static bool IsAliveNextFrame(bool isAlive, int neighbours)
    {
        return isAlive ? neighbours is 2 or 3 : neighbours == 3;
    }
}
