namespace MatcomFestival2._0.Logic;

//funcion del interprete que guarda el codigo
public interface ICodeAnalizer
{
    void CodeAnalizer(string code);
}
public interface IExecuteCode
{
    void Compile(IGame game, IPersonCard[] allies, IPersonCard[] enemys);
}
public delegate void IALogic(IBattle battle);    // delegado de la IA
public delegate void Interpreter(IGame game, IPersonCard[] allies, IPersonCard[] enemys);


public interface IBattle
{
    IGameStatus Status { get; }
    internal IGame MatcomFestival { get; set; }
}
public interface IPlay
{
    void Attack(IPlayerStatus enemy, IPersonCard card, IPersonCard target, string skill);
    void Attack(IPlayerStatus enemy, IPersonCard card, string skill);
    void Invoque(IPersonCard card);
    void Draw();
    void Active(ISpecialCard card, Interpreter interprete, IPersonCard[] allies, IPersonCard[] enemys);
    void EndTurn();
    void Descart(ICard card);
    void InvoqueProfessor(IProfessorCard professor, IPersonCard[] sacrifices);
}

public class Battle : IBattle
{
    public IGameStatus Status { get; set; }
    public IGame MatcomFestival { get; set; }
    //  Constructor
    public Battle(params IPlayer[] players)
    {
        MatcomFestival = new Game(players);
        Status = new GameStatus(MatcomFestival);

        if (players[0] is IA)
        {
            ((IA)players[0]).Play(this);
        }
    }


}

