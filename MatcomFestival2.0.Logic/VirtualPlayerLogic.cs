using MatcomFestival2._0.Logic;
namespace VirtualPlayer;

public class VirtualPlayerLogic
{
    public VirtualPlayerLogic() { }
    public void Play(IBattle battle)
    {
        battle.Draw();

        Invocation(battle);
        CardActivation(battle);
        
        if (battle.Status.PlayerPlay.Hand.All(c => c.Name != " "))
        {
            battle.Descart(battle.Status.PlayerPlay.Hand[0]);
        }

        Attack(battle);

        if (Utils.ThereIsAWinner(battle.Status))
        {
            return;
        }

        battle.EndTurn();
    }
    private IPersonCard CardToInvoque(ICard[] hand, IGameStatus Status)
    {
        IPersonCard[] field = Status.PlayerPlay.Field;
        int cardIndex = -1;
        int maxAttack = int.MinValue;
        IPersonCard card = new PersonCard(" ", 0, 0, 0, 0, 0);
        for (int i = 0; i < hand.Length; i++)
        {
            #region validar que sea un estudiante o un profe que se pueda invocar
            if (Utils.IsEmpty(hand[i]))
            {
                continue;
            }
            if (hand[i] is SpecialCard)
            {
                continue;
            }
            if (hand[i] is ProfessorCard && ((ProfessorCard)hand[i]).Level > PersonsOnField(field))
            {
                continue;
            }
            #endregion

            card = (PersonCard)hand[i];
            #region buscar la carta que más daño reciba con esta carta
            foreach (var player in Status.Players)
            {
                #region validar que el enemigo no es la ia
                if (((PlayerStatus)Status.PlayerPlay).Equals(player))
                {
                    continue;
                }
                #endregion

                for (int c = 0; c < player.Field.Length; c++)
                {
                    #region verificar que hay una carta en esa posición
                    if (player.Field[i].Name == " ")
                    {
                        continue;
                    }
                    #endregion

                    #region buscar asignatura que más daño haga
                    if (card.Analisis - player.Field[c].Analisis > maxAttack)
                    {
                        maxAttack = card.Analisis - player.Field[c].Analisis;
                        cardIndex = i;
                    }

                    if (card.Algebra - player.Field[c].Algebra > maxAttack)
                    {
                        maxAttack = card.Algebra - player.Field[c].Algebra;
                        cardIndex = i;
                    }

                    if (card.Logic - player.Field[c].Logic > maxAttack)
                    {
                        maxAttack = card.Logic - player.Field[c].Logic;
                        cardIndex = i;
                    }

                    if (card.Pro - player.Field[c].Pro > maxAttack)
                    {
                        maxAttack = card.Pro - player.Field[c].Pro;
                        cardIndex = i;
                    }
                    #endregion
                }
            }
            #endregion
        }

        return card;
    }
    private (IPlayerStatus?, IPersonCard?) CardToAttack(IPersonCard card, IGameStatus Status)
    {
        IPersonCard[] field = Status.PlayerPlay.Field;
        int enemyIndex = -1;
        int targetIndex = -1;
        int maxAttack = 0;

        #region buscar la carta que más daño reciba con esta carta
        for (int i = 0; i < Status.Players.Length; i++)
        {
            #region validar que el enemigo no es la ia
            if (Status.Players[i].Name == Status.PlayerPlay.Name)
            {
                continue;
            }
            #endregion

            for (int c = 0; c < Status.Players[i].Field.Length; c++)
            {
                #region verificar que hay una carta en esa posición
                if (Status.Players[i].Field[c].Name == " ")
                {
                    continue;
                }
                #endregion

                #region buscar asignatura que más daño haga
                if (card.Analisis - Status.Players[i].Field[c].Analisis > maxAttack)
                {
                    maxAttack = card.Analisis - Status.Players[i].Field[c].Analisis;
                    targetIndex = c;
                    enemyIndex = i;
                }

                if (card.Algebra - Status.Players[i].Field[c].Algebra > maxAttack)
                {
                    maxAttack = card.Algebra - Status.Players[i].Field[c].Algebra;
                    targetIndex = c;
                    enemyIndex = i;
                }

                if (card.Logic - Status.Players[i].Field[c].Logic > maxAttack)
                {
                    maxAttack = card.Logic - Status.Players[i].Field[c].Logic;
                    targetIndex = c;
                    enemyIndex = i;
                }

                if (card.Pro - Status.Players[i].Field[c].Pro > maxAttack)
                {
                    maxAttack = card.Pro - Status.Players[i].Field[c].Pro;
                    targetIndex = c;
                    enemyIndex = i;
                }
                #endregion
            }
        }
        #endregion

        #region retornar un diccionario vacio si ningun ataque es efectivo
        if (targetIndex == -1)
        {
            return (null, null);
        }
        #endregion

        return (Status.Players[enemyIndex], Status.Players[enemyIndex].Field[targetIndex]);
    }
    private string SubjectToAttack(IPersonCard card, IPersonCard target)
    {
        string subject = " ";
        int maxAttack = 0;

        if (card.Analisis - target.Analisis > maxAttack)
        {
            maxAttack = card.Analisis - target.Analisis;
            subject = "Analisis";
        }

        if (card.Algebra - target.Algebra > maxAttack)
        {
            maxAttack = card.Algebra - target.Algebra;
            subject = "Algebra";
        }

        if (card.Logic - target.Logic > maxAttack)
        {
            maxAttack = card.Logic - target.Logic;
            subject = "Logic";
        }

        if (card.Pro - target.Pro > maxAttack)
        {
            maxAttack = card.Pro - target.Pro;
            subject = "Programming";
        }

        return subject;
    }
    private string SubjectToAttack(IPersonCard card)
    {
        string subject = " ";
        int maxAttack = 0;

        if (card.Analisis > maxAttack)
        {
            maxAttack = card.Analisis;
            subject = "Analisis";
        }

        if (card.Algebra > maxAttack)
        {
            maxAttack = card.Algebra;
            subject = "Algebra";
        }

        if (card.Logic > maxAttack)
        {
            maxAttack = card.Logic;
            subject = "Logic";
        }

        if (card.Pro > maxAttack)
        {
            maxAttack = card.Pro;
            subject = "Programming";
        }

        return subject;
    }
    private void Attack(IBattle battle)
    {
        foreach (var item in battle.Status.PlayerPlay.Field)
        {
            if (Utils.IsEmpty(item))
            {
                continue;
            }
            if (ThereIsNotEmptyFields(battle.Status))
            {
                (IPlayerStatus?, IPersonCard?) dupla = CardToAttack(item, battle.Status);
                #region analizar otra carta si el ataque con esta es inefectivo
                if (dupla.Item1 is null)
                {
                    continue;
                }
                #endregion
                
                battle.Attack(dupla.Item1, item, dupla.Item2, SubjectToAttack(item, dupla.Item2));
                continue;
            }
            
            IPlayerStatus enemy = battle.Status.Players.First(p => p.Field.All(c => c.Name == " "));
            
            battle.Attack(enemy, item, SubjectToAttack(item));

        }
    }
    private void Invocation(IBattle battle)
    {
        IPersonCard card = CardToInvoque(battle.Status.PlayerPlay.Hand, battle.Status);
        if (card is ProfessorCard)
        {
            IPersonCard[] sacrifices = new PersonCard[((ProfessorCard)card).Level];
            int index = 0;
            foreach (var item in battle.Status.PlayerPlay.Field)
            {
                if (!Utils.IsEmpty(item))
                {
                    sacrifices[index] = item;
                    index++;
                }
                if (index == sacrifices.Length)
                {
                    break;
                }

            }
            battle.InvoqueProfessor((ProfessorCard)card, sacrifices);
        }
        else
        {
            battle.Invoque(card);
        }
    }
    private bool ThereIsNotEmptyFields(IGameStatus Status)
    {
        return Status.Players.Where(e => e.Name != Status.PlayerPlay.Name).All(player => player.Field.Any(card => card.Name != " "));
    }
    private int PersonsOnField(IPersonCard[] persons)
    {
        int personsOnField = 0;
        for (int i = 0; i < persons.Length; i++)
        {
            if (!Utils.IsEmpty(persons[i]))
            {
                personsOnField++;
            }
        }
        return personsOnField;
    }
    private int IndexSpecialCard(IBattle battle)
    {
        for (int i = 0; i < battle.Status.PlayerPlay.Hand.Length; i++)
        {
            if (!(battle.Status.PlayerPlay.Hand[i] is SpecialCard))
            {
                continue;
            }

            int counter = 0;
            foreach (var item in battle.Status.PlayerPlay.Field)
            {
                if (item.Name != " ")
                {
                    counter++;
                }
            }

            if (counter < ((SpecialCard)battle.Status.PlayerPlay.Hand[i]).AlliesToAffect)
            {
                return -1;
            }

            counter = 0;

            foreach (var item in battle.Status.Players)
            {
                foreach (var card in item.Field)
                {
                    if (card.Name != " ")
                    {
                        counter++;
                    }
                }
            }

            if (counter < ((SpecialCard)battle.Status.PlayerPlay.Hand[i]).EnemysToAffect)
            {
                return -1;
            }

            return i;
        }
        return -1;
    }
    private void CardActivation(IBattle battle)
    {
        int indexCard = IndexSpecialCard(battle);
        #region verificar si hay carta para invocar
        if (indexCard == -1)
        {
            return;
        }
        #endregion

        ISpecialCard card = (SpecialCard)battle.Status.PlayerPlay.Hand[indexCard];
        IPersonCard[] allies = new PersonCard[card.AlliesToAffect];
        int index = 0;
        for (int i = 0; i < battle.Status.PlayerPlay.Field.Length; i++)
        {
            if (battle.Status.PlayerPlay.Field[i].Name != " ")
            {
                allies[index] = battle.Status.PlayerPlay.Field[i];
                index++;
            }
            if (index == allies.Length)
            {
                break;
            }
        }

        IPersonCard[] enemys = new PersonCard[card.EnemysToAffect];
        index = 0;
        foreach (var player in battle.Status.Players)
        {
            foreach (var target in player.Field)
            {
                if (index == enemys.Length)
                {
                    break;
                }

                if (target.Name != " ")
                {
                    enemys[index] = target;
                    index++;
                }
            }
        }

        // try
        // {
        //     ICodeAnalizer interpreter = new CodeA(card.Code);
        //     battle.Active(card, new Interpreter(interpreter.Run), allies, enemys);
        // }
        // catch (System.Exception)
        // {
        //     battle.Descart(card);
        // }
    }
}