namespace MatcomFestival2._0.Logic;

public interface IGame
{
    int Turn { get; set; }
    IPlayer PlayerPlay { get; set; }
    IPlayer[] Players { get; set; }
    bool CanDraw { get; set; }
    bool CanInvoque { get; set; }
    int PlayerIndex { get; set; }
}
public interface IGameStatus
{
    int Turn { get; set; }
    IPlayer PlayerPlay { get; set; }
    int PlayerIndex { get; set; }
    IPlayerStatus[] Players { get; set; }
    bool CanDraw { get; set; }
    bool CanInvoque { get; set; }
}

public class GameStatus : IGameStatus
{
    //  Propiedades
    public int Turn { get; set; }
    public IPlayer PlayerPlay { get; set; }
    public int PlayerIndex { get; set; }
    public bool CanDraw { get; set; }
    public bool CanInvoque { get; set; }
    public IPlayerStatus[] Players { get; set; }

    //  Constructor
    public GameStatus(IGame game)
    {
        PlayerPlay = (Player)game.PlayerPlay.Clone();
        Players = SetPlayers(game.Players);
        CanDraw = game.CanDraw;
        CanInvoque = game.CanInvoque;
        PlayerIndex = game.PlayerIndex;
        Turn = game.Turn;
    }

    private IPlayerStatus[] SetPlayers(IPlayer[] players)
    {
        IPlayerStatus[] Show = new PlayerStatus[players.Length];
        for (int i = 0; i < players.Length; i++)
        {
            Show[i] = new PlayerStatus(players[i].Life, players[i].Name, players[i].Field);
        }

        return Show;
    }
}
public class Game : IGame
{
    public int Turn { get; set; }
    public IPlayer PlayerPlay { get; set; }
    public IPlayer[] Players { get; set; }
    public bool CanDraw { get; set; }
    public bool CanInvoque { get; set; }
    public int PlayerIndex { get; set; }

    public Game(IPlayer[] players)
    {
        PlayerPlay = players[0];
        Players = players;
        PlayerIndex = 0;
        CanDraw = true;
        CanInvoque = true;
        Turn = 1;
    }
}