public static class Play
{
    public static void Active(this IBattle battle, ISpecialCard card, Interpreter interprete, IPersonCard[] allies, IPersonCard[] enemys)
    {
        #region revisando la activación es correcta
        //  la carta activada debe estar en la mano del jugador
        //  las cartas aliadas a afectar deben estar en el campo del jugador que está jugando
        //  las cartas enemigas deben estar en campos enemigos
        if (!AntiHack.IsOnHand(battle.MatcomFestival.PlayerPlay, card))
        {
            return;
        }

        if (!AntiHack.IsOnField(battle.MatcomFestival.PlayerPlay, allies))
        {
            return;
        }

        if (!AntiHack.IsOnEnemysField(battle.MatcomFestival, enemys))
        {
            return;
        }
        #endregion

        int indexCard = -1;
        for (int i = 0; i < battle.MatcomFestival.PlayerPlay.Hand.Length; i++)
        {
            if (battle.MatcomFestival.PlayerPlay.Hand[i].Name == card.Name)
            {
                indexCard = i;
            }
        }

        if (indexCard == -1)
        {
            return;
        }

        try
        {
            interprete(battle.MatcomFestival, allies, enemys);
        }
        catch (System.Exception)
        {
            return;
        }

        Utils.RemoveCard(battle.MatcomFestival.PlayerPlay.Hand[indexCard]);

        Utils.UpdateGameStatus(battle.MatcomFestival, battle.Status);

    }
    public static void Attack(this IBattle battle, IPlayerStatus enemy, IPersonCard card, IPersonCard target, string skill)
    {
        #region revisando si el ataque es valido
        //  no es el 1er turno, la carta no ha sido usada en este turno
        //  la carta atacante está en el campo del jugador que está jugando
        //  el objetivo no es un espacio vacío y está en algún campo enemigo
        if (card.Used || battle.MatcomFestival.Turn == 1)
        {
            return;
        }

        if (!AntiHack.IsOnField(battle.MatcomFestival.PlayerPlay, card))
        {
            return;
        }

        if (Utils.IsEmpty(target))
        {
            return;
        }

        if (!AntiHack.IsAnEnemy(battle.MatcomFestival, enemy))
        {
            return;
        }

        if (!AntiHack.IsOnEnemysField(battle.MatcomFestival, target))
        {
            return;
        }

        if (Utils.PlayerIsDead(enemy))
        {
            return;
        }
        #endregion

        #region ataque
        switch (skill)
        {
            case "Algebra":
                if (card.Algebra > target.Algebra)
                    target.BrainHealth -= (card.Algebra - target.Algebra);
                break;
            case "Analisis":
                if (card.Analisis > target.Analisis)
                    target.BrainHealth -= (card.Analisis - target.Analisis);
                break;
            case "Logic":
                if (card.Logic > target.Logic)
                    target.BrainHealth -= (card.Logic - target.Logic);
                break;
            case "Programming":
                if (card.Pro > target.Pro)
                    target.BrainHealth -= (card.Pro - target.Pro);
                break;
            default:
                return;
        }
        #endregion

        #region validar si la carta fue destruida y afectar la vida del enemigo
        if (Utils.CardIsDead(target))
        {
            enemy.Life += target.BrainHealth;
            Utils.DestroyCard(target);
        }

        if (Utils.PlayerIsDead(enemy))
        {
            Utils.DestroyPlayer(enemy);
        }
        #endregion

        card.Used = true;

        battle.MatcomFestival.CanInvoque = false;

        Utils.UpdateGameStatus(battle.MatcomFestival, battle.Status);
    }
    public static void Attack(this IBattle battle, IPlayerStatus enemy, IPersonCard card, string skill)
    {
        #region revisando si el ataque es valido
        //  la carta no ha sido usada y no es el 1er turno
        //  la carta debe estar en el campo del jugador que está jugando
        if (card.Used || battle.MatcomFestival.Turn == 1)
        {
            return;
        }

        if (!AntiHack.IsOnField(battle.MatcomFestival.PlayerPlay, card))
        {
            return;
        }

        if (!AntiHack.FieldIsEmpty(enemy))
        {
            return;
        }

        if (Utils.PlayerIsDead(enemy))
        {
            return;
        }
        #endregion

        #region ataque
        switch (skill)
        {
            case "Algebra":
                enemy.Life -= card.Analisis;
                break;
            case "Analisis":
                enemy.Life -= card.Algebra;
                break;
            case "Logic":
                enemy.Life -= card.Logic;
                break;
            case "Programming":
                enemy.Life -= card.Pro;
                break;
            default:
                return;
        }
        #endregion

        #region si el player está muerto sacarlo del juego
        if (Utils.PlayerIsDead(enemy))
        {
            Utils.DestroyPlayer(enemy);
        }
        #endregion

        card.Used = true;

        battle.MatcomFestival.CanInvoque = false;

        Utils.UpdateGameStatus(battle.MatcomFestival, battle.Status);
    }
    public static void Draw(this IBattle battle)
    {
        IPlayer player = battle.MatcomFestival.PlayerPlay;
        System.Console.WriteLine("turno" + battle.MatcomFestival.Turn);
        #region verificar si se puede robar
        if (!battle.MatcomFestival.CanDraw || battle.MatcomFestival.Turn == 1)
        {
            return;
        }

        if (player.DeckIndex == player.Deck.Length)
        {
            return;
        }
        #endregion

        #region robar
        for (int i = 0; i < player.Hand.Length; i++)
        {
            if (Utils.IsEmpty(player.Hand[i]))
            {
                System.Console.WriteLine("entre aqui");
                player.Hand[i] = player.Deck[player.DeckIndex];
                player.DeckIndex++;
                break;
            }
        }
        #endregion

        battle.MatcomFestival.CanDraw = false;

        Utils.UpdateGameStatus(battle.MatcomFestival, battle.Status);
    }
    public static void EndTurn(this IBattle battle)
    {
        IPlayer[] players = battle.MatcomFestival.Players;

        #region cambiar el jugador que está jugando
        while (Utils.PlayerIsDead(players[(battle.MatcomFestival.PlayerIndex + 1) % players.Length]))
        {
            battle.MatcomFestival.PlayerIndex = (battle.MatcomFestival.PlayerIndex + 1) % players.Length;
            #region si vuelve a jugar el 1er jugador cambia el número de turno
            if (battle.MatcomFestival.PlayerIndex == 0)
            {
                battle.MatcomFestival.Turn++;
            }
            #endregion
        }

        battle.MatcomFestival.PlayerIndex = (battle.MatcomFestival.PlayerIndex + 1) % players.Length;
        battle.MatcomFestival.PlayerPlay = players[battle.MatcomFestival.PlayerIndex];

        #endregion

        #region cambiar turno
        if (battle.MatcomFestival.PlayerIndex == 0)
        {
            battle.MatcomFestival.Turn++;
        }
        #endregion

        battle.MatcomFestival.CanDraw = true;
        battle.MatcomFestival.CanInvoque = true;

        #region limpiar estado de uso de las cartas del campo
        for (int i = 0; i < 5; i++)
        {
            if (battle.MatcomFestival.PlayerPlay.Field[i].Name != " ")
            {
                battle.MatcomFestival.PlayerPlay.Field[i].Used = false;
            }
        }
        #endregion

        Utils.UpdateGameStatus(battle.MatcomFestival, battle.Status);

        if (battle.MatcomFestival.PlayerPlay is IA)
        {
            IA player = (IA)battle.MatcomFestival.PlayerPlay;
            player.Play(battle);
        }
    }
    public static void Invoque(this IBattle battle, IPersonCard card)
    {
        IPlayer player = battle.MatcomFestival.PlayerPlay;
        #region revisando si la invocación es valida
        //  en el turno debe poderse invocar
        //  la carta debe estar en la mano
        if (!battle.MatcomFestival.CanInvoque)
        {
            return;
        }

        if (!AntiHack.IsOnHand(player, card))
        {
            return;
        }
        #endregion

        #region seleccionar posición de la carta que se invoca
        int index = -1;
        for (int i = 0; i < player.Hand.Length; i++)
        {
            if (player.Hand[i].Name == card.Name)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
        {
            return;
        }
        #endregion

        #region sacar de la mano y ponerla en el campo
        for (int i = 0; i < player.Field.Length; i++)
        {
            if (Utils.IsEmpty(player.Field[i]))
            {
                player.Field[i] = (PersonCard)((PersonCard)player.Hand[index]).Clone();
                Utils.RemoveCard(player.Hand[index]);
                break;
            }
        }
        #endregion

        battle.MatcomFestival.CanInvoque = false;

        Utils.UpdateGameStatus(battle.MatcomFestival, battle.Status);
    }
    public static void InvoqueProfessor(this IBattle battle, IProfessorCard card, params IPersonCard[] persons)
    {
        IPlayer player = battle.MatcomFestival.PlayerPlay;

        #region revisando si la invocación es valida
        //  en el turno se debe poder invocar
        //  los sacrificios no pueden estar repetidos
        //  el profesor debe estar en la mano para ser invocado
        //  los sacrificios deben estar en el campo del jugador que está jugando
        if (!battle.MatcomFestival.CanInvoque)
        {
            return;
        }

        if (Utils.AreRepeated<IPersonCard>(persons))
        {
            return;
        }

        if (!AntiHack.IsOnHand(player, card))
        {
            return;
        }

        if (!AntiHack.IsOnField(player, persons))
        {
            return;
        }
        #endregion

        #region seleccionar posición del profesor en la mano
        int index = -1;
        for (int i = 0; i < player.Hand.Length; i++)
        {
            if (player.Hand[i].Equals(card))
            {
                index = i;
                break;
            }
        }
        #endregion

        #region efectuar los sacrificios e invocar
        if (card.Sacrifices(player, persons))
        {
            for (int i = 0; i < player.Field.Length; i++)
            {
                if (Utils.IsEmpty(player.Field[i]))
                {
                    player.Field[i] = (ProfessorCard)card.Clone();
                    Utils.RemoveCard(player.Hand[index]);
                    break;
                }
            }

            battle.MatcomFestival.CanInvoque = false;

        }
        #endregion

        battle.MatcomFestival.CanInvoque = false;

        Utils.UpdateGameStatus(battle.MatcomFestival, battle.Status);
    }
    public static void Descart(this IBattle battle, ICard card)
    {
        IPlayer player = battle.MatcomFestival.PlayerPlay;

        #region revisando si la carta está en la mano
        if (!AntiHack.IsOnHand(player, card))
        {
            return;
        }
        #endregion

        #region remover de la mano
        for (int i = 0; i < player.Hand.Length; i++)
        {
            if (player.Hand[i].Name == card.Name)
            {
                Utils.RemoveCard(player.Hand[i]);
            }
        }
        #endregion

        Utils.UpdateGameStatus(battle.MatcomFestival, battle.Status);
    }

}