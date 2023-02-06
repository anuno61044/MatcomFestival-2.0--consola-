using System.Text.Json;
namespace MatcomFestival2._0.Logic;

public interface IPlayer : IPlayerStatus
{
    ICard[] Hand { get; set; }
    ICard[] Deck { get; set; }
    int DeckIndex { get; set; }
}
public interface IPlayerStatus : ICloneable
{
    string Name { get; set; }
    int Life { get; set; }
    IPersonCard[] Field { get; set; }
}
public interface IVirtualPlayer : IPlayer
{
    IALogic Logic { get; set; }
}

public class PlayerStatus : IPlayerStatus, IEquatable<IPlayerStatus>
{
    //  Propiedades
    public int Life { get; set; }
    public string Name { get; set; }
    public IPersonCard[] Field { get; set; }
    //  Constructores
    public PlayerStatus(string Name)
    {
        Life = 100;
        this.Name = Name;
        Field = new PersonCard[5];
    }
    public PlayerStatus(int Life, string Name, IPersonCard[] Field)
    {
        this.Life = Life;
        this.Name = Name;
        this.Field = Field;
    }
    //  Métodos
    public object Clone()
    {
        PlayerStatus player = new PlayerStatus(Life, Name, (PersonCard[])Field.Clone());
        return player;
    }
    
    public bool Equals(IPlayerStatus? other)
    {
        if (other is null)
        {
            return false;
        }

        if (Name != other.Name)
        {
            return false;
        }

        if (Life != other.Life)
        {
            return false;
        }

        for (int i = 0; i < Field.Length; i++)
        {
            if (!Field.Equals(other.Field))
            {
                return false;
            }
        }

        return true;
    }
}
public class Player : PlayerStatus, IPlayer
{
    //  Propiedades
    public ICard[] Hand { get; set; }
    public ICard[] Deck { get; set; }
    public int DeckIndex { get; set; }
    //  Constructor
    public Player(string Name) : base(Name)
    {
        this.Name = Name;
        Life = 100;
        DeckIndex = 5;

        Field = new PersonCard[5];
        for (int i = 0; i < 5; i++)
        {
            Field[i] = new PersonCard(" ", 0, 0, 0, 0, 0);
        }
        Hand = new Card[5];
        Deck = SetDeck();
    }
    //  Métodos
    #region métodos para formar el deck y barajarlo
    private ICard[] SetDeck()
    {
        string d = File.ReadAllText("MatcomFestival2.0.Logic/Data/PersonCards.json");
        string e = File.ReadAllText("MatcomFestival2.0.Logic/Data/SpecialCards.json");
        string f = File.ReadAllText("MatcomFestival2.0.Logic/Data/ProfessorCards.json");

        PersonCard[] personCards; SpecialCard[] specialCards; ProfessorCard[] professorCards;

        if (d == "")
        {
            personCards = Array.Empty<PersonCard>();
        }
        else
        {
            personCards = JsonSerializer.Deserialize<PersonCard[]>(d);
        }
        if (e == "")
        {
            specialCards = Array.Empty<SpecialCard>();
        }
        else
        {
            specialCards = JsonSerializer.Deserialize<SpecialCard[]?>(e);
        }
        if (f == "")
        {
            professorCards = Array.Empty<ProfessorCard>();
        }
        else
        {
            professorCards = JsonSerializer.Deserialize<ProfessorCard[]?>(f);
        }

        ICard[] deck = new Card[personCards.Length + specialCards.Length + professorCards.Length];

        for (int i = 0; i < personCards.Length; i++)
        {
            deck[i] = personCards[i];
        }
        for (int i = personCards.Length; i < personCards.Length + specialCards.Length; i++)
        {
            deck[i] = specialCards[i - personCards.Length];
        }
        for (int i = personCards.Length + specialCards.Length; i < personCards.Length + specialCards.Length + professorCards.Length; i++)
        {
            deck[i] = professorCards[i - personCards.Length + specialCards.Length];
        }

        deck = Shuffle(deck);

        for (int i = 0; i < 5; i++)
        {
            ICard card = deck[i];
            Hand[i] = card;
        }
        return deck;
    }
    private ICard[] Shuffle(ICard[] deck)
    {
        Random random = new Random();
        List<ICard> list = deck.ToList();

        ICard[] newDeck = new Card[deck.Length];
        for (int i = 0; i < deck.Length; i++)
        {
            int index = random.Next(0, deck.Length - i);
            newDeck[i] = list[index];
            list.RemoveAt(index);
        }
        return newDeck;
    }
    
    #endregion
    public new object Clone()
    {
        IPlayer player = new Player(Name);

        player.DeckIndex = DeckIndex;
        player.Life = Life;

        player.Hand = (Card[])Hand.Clone();
        player.Field = (PersonCard[])Field.Clone();
        player.Deck = (Card[])Deck.Clone();

        return player;
    }

    public bool Equals(IPlayer? other)
    {
        if (other is null)
        {
            return false;
        }

        if (!((PlayerStatus)this).Equals((PlayerStatus)other))
        {
            return false;
        }

        if (!Hand.Equals(other.Hand))
        {
            return false;
        }

        if (!Deck.Equals(other.Deck))
        {
            return false;
        }

        return true;
    }
}
public class IA : Player, IVirtualPlayer
{
    public IALogic Logic { get; set; }
    public IA(string name, IALogic logic) : base(name)
    {
        Logic = logic;
    }

    public void Play(IBattle battle)
    {
        Logic(battle);
    }
}






