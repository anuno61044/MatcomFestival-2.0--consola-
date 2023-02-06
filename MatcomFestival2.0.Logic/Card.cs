namespace MatcomFestival2._0.Logic;

public interface ICard
{
    string Name { get; set; }
}
public interface IPersonCard : ICard, ICloneable, IEquatable<IPersonCard>
{
    int BrainHealth { get; set; }
    int Analisis { get; set; }
    int Algebra { get; set; }
    int Logic { get; set; }
    int Pro { get; set; }
    bool Used { get; set; }
    int Level { get; }
}
public interface IProfessorCard : IPersonCard
{
    bool Sacrifices(IPlayer player, params IPersonCard[] students);
}
public interface ISpecialCard : ICard, ICloneable, IEquatable<ISpecialCard>
{
    string Description { get; }
    string Code { get; }
    int AlliesToAffect { get; }
    int EnemysToAffect { get; }
}

public abstract class Card : ICard
{
    public string Name { get; set; }

    public Card(string name)
    {
        Name = name;
    }
}
public class PersonCard : Card, IPersonCard
{
    //  Propiedades
    public int BrainHealth { get; set; }
    public int Analisis { get; set; }
    public int Algebra { get; set; }
    public int Logic { get; set; }
    public int Pro { get; set; }
    public bool Used { get; set; }
    public int Level { get; protected set; }
    //  Constructor
    public PersonCard(string Name, int BrainHealth, int Analisis, int Algebra, int Logic, int Pro) : base(Name)
    {
        this.BrainHealth = BrainHealth;
        this.Analisis = Analisis;
        this.Algebra = Algebra;
        this.Logic = Logic;
        this.Pro = Pro;
        Used = false;
        Level = 0;
    }
    //  Métodos
    public object Clone()
    {
        PersonCard card = new PersonCard(Name, BrainHealth, Analisis, Algebra, Logic, Pro);
        return card;
    }

    public bool Equals(IPersonCard? other)
    {
        if (other == null)
        {
            return false;
        }
        
        if (Name != other.Name)
        {
            return false;
        }
    
        if (BrainHealth != other.BrainHealth)
        {
            return false;
        }

        if (Analisis != other.Analisis)
        {
            return false;
        }

        if (Algebra != other.Algebra)
        {
            return false;
        }
        
        if (Logic != other.Logic)
        {
            return false;
        }

        if (Pro != other.Pro)
        {
            return false;
        }

        if (Level != other.Level)
        {
            return false;
        }

        if (Used != other.Used)
        {
            return false;
        }

        return true;
    }
}
public class SpecialCard : Card, ISpecialCard
{
    //  Propiedades
    public string Description { get; private set;}
    public string Code { get; private set;}
    public int AlliesToAffect { get; private set;}
    public int EnemysToAffect { get; private set;}
    //  Constructor
    public SpecialCard(string name, string description, string code, int alliesToAffect, int enemysToAffect) : base (name)
    {
        Description = description;
        Code = code;
        AlliesToAffect = alliesToAffect;
        EnemysToAffect = enemysToAffect;
    }
    //  Métodos
    public object Clone()
    {
        SpecialCard card = new SpecialCard(Name, Description, Code, AlliesToAffect, EnemysToAffect);
        return card;
    }

    public bool Equals(ISpecialCard? other)
    {
        if (other is null)
        {
            return false;
        }

        if (Name != other.Name)
        {
            return false;
        }

        if (Description != other.Description)
        {
            return false;
        }

        if (Code != other.Code)
        {
            return false;
        }

        if (AlliesToAffect != other.AlliesToAffect)
        {
            return false;
        }

        if (EnemysToAffect != other.EnemysToAffect)
        {
            return false;
        }

        return true;
    }

}
public class ProfessorCard : PersonCard, IProfessorCard
{
    //  Constructor
    public ProfessorCard(string Name, int Level, int BrainHealth, int Analisis, int Algebra, int Logic, int Pro) : base(Name, BrainHealth, Analisis, Algebra, Logic, Pro)
    {
        this.Level = Level;
    }
    //  Métodos
    public bool Sacrifices(IPlayer player, params IPersonCard[] students)
    {
        if (students.Length < Level)
        {
            return false;
        }

        for (int i = 0; i < Level; i++)
        {
            students[i].Name = " ";
        }
        return true;
    }
    public new object Clone()
    {
        ProfessorCard card = new ProfessorCard(Name, Level, BrainHealth, Analisis, Algebra, Logic, Pro);
        return card;
    }
}


