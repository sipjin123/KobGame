
public enum GameStates
{
    Title,
    Start,
    Results,
    Win,
    Lose
}

public enum Turn
{
    Left,
    Right
}

public enum CharacterStates
{
    Ready,
    Running,
    Died,
    Idle
}

public enum ObstacleType
{
    None,
    Cannon,
    Spinner,
    Hammer,
    Sharks
}

public enum PlatformType
{
    Straight,
    Left,
    Right,
    End
}

public enum SFX
{
    SharkBite,
    SharkLaunch,
    DieSpin,
    DiePendulum,
    DieShark,
    DieCannon,
    Win,
    Lose,
    Button,
}