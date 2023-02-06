using System.Text.Json;
using System.Collections.Generic;
namespace MatcomFestival2._0.Logic;

public static class Utils
{
    public static bool ThereIsAWinner(IGameStatus game)
    {
        int count = 0;
        for (int i = 0; i < game.Players.Length; i++)
        {
            if (game.Players[i].Name != " ")
            {
                count++;
            }

            if (count > 1)
            {
                return false;
            }
        }
        return true;
    }
    public static int GetWinnerIndex(IGameStatus game)
    {
        for (int i = 0; i < game.Players.Length; i++)
        {
            if (game.Players[i].Name != " ")
            {
                return i;
            }
        }

        return -1;
    }
    public static bool AreRepeated<T>(T[] items) where T : IEquatable<T>
    {
        for (int i = 0; i + 1 < items.Length; i++)
        {
            if (items[(i + 1)..items.Length].Any(e => e.Equals(items[i])))
            {
                return true;
            }
        }
        return false;
    }
    public static bool IsEmpty(this ICard card)
    {
        if (card.Name == " ")
        {
            return true;
        }

        return false;
    }
    public static bool CardIsDead(IPersonCard card)
    {
        if (card.BrainHealth <= 0)
        {
            return true;
        }
        return false;
    }
    public static bool PlayerIsDead(IPlayerStatus player)
    {
        if (player.Life <= 0)
        {
            return true;
        }
        return false;
    }
    public static void RemoveCard(ICard card)
    {
        card.Name = " ";
    }
    public static void DestroyCard(IPersonCard card)
    {
        RemoveCard(card);
        card.BrainHealth = 0;
    }
    public static void DestroyPlayer(IPlayerStatus player)
    {
        player.Name = " ";
    }
    public static void UpdateGameStatus(IGame game, IGameStatus status)
    {
        status.Turn = game.Turn;
        status.CanInvoque = game.CanInvoque;
        status.CanDraw = game.CanDraw;
        status.PlayerIndex = game.PlayerIndex;

        status.PlayerPlay = (IPlayer)game.PlayerPlay.Clone();
        status.Players = (IPlayerStatus[])game.Players.Clone();
    }
    public static void AddCardToDeck(ICard card)
    {
        if (card is ProfessorCard)
        {
            IProfessorCard professor = (ProfessorCard)card;
            string cards = File.ReadAllText("MatcomFestival2.0.Logic/Data/ProfessorCards.json");
            ProfessorCard[] professorCards;
            if (cards == "")
            {
                professorCards = Array.Empty<ProfessorCard>();
            }
            else
            {
                professorCards = JsonSerializer.Deserialize<ProfessorCard[]>(cards);
            }

            IPersonCard[] newProfessors = new PersonCard[professorCards.Length + 1];
            for (int i = 0; i < professorCards.Length; i++)
            {
                newProfessors[i] = professorCards[i];
            }
            newProfessors[newProfessors.Length - 1] = professor;
            string json = JsonSerializer.Serialize(newProfessors);
            File.WriteAllText("MatcomFestival2.0.Logic/Data/ProfessorCards.json", json);
        }

        if (card is PersonCard)
        {
            IPersonCard student = (PersonCard)card;
            string cards = File.ReadAllText("MatcomFestival2.0.Logic/Data/PersonCards.json");
            IPersonCard[] persons = JsonSerializer.Deserialize<PersonCard[]>(cards);
            IPersonCard[] newPersons = new PersonCard[persons.Length + 1];
            for (int i = 0; i < persons.Length; i++)
            {
                newPersons[i] = persons[i];
            }
            newPersons[newPersons.Length - 1] = student;
            string json = JsonSerializer.Serialize(newPersons);
            File.WriteAllText("MatcomFestival2.0.Logic/Data/PersonCards.json", json);
        }

        if (card is SpecialCard)
        {
            ISpecialCard special = (SpecialCard)card;
            string cardsSpecial = File.ReadAllText("MatcomFestival2.0.Logic/Data/SpecialCards.json");

            if (cardsSpecial == "")
            {
                ISpecialCard[] arr = new ISpecialCard[1] { special };
                string cardString = JsonSerializer.Serialize(arr);
                File.WriteAllText("MatcomFestival2.0.Logic/Data/SpecialCards.json", cardString);
            }
            else
            {
                ISpecialCard[] specials = JsonSerializer.Deserialize<SpecialCard[]>(cardsSpecial);
                ISpecialCard[] specials2 = new SpecialCard[specials.Length + 1];
                for (int i = 0; i < specials.Length; i++)
                {
                    specials2[i] = specials[i];
                }
                specials2[specials2.Length - 1] = special;
                string json = JsonSerializer.Serialize(specials2);
                File.WriteAllText("MatcomFestival2.0.Logic/Data/SpecialCards.json", json);
            }
        }
    }
}

public static class AntiHack
{
    public static bool IsOnHand(IPlayer player, ICard card)
    {
        foreach (var item in player.Hand)
        {
            if (card is PersonCard && ((PersonCard)item).Equals((PersonCard)card))
            {
                return true;
            }

            if (card is SpecialCard && ((SpecialCard)item).Equals((SpecialCard)card))
            {
                return true;
            }
        }
        return false;
    }
    public static bool IsOnField(IPlayerStatus player, params IPersonCard[] persons)
    {
        for (int i = 0; i < persons.Length; i++)
        {
            bool isOnField = false;
            for (int j = 0; j < player.Field.Length; j++)
            {
                if (player.Field[j].Equals(persons[i]))
                {
                    isOnField = true;
                    break;
                }
            }

            if (!isOnField)
            {
                return false;
            }

        }
        return true;
    }
    public static bool IsAnEnemy(IGame game, IPlayerStatus enemy)
    {
        if (game.Players[game.PlayerIndex].Equals(enemy))
        {
            return false;
        }

        foreach (var item in game.Players)
        {
            if (item.Equals(enemy))
            {
                return true;
            }
        }

        return false;
    }
    public static bool IsOnEnemysField(IGame game, params IPersonCard[] persons)
    {
        foreach (var card in persons)
        {
            bool cardFound = false;
            foreach (var player in game.Players)
            {
                if (!IsAnEnemy(game, player))
                {
                    continue;
                }

                if (IsOnField(player, card))
                {
                    cardFound = true;
                    break;
                }
            }
            if (!cardFound)
            {
                return false;
            }
        }
        return true;
    }
    public static bool FieldIsEmpty(IPlayerStatus player)
    {
        return player.Field.All(c => c.Name == " ");
    }


}

