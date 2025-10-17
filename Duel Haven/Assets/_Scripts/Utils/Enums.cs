public enum Directions
{
    Down = 2,
    DownRight = 3,
    Right = 6,
    UpRight = 9,
    Up = 8,
    UpLeft = 7,
    Left = 4,
    DownLeft = 1
}

public enum PlayerState
{
    IDLE,
    CHANNELING,
    CANCELABLE,
    DISABLED
}

public enum CastType
{
    NONE,
    CAST,
    CHARGE,
    CHANNEL
}