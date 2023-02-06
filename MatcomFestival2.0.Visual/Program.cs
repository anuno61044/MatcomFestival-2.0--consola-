using MatcomFestival2._0.Logic;
using VirtualPlayer;


public class Program
{
    public static void Main()
    {
        Visual visual = new Visual();

        // IBattle? battle = visual.MainMenu();
        // if (battle is null)
        // {
        //     return;
        // }

        // Battle battle = new Battle(new Player("Alfredo"), new Player("Abraham"));

        VirtualPlayerLogic terminator = new VirtualPlayerLogic();

        // Battle battle = new Battle(new IA(new IALogic(terminator.Play), "A"), new IA(new IALogic(terminator.Play), "B"));
        // Battle battle = new Battle(new Player("Alfredo"), new IA("Terminator", new IALogic(terminator.Play)));
        Battle battle = new Battle(new Player("A"), new Player("B"));

        visual.Game(battle);


    }
}

public class Visual
{
    public Visual() { }

    public IBattle? MainMenu()
    {
        Console.WriteLine("       > MATCOM FESTIVAL <");

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine(" >> MAIN MENU << ");
            Console.WriteLine();
            Console.WriteLine("1.[Battle]");
            Console.WriteLine("2.[Create Card]");
            Console.WriteLine("3.[Credits]");
            Console.WriteLine("0.[Exit]");
            Console.WriteLine();

            Console.WriteLine("Please select an option");
            string option = Console.ReadLine();
            switch (option)
            {
                case "0":
                    return null;
                case "1":
                    return Createbattle();
                case "2":
                    CreateCard();
                    break;
                case "3":
                    Credits();
                    break;
                default:
                    Console.WriteLine("Invalid number");
                    return null;
            }
        }
    }
    private Battle? Createbattle()
    {
        Console.WriteLine();
        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine();
        Console.WriteLine(" >>> BATTLE CREATOR <<< ");
        Console.WriteLine();

        #region configuración de players
        // Console.WriteLine("How many players will play ? (Press the number of Players)");
        // string num = Console.ReadLine();
        // int number;

        // #region validando anormalidades
        // try
        // {
        //     number = Int32.Parse(num);
        // }
        // catch (System.Exception)
        // {
        //     Console.WriteLine("Invalid input");
        //     return null;
        // }

        // if (number <= 1)
        // {
        //     Console.WriteLine("The number of players is invalid");
        //     return null;
        // }
        // #endregion

        int number = 2;

        Player[] players = new Player[number];
        for (int i = 0; i < number; i++)
        {
            #region creando los distintos tipos de jugadores
            Console.WriteLine("What kind of player is Player {0}", (i + 1));
            Console.WriteLine("1.[Human]  2.[IA]");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Write a name for the Player {0}", (i + 1));
                    string name1 = Console.ReadLine();
                    if (name1 == "")
                    {
                        Console.WriteLine("Invalid name1");
                        return null;
                    }
                    if (name1 == "Diaz Canel")
                    {
                        Console.WriteLine("Really? Are you a Comunist? The are too many names in the world, choice another name please");
                        return null;
                    }
                    players[i] = new Player(name1);
                    break;
                case "2":
                    Console.WriteLine("Write a name for the Player{0}", i + 1);
                    string name2 = Console.ReadLine();
                    if (name2 == "")
                    {
                        Console.WriteLine("Invalid name2");
                        return null;
                    }
                    if (name2 == "Diaz Canel")
                    {
                        Console.WriteLine("Really? Are you a Comunist? The are too many names in the world, choice another name please");
                        return null;
                    }
                    VirtualPlayerLogic terminator = new VirtualPlayerLogic();
                    players[i] = new IA(name2, new IALogic(terminator.Play));
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    return null;
            }
            #endregion
        }
        #endregion

        Battle battle = new Battle(players);
        return battle;
    }
    private void Credits()
    {
        Console.WriteLine();
        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine();
        Console.WriteLine(" >>> CREDITS <<< ");
        Console.WriteLine();

        Console.WriteLine("Admin: Dev.Albaro Suárez Valdez");
        Console.WriteLine("Admin: Dev.Alfredo Nuño Oquendo");
        Console.WriteLine("Support: Dev.Alejando ... (mentor)");
        Console.WriteLine();
    }
    public void CreateCard()
    {
        Console.WriteLine();
        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine();
        Console.WriteLine(" >>>> CARD CREATOR <<<< ");
        Console.WriteLine();

        Console.WriteLine("Wich kind of card do you want to create ?");
        Console.WriteLine("Select an option");
        Console.WriteLine("1.[Student]  2.[Professor]  3.[Special]");
        string? option = Console.ReadLine();
        switch (option)
        {
            case "1":
                CreateStudent();
                break;
            case "2":
                CreateProfesor();
                break;
            case "3":
                CreateSpecialCard();
                break;
            default:
                Console.WriteLine("Invalid input");
                return;
        }
    }
    private void CreateStudent()
    {
        Console.WriteLine();
        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine();
        Console.WriteLine(" >>>> STUDENT CREATOR <<<< ");
        Console.WriteLine();

        #region pedir nombre y estadisticas
        //      Name
        Console.WriteLine("Write the name of the student");
        string? name = Console.ReadLine();
        if (name == "" || name == null)
        {
            Console.WriteLine("Invalid name");
            return;
        }
        //      BrainHealth
        Console.WriteLine("Set the BrainHealth points");
        string? brainHealth_string = Console.ReadLine();
        int brainHealth = 100;
        try
        {
            brainHealth = Int32.Parse(brainHealth_string);
        }
        catch (System.Exception)
        {
            Console.WriteLine("Invalid BrainHealth value, will be set 50 as a default value");
        }
        //      Analisis
        Console.WriteLine("Set the Analisis points");
        string? analisis_string = Console.ReadLine();
        int analisis = 50;
        try
        {
            analisis = Int32.Parse(analisis_string);
        }
        catch (System.Exception)
        {
            Console.WriteLine("Invalid Analisis value, will be set 50 as a default value");
        }
        //      Algebra
        Console.WriteLine("Set the Algebra points");
        string? algebra_string = Console.ReadLine();
        int algebra = 50;
        try
        {
            algebra = Int32.Parse(algebra_string);
        }
        catch (System.Exception)
        {
            Console.WriteLine("Invalid Algebra value, will be set 50 as a default value");
        }
        //      Programming
        Console.WriteLine("Set the Programming points");
        string? pro_string = Console.ReadLine();
        int pro = 50;
        try
        {
            pro = Int32.Parse(pro_string);
        }
        catch (System.Exception)
        {
            Console.WriteLine("Invalid Programming value, will be set 50 as a default value");
        }
        //      Logic
        Console.WriteLine("Set the Logic points");
        string? logic_string = Console.ReadLine();
        int logic = 50;
        try
        {
            logic = Int32.Parse(logic_string);
        }
        catch (System.Exception)
        {
            Console.WriteLine("Invalid Logic value, will be set 50 as a default value");
        }
        #endregion

        #region validar si la carta creada es la esperada
        string accept = " ";
        while (accept != "y")
        {
            Console.WriteLine("Do you want to add this card to the deck ?");
            Console.WriteLine("     Name: {0}", name);
            Console.WriteLine("     BrainHealth: {0}", brainHealth);
            Console.WriteLine("     Analisis Points: {0}", analisis);
            Console.WriteLine("     Algebra Points: {0}", algebra);
            Console.WriteLine("     Logic Points: {0}", logic);
            Console.WriteLine("     Programming: {0}", pro);
            Console.WriteLine();
            Console.WriteLine("Press y.[yes] or n.[no]");
            accept = Console.ReadLine();
            if (accept == "n")
            {
                return;
            }
            if (accept != "y")
            {
                Console.WriteLine("Invalid input.");
            }
        }
        #endregion

        #region añadir carta al json
        //crear la carta
        IPersonCard NewCard = new PersonCard(name, brainHealth, analisis, algebra, logic, pro);
        Utils.AddCardToDeck(NewCard);
        #endregion

        Console.WriteLine();
        Console.WriteLine("This card was added to the deck");
        Console.WriteLine("     Name: {0}", name);
        Console.WriteLine("     BrainHealth: {0}", brainHealth);
        Console.WriteLine("     Analisis Points: {0}", analisis);
        Console.WriteLine("     Algebra Points: {0}", algebra);
        Console.WriteLine("     Logic Points: {0}", logic);
        Console.WriteLine("     Programming: {0}", pro);
        Console.WriteLine();
    }
    private void CreateProfesor()
    {
        Console.WriteLine();
        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine();
        Console.WriteLine(" >>>> PROFESSOR CREATOR <<<< ");
        Console.WriteLine();

        #region pedir nombre y estadisticas
        //      Name
        Console.WriteLine("Write the name of the professor");
        string? name = Console.ReadLine();
        if (name == "" || name == null)
        {
            Console.WriteLine("Invalid name");
            return;
        }
        //      brainHealth
        Console.WriteLine("Set the BrainHealth points");
        string? brainHealth_string = Console.ReadLine();
        int brainHealth = 100;
        try
        {
            brainHealth = Int32.Parse(brainHealth_string);
        }
        catch (System.Exception)
        {
            Console.WriteLine("Invalid BrainHealth value, will be set 100 as a default value");
        }
        //      Analisis
        Console.WriteLine("Set the Analisis points");
        string? analisis_string = Console.ReadLine();
        int analisis = 50;
        try
        {
            analisis = Int32.Parse(analisis_string);
        }
        catch (System.Exception)
        {
            Console.WriteLine("Invalid Analisis value, will be set 50 as a default value");
        }
        //      Algebra
        Console.WriteLine("Set the Algebra points");
        string? algebra_string = Console.ReadLine();
        int algebra = 50;
        try
        {
            algebra = Int32.Parse(algebra_string);
        }
        catch (System.Exception)
        {
            Console.WriteLine("Invalid Algebra value, will be set 50 as a default value");
        }
        //      Programming
        Console.WriteLine("Set the Programming points");
        string? pro_string = Console.ReadLine();
        int pro = 50;
        try
        {
            pro = Int32.Parse(pro_string);
        }
        catch (System.Exception)
        {
            Console.WriteLine("Invalid Programming value, will be set 50 as a default value");
        }
        //      Logic
        Console.WriteLine("Set the Logic points");
        string? logic_string = Console.ReadLine();
        int logic = 50;
        try
        {
            logic = Int32.Parse(logic_string);
        }
        catch (System.Exception)
        {
            Console.WriteLine("Invalid Logic value, will be set 50 as a default value");
        }
        //      Nivel
        Console.WriteLine("Set Level");
        string? lvl_string = Console.ReadLine();
        int lvl = 2;
        try
        {
            lvl = Int32.Parse(lvl_string);
        }
        catch (System.Exception)
        {
            Console.WriteLine("Invalid Level value, will be set 2 as a default value");
        }
        if (lvl < 0 || lvl > 2)
        {
            Console.WriteLine("Invalid Level value, will be set 2 as a default value");
        }
        #endregion

        #region validar si la carta creada es la esperada
        string accept = " ";
        while (accept != "y")
        {
            Console.WriteLine("Do you want to add this card to the deck ?");
            Console.WriteLine("     Name: {0}", name);
            Console.WriteLine("     Level: {0}", lvl);
            Console.WriteLine("     BrainHealth: {0}", brainHealth);
            Console.WriteLine("     Analisis Points: {0}", analisis);
            Console.WriteLine("     Algebra Points: {0}", algebra);
            Console.WriteLine("     Logic Points: {0}", logic);
            Console.WriteLine("     Programming: {0}", pro);
            Console.WriteLine();
            Console.WriteLine("Press y.[yes] or n.[no]");
            accept = Console.ReadLine();
            if (accept == "n")
            {
                return;
            }
            if (accept != "y")
            {
                Console.WriteLine("Invalid input.");
            }
        }
        #endregion

        #region añadir carta al json
        //crear la carta
        ProfessorCard newCard = new ProfessorCard(name, lvl, brainHealth, analisis, algebra, logic, pro);
        Utils.AddCardToDeck(newCard);
        
        #endregion

        Console.WriteLine();
        Console.WriteLine("This card was added to the deck");
        Console.WriteLine("     Name: {0}", name);
        Console.WriteLine("     Level: {0}", lvl);
        Console.WriteLine("     BrainHealth: {0}", brainHealth);
        Console.WriteLine("     Analisis Points: {0}", analisis);
        Console.WriteLine("     Algebra Points: {0}", algebra);
        Console.WriteLine("     Logic Points: {0}", logic);
        Console.WriteLine("     Programming: {0}", pro);
        Console.WriteLine();
    }
    private void CreateSpecialCard()
    {
        Console.WriteLine();
        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine();
        Console.WriteLine(" >>>> SPECIAL CREATOR <<<< ");
        Console.WriteLine();

        #region pedir datos de la carta
        //        Name
        Console.WriteLine("Write the name of the card");
        string? name = Console.ReadLine();
        if (name == "" || name == null)
        {
            Console.WriteLine("Invalid name");
            return;
        }
        //      Descripción
        Console.WriteLine("Write a description of the card");
        string? description = Console.ReadLine();
        if (description == "" || description == null)
        {
            Console.WriteLine("Invalid description");
            return;
        }
        //      Code
        Console.WriteLine("Write the effect to activate when the card be played");
        string? code = Console.ReadLine();
        if (code == "" || code == null)
        {
            Console.WriteLine("Invalid code");
            return;
        }
        //      Cantidad de cartas que affecta la carta
        Console.WriteLine("Set how many cards on your field this card will affect");
        string? alliesToAffect_string = Console.ReadLine();
        int alliesToAffect = 0;
        try
        {
            alliesToAffect = Int32.Parse(alliesToAffect_string);
        }
        catch (System.Exception)
        {
            Console.WriteLine("Invalid input");
            return;
        }
        if (alliesToAffect < 0 || alliesToAffect > 5)
        {
            Console.WriteLine("Invalid input");
            return;
        }
        //      Cantidad de jugadores que affecta la carta
        Console.WriteLine("Set how many players this card affect");
        string? enemysToAffect_string = Console.ReadLine();
        int enemysToAffect = 0;
        try
        {
            enemysToAffect = Int32.Parse(enemysToAffect_string);
        }
        catch (System.Exception)
        {
            Console.WriteLine("Invalid input");
            return;
        }
        if (enemysToAffect < 0)
        {
            Console.WriteLine("Invalid input");
            return;
        }
        #endregion

        #region validar si la carta creada es la esperada
        string accept = " ";
        while (accept != "y")
        {
            Console.WriteLine("Do you want to add this card to the deck ?");
            Console.WriteLine("     Name: {0}", name);
            Console.WriteLine("     Description: {0}", description);
            Console.WriteLine("     Code: {0}", code);
            Console.WriteLine("     How many cards affect: {0}", alliesToAffect);
            Console.WriteLine("     How many players affect: {0}", enemysToAffect);
            Console.WriteLine();
            Console.WriteLine("Press y.[yes] or  n.[no]");
            accept = Console.ReadLine();
            Console.WriteLine();
            if (accept == "n")
            {
                return;
            }
            if (accept != "y")
            {
                Console.WriteLine("Invalid input.");
            }
        }
        #endregion

        #region añadir carta al json
        //crear la carta
        SpecialCard card = new SpecialCard(name, description, code, alliesToAffect, enemysToAffect);
        Utils.AddCardToDeck(card);
        #endregion

        Console.WriteLine();
        Console.WriteLine("This card was added to the deck");
        Console.WriteLine("     Name: {0}", name);
        Console.WriteLine("     Description: {0}", description);
        Console.WriteLine("     Code: {0}", code);
        Console.WriteLine("     How many cards affect: {0}", alliesToAffect);
        Console.WriteLine("     How many players affect: {0}", enemysToAffect);
        Console.WriteLine();

    }
    public void Game(IBattle battle)
    {
        while (!Utils.ThereIsAWinner(battle.Status))
        {
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine(" >>> BATTLE <<< ");
            Console.WriteLine();


            #region imprimir estadistias de los jugadores que no estan jugando
            IPlayerStatus[] players = battle.Status.Players;
            for (int i = 0; i < players.Length; i++)
            {
                if (i == battle.Status.PlayerIndex)
                {
                    continue;
                }

                if (players[i].Name == " ")
                {
                    continue;
                }

                Console.WriteLine("                     Name: " + players[i].Name + "    " + "Life: " + players[i].Life);
                Console.WriteLine("Field: (Name : Lvl , BrainHealth , AnalisisPoints , AlgebraPoints , ProgrammingPoints , LogicPoints)");

                for (int j = 0; j < players[i].Field.Length; j++)
                {
                    if (players[i].Field[j].Name == " ")
                    {
                        Console.WriteLine("     {0}.[-- Empty --]", j + 1);
                        continue;
                    }

                    Console.WriteLine("     " + (j + 1) + ".[" + players[i].Field[j].Name + " : " + players[i].Field[j].Level + " , " + players[i].Field[j].BrainHealth + " , " + players[i].Field[j].Analisis + " , " + players[i].Field[j].Algebra + " , " + players[i].Field[j].Pro + " , " + players[i].Field[j].Logic + "]");
                }
                Console.WriteLine();
            }
            #endregion

            #region imprimir estadisticas del jugador que está jugando
            IPlayer player = battle.Status.PlayerPlay;


            Console.WriteLine("                     My Name: " + player.Name + "   " + "My Life: " + player.Life);

            //imprimir cartas en el campo
            Console.WriteLine("MyField: (Name : Lvl , BrainHealth , AnalisisPoints , AlgebraPoints , ProgrammingPoints , LogicPoints)");
            for (int i = 0; i < player.Field.Length; i++)
            {
                if (player.Field[i].Name == " ")
                {
                    Console.WriteLine("     {0}.[-- Empty --]", i + 1);
                    continue;

                }

                if (player.Field[i] is PersonCard)
                {
                    PersonCard personCard = (PersonCard)player.Field[i];

                    if (personCard.Used)
                    {
                        Console.WriteLine("     " + (i + 1) + ".[" + personCard.Name + " : " + personCard.Level + " , " + personCard.BrainHealth + " , " + personCard.Analisis + " , " + personCard.Algebra + " , " + personCard.Pro + " , " + personCard.Logic + "]");
                        continue;
                    }

                    Console.WriteLine("     " + (i + 1) + ".[" + personCard.Name + " : " + personCard.Level + " , " + personCard.BrainHealth + " , " + personCard.Analisis + " , " + personCard.Algebra + " , " + personCard.Pro + " , " + personCard.Logic + "]       (attack avaliable)");
                }
            }

            //imprimir cartas en la mano
            Console.WriteLine("MyHand: (Name : Lvl , BrainHealth , AnalisisPoints , AlgebraPoints , ProgrammingPoints , LogicPoints)");
            for (int i = 0; i < player.Hand.Length; i++)
            {
                if (player.Hand[i].Name == " ")
                {
                    Console.WriteLine("     {0}.[-- Empty --]", (i + 1));
                    continue;
                }

                if (player.Hand[i] is PersonCard)
                {
                    IPersonCard personCard = (IPersonCard)player.Hand[i];
                    Console.WriteLine("     " + (i + 1) + ".[" + personCard.Name + " : " + personCard.Level + " , " + personCard.BrainHealth + " , " + personCard.Analisis + " , " + personCard.Algebra + " , " + personCard.Pro + " , " + personCard.Logic + "]");
                }

                if (player.Hand[i] is SpecialCard)
                {
                    ISpecialCard specialCard = (ISpecialCard)player.Hand[i];
                    Console.WriteLine("     " + (i + 1) + ".[" + specialCard.Name + " : " + specialCard.Description + "]");
                }

            }
            #endregion

            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine();

            #region Acciones en el juego
            Console.WriteLine("Actions (Press the number)");
            Console.WriteLine("1.[Draw]  2.[Invoque]  3.[Attack]  4.[Active]  5.[End Turn]  6.[Descart]  0.[Exit]");
            string number = Console.ReadLine();
            switch (number)
            {
                case "1":
                    Draw(battle);
                    break;
                case "2":
                    Invoque(battle);
                    break;
                case "3":
                    Attack(battle);
                    break;
                case "4":
                    Active(battle);
                    break;
                case "5":
                    EndTurn(battle);
                    break;
                case "6":
                    Descart(battle);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
            #endregion

        }

        
        #region imprimir ganador
        int winnerIndex = Utils.GetWinnerIndex(battle.Status);

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine();
        Console.WriteLine("     The Winner is {0}", battle.Status.Players[winnerIndex].Name);
        Console.WriteLine();
        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine();
        #endregion


    }
    private void Draw(IBattle battle)
    {
        if (!battle.Status.CanDraw)
        {
            Console.WriteLine("You can only draw once time by turn");
            return;
        }

        battle.Draw();
    }
    private void Invoque(IBattle battle)
    {
        IPlayer player = battle.Status.PlayerPlay;
        IPlayerStatus[] players = battle.Status.Players;

        if (!battle.Status.CanInvoque)
        {
            Console.WriteLine("You can only invoque a card once time by turn");
            return;
        }

        #region pedir carta para invocar y valida la entrada
        Console.WriteLine("Which card do you want to invoque?. Please select the previous number.");
        string indexCardtoInvoque = Console.ReadLine();
        if (indexCardtoInvoque != "1" && indexCardtoInvoque != "2" && indexCardtoInvoque != "3" && indexCardtoInvoque != "4" && indexCardtoInvoque != "5")
        {
            Console.WriteLine("Invalid number");
            return;
        }
        if (!(player.Hand[Int32.Parse(indexCardtoInvoque) - 1] is PersonCard))
        {
            Console.WriteLine("You need to select a student or a professor");
            return;
        }
        #endregion

        #region definir si es estudiante o profesor
        if ((PersonCard)player.Hand[Int32.Parse(indexCardtoInvoque) - 1] is ProfessorCard)
        {
            if (battle.Status.PlayerPlay.Field.All(card => card.Name == " "))
            {
                Console.WriteLine("You can't invoque a professor without students on the Field");
                return;
            }

            IProfessorCard professor = (ProfessorCard)player.Hand[Int32.Parse(indexCardtoInvoque) - 1];
            Console.WriteLine("For invoque this professor you need to sacrifice {0} students on your field", professor.Level);

            IPersonCard[] students = new PersonCard[professor.Level];
            for (int i = 0; i < professor.Level; i++)
            {
                int choice;
                Console.WriteLine("Select the number of the student {0} to sacrificate", (i + 1));
                try
                {
                    choice = Int32.Parse(Console.ReadLine());
                    if (battle.Status.PlayerPlay.Field[choice - 1].Name == " ")
                    {
                        return;
                    }
                    students[i] = battle.Status.PlayerPlay.Field[choice - 1];
                }
                catch (System.Exception)
                {
                    Console.WriteLine("Invalid input");
                    return;
                }
            }

            battle.InvoqueProfessor(professor, students);
        }
        else
        {
            battle.Invoque((PersonCard)player.Hand[Int32.Parse(indexCardtoInvoque) - 1]);
        }
        #endregion

    }
    private void Attack(IBattle battle)
    {
        IPlayer player = battle.Status.PlayerPlay;
        IPlayerStatus[] players = battle.Status.Players;

        #region invalidar ataque con el campo vacio
        if (player.Field.All(e => e.Name == " "))
        {
            Console.WriteLine("You can't attack with an empty field");
            return;
        }
        #endregion

        #region invalidar ataque en el primer turno
        if (battle.Status.Turn == 1)
        {
            Console.WriteLine("You can't attack anybody in your first turn");
            return;
        }
        #endregion

        #region pedir asignatura a usar y validar la entrada
        Console.WriteLine("Select the subject to use in the attack");
        Console.WriteLine("1.[Analisis]  2.[Algebra]  3.[Programming]  4.[Logic]");
        string option = Console.ReadLine();
        string skill = " ";
        switch (option)
        {
            case "1":
                skill = "Analisis";
                break;
            case "2":
                skill = "Algebra";
                break;
            case "3":
                skill = "Programming";
                break;
            case "4":
                skill = "Logic";
                break;
            default:
                Console.WriteLine("Invalid subject");
                return;
        }
        #endregion

        #region pedir enemigo a atacar y validar la entrada
        string nameEnemy;
        if (players.Length == 2)
        {
            nameEnemy = players.First(p => p.Name != player.Name).Name;
        }
        else
        {
            Console.WriteLine("Which enemy do you want to atack?. Please write his name.");
            nameEnemy = Console.ReadLine();
            if (players.All(e => e.Name != nameEnemy))
            {
                Console.WriteLine("Name not found");
                return;
            }
            if (player.Name == nameEnemy)
            {
                Console.WriteLine("Did you really want to attack yourself ?");
                return;
            }
        }
        #endregion

        #region pedir PersonCard a usar y validad la entrada
        Console.WriteLine("Select the student or professor to use");
        string indexCardAttack = Console.ReadLine();
        if (indexCardAttack != "1" && indexCardAttack != "2" && indexCardAttack != "3" && indexCardAttack != "4" && indexCardAttack != "5")
        {
            Console.WriteLine("Invalid number");
            return;
        }

        if (player.Field[Int32.Parse(indexCardAttack) - 1].Used)
        {
            Console.WriteLine("This card can't attack anymore in this turn");
            return;
        }
        #endregion

        #region pedir PersonCard para atacarla y validar la entrada
        int indexCardTarget = -1;
        IPlayerStatus enemy = players.First(e => e.Name == nameEnemy);
        if (enemy.Field.Any(card => card.Name != " "))
        {
            Console.WriteLine("Select the student or professor to attack");
            string indexCardTarget_string = Console.ReadLine();
            if (indexCardTarget_string != "1" && indexCardTarget_string != "2" && indexCardTarget_string != "3" && indexCardTarget_string != "4" && indexCardTarget_string != "5")
            {
                Console.WriteLine("Invalid number");
                return;
            }

            indexCardTarget = Int32.Parse(indexCardTarget_string) - 1;

            if (enemy.Field[indexCardTarget].Name == " ")
            {
                Console.WriteLine("There is no card in this position in the field");
                return;
            }
        }
        #endregion


        //Ataque
        if (indexCardTarget == -1)
        {
            battle.Attack(enemy, (PersonCard)player.Field[Int32.Parse(indexCardAttack) - 1], skill);
            return;
        }
        else
        {
            battle.Attack(enemy, (PersonCard)player.Field[Int32.Parse(indexCardAttack) - 1], enemy.Field[indexCardTarget], skill);
        }

    }
    private void Active(IBattle battle)             // falta
    {
        IPlayer player = battle.Status.PlayerPlay;
        IPlayerStatus[] players = battle.Status.Players;

        #region pedir carta para activar y validar la entrada
        Console.WriteLine("Which card do you want to active?");
        string indexspecialCard = Console.ReadLine();
        if (indexspecialCard != "1" && indexspecialCard != "2" && indexspecialCard != "3" && indexspecialCard != "4" && indexspecialCard != "5")
        {
            Console.WriteLine("Invalid number");
            return;
        }
        if (!(player.Hand[Int32.Parse(indexspecialCard) - 1] is SpecialCard))
        {
            Console.WriteLine("You need to select a special card");
            return;
        }
        #endregion

        SpecialCard specialCard = (SpecialCard)player.Hand[Int32.Parse(indexspecialCard) - 1];

        #region acciones si la carta afecta a cartas enemigas
        IPersonCard[] enemysToAffect;

        if (specialCard.EnemysToAffect > 0)
        {
            Console.WriteLine("This card needs {0} cards to active", specialCard.EnemysToAffect);
            enemysToAffect = new IPersonCard[specialCard.EnemysToAffect];

            for (int a = 1; a <= specialCard.EnemysToAffect; a++)
            {
                #region seleccionar los players a quienes pertenecen las cartas que se afectarán
                Console.WriteLine("Select the player's name whom belong the card {0}", a);
                string namePlayerBelongCard = Console.ReadLine();
                if (players.All(e => e.Name != namePlayerBelongCard))
                {
                    Console.WriteLine("Name not found");
                    return;
                }
                if (player.Name == namePlayerBelongCard)
                {
                    Console.WriteLine("Did you really want to attack yourself ?");
                    return;
                }
                #endregion

                #region seleccionar las carta que se afectarán
                Console.WriteLine("Select a card{0} to affect", a);
                string indexPersonCardTarget = Console.ReadLine();
                if (indexPersonCardTarget != "1" && indexPersonCardTarget != "2" && indexPersonCardTarget != "3" && indexPersonCardTarget != "4" && indexPersonCardTarget != "5")
                {
                    Console.WriteLine("Invalid number");
                    return;
                }
                if ((players.First(e => e.Name == namePlayerBelongCard).Field[Int32.Parse(indexPersonCardTarget) - 1].Name == " "))
                {
                    Console.WriteLine("You need to select a student or a professor");
                    return;
                }
                #endregion

                enemysToAffect[a - 1] = players.First(e => e.Name == namePlayerBelongCard).Field[Int32.Parse(indexPersonCardTarget) - 1];
            }
        }
        else
        {
            enemysToAffect = Array.Empty<PersonCard>();
        }
        #endregion

        #region acciones si la carta afecta las cartas del jugador que está jugando
        IPersonCard[] alliesToAffect;

        if (specialCard.AlliesToAffect > 0)
        {
            Console.WriteLine("This card needs {0} of your cards on the Field to active", specialCard.AlliesToAffect);
            if (specialCard.AlliesToAffect > player.Field.Where(card => card.Name != " ").Count())
            {
                Console.WriteLine("You dont have enought cards on the field to activate this card");
                return;
            }

            alliesToAffect = new IPersonCard[specialCard.AlliesToAffect];

            for (int a = 1; a <= specialCard.AlliesToAffect; a++)
            {
                #region seleccionar la carta que se afectará
                Console.WriteLine("Select the number of the card that you want to affect");
                string indexAllie = Console.ReadLine();
                if (indexAllie != "1" && indexAllie != "2" && indexAllie != "3" && indexAllie != "4" && indexAllie != "5")
                {
                    Console.WriteLine("Invalid number");
                    return;
                }
                if (player.Field[Int32.Parse(indexAllie) - 1].Name == " ")
                {
                    Console.WriteLine("You need to select a student or a professor");
                    return;
                }
                #endregion

                alliesToAffect[a - 1] = player.Field[Int32.Parse(indexAllie) - 1];
            }
        }
        else
        {
            alliesToAffect = Array.Empty<PersonCard>();
        }
        #endregion

        // try
        // {
        //     battle.Active(specialCard, new Interpreter(interprete.Run), alliesToAffect, enemysToAffect);
        // }
        // catch (System.Exception)
        // {
        //     Console.WriteLine("Invalid code in card");
        // }
    }
    private void EndTurn(IBattle battle)
    {
        battle.EndTurn();
    }
    private void Descart(IBattle battle)
    {
        IPlayer player = battle.Status.PlayerPlay;

        Console.WriteLine("Which card do you want to descart?  (Please select the number of the card)");
        int choice;
        try
        {
            choice = Int32.Parse(Console.ReadLine());
        }
        catch (System.Exception)
        {
            Console.WriteLine("Invalid input");
            return;
        }
        if (player.Hand[choice - 1].Name == " ")
        {
            return;
        }

        player.Hand[choice - 1].Name = " ";
    }

}

