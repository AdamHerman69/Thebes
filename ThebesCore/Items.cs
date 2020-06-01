using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThebesCore
{
    public interface IItem
    {
        string Id { get; set; }

        void UpdateStats(IPlayer player);
    }

    [Serializable]
    public abstract class Item : IItem
    {
        public string Id { get; set; }

        public Item(string id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Updates the player data with the effect of the item
        /// </summary>
        /// <param name="player"></param>
        public abstract void UpdateStats(IPlayer player);
        public abstract override string ToString();
    }






    // ----------------------------------CARDS------------------------------------------------






    public interface ICard : IItem
    {
        IUniversity Place { get; }
        int Weeks { get; }
    }

    [Serializable]
    public abstract class Card : Item, ICard
    {
        public IUniversity Place { get; }
        public int Weeks { get; }

        public Card(string id, IUniversity place, int weeks) : base(id)
        {
            this.Place = place;
            this.Weeks = weeks;
        }
    }

    public interface ISpecializedKnowledgeCard : ICard
    {
        IDigSite digSite { get; }
        int KnowledgeAmount { get; }
    }

    [Serializable]
    public class SpecializedKnowledgeCard : Card, ISpecializedKnowledgeCard
    {
        public int KnowledgeAmount { get; set; }
        public IDigSite digSite { get; set; }

        public SpecializedKnowledgeCard(string id, IUniversity place, int weeks, int knowledgeAmount, IDigSite digSite) : base(id, place, weeks)
        {
            this.KnowledgeAmount = knowledgeAmount;
            this.digSite = digSite;
        }

        public override void UpdateStats(IPlayer player)
        {
            player.SpecializedKnowledge[digSite] += KnowledgeAmount;
        }

        public override string ToString()
        {
            return "+ " + KnowledgeAmount + " " + digSite.Name + " at " + this.Place + " for " + this.Weeks + " weeks";
        }
    }

    public interface IGeneralKnowledgeCard : ICard
    {
        int KnowledgeAmount { get; set; }
    }

    [Serializable]
    public class GeneralKnowledgeCard : Card, IGeneralKnowledgeCard
    {
        public int KnowledgeAmount { get; set; }

        public GeneralKnowledgeCard(string id, IUniversity place, int weeks, int knowledgeAmount) : base(id, place, weeks)
        {
            this.KnowledgeAmount = knowledgeAmount;
        }

        public override void UpdateStats(IPlayer player)
        {
            player.GeneralKnowledge += KnowledgeAmount;
        }

        public override string ToString()
        {
            return "+ " + KnowledgeAmount + " general at " + this.Place + " for " + this.Weeks + " weeks";
        }
    }

    public interface IRumorsCard : ICard
    {
        IDigSite digSite { get; set; }
        int KnowledgeAmount { get; set; }
    }

    [Serializable]
    public class RumorsCard : Card, IRumorsCard
    {
        public int KnowledgeAmount { get; set; }
        public IDigSite digSite { get; set; }

        public RumorsCard(string id, IUniversity place, int weeks, int knowledgeAmount, IDigSite digSite) : base(id, place, weeks)
        {
            this.KnowledgeAmount = knowledgeAmount;
            this.digSite = digSite;
        }

        public override void UpdateStats(IPlayer player)
        {
            player.SingleUseKnowledge[digSite] += KnowledgeAmount;
        }

        public override string ToString()
        {
            return "+ " + KnowledgeAmount + " " + digSite.Name + " rumors at " + this.Place + " for " + this.Weeks + " weeks";
        }
    }

    public interface IZeppelinCard : ICard
    {
    }

    [Serializable]
    public class ZeppelinCard : Card, IZeppelinCard
    {
        public ZeppelinCard(string id, IUniversity place, int weeks) : base(id, place, weeks) { }
        public override void UpdateStats(IPlayer player)
        {
            player.Zeppelins++;
        }

        public override string ToString()
        {
            return "zeppelin at " + this.Place + " for " + this.Weeks + " weeks";
        }
    }

    public interface ICarCard : ICard
    {
    }

    [Serializable]
    public class CarCard : Card, ICarCard
    {
        public CarCard(string id, IUniversity place, int weeks) : base(id, place, weeks) { }

        public override void UpdateStats(IPlayer player)
        {
            player.Cars++;
        }

        public override string ToString()
        {
            return "car at " + this.Place + " for " + this.Weeks + " weeks";
        }
    }

    public interface IAssistantCard : ICard
    {
    }

    [Serializable]
    public class AssistantCard : Card, IAssistantCard
    {
        public AssistantCard(string id, IUniversity place, int weeks) : base(id, place, weeks) { }
        public override void UpdateStats(IPlayer player)
        {
            player.Assistants++;
        }

        public override string ToString()
        {
            return "assistant at " + this.Place + " for " + this.Weeks + " weeks";
        }
    }

    public interface IShovelCard : ICard
    {
    }

    [Serializable]
    public class ShovelCard : Card, IShovelCard
    {
        public ShovelCard(string id, IUniversity place, int weeks) : base(id, place, weeks) { }

        public override void UpdateStats(IPlayer player)
        {
            player.Shovels++;
        }

        public override string ToString()
        {
            return "shovel at " + this.Place + " for " + this.Weeks + " weeks";
        }
    }

    public interface ISpecialPermissionCard : ICard
    {
    }

    [Serializable]
    public class SpecialPermissionCard : Card, ISpecialPermissionCard
    {
        public SpecialPermissionCard(string id, IUniversity place, int weeks) : base(id, place, weeks) { }

        public override void UpdateStats(IPlayer player)
        {
            player.SpecialPermissions++;
        }

        public override string ToString()
        {
            return "permission at " + this.Place + " for " + this.Weeks + " weeks";
        }
    }

    public interface ICongressCard : ICard
    {
    }

    [Serializable]
    public class CongressCard : Card, ICongressCard
    {
        public CongressCard(string id, IUniversity place, int weeks) : base(id, place, weeks) { }

        public override void UpdateStats(IPlayer player)
        {
            player.Congresses++;
            if (player.Congresses <= 7)
            {
                player.Points += player.Congresses;
            }
        }

        public override string ToString()
        {
            return "congress at " + this.Place + " for " + this.Weeks + " weeks";
        }
    }

    public interface IExhibitionCard : ICard
    {
        List<IDigSite> ArtifactsRequired { get; }

        bool CheckRequiredArtifacts(Dictionary<IDigSite, List<IToken>> tokensObtained);
        bool CheckRequiredArtifacts(Func<IDigSite, double> tokensObtained);
        bool IsSmallExhibition();
        int Points { get; }
    }

    [Serializable]
    public class ExhibitionCard : Card, IExhibitionCard
    {
        public int Points { get; private set; }
        public List<IDigSite> ArtifactsRequired { get; private set; }

        public ExhibitionCard(string id, IUniversity place, int weeks, int points, List<IDigSite> artifactsRequired) : base(id, place, weeks)
        {
            this.Points = points;
            this.ArtifactsRequired = artifactsRequired;
        }

        public override void UpdateStats(IPlayer player)
        {
            player.Points += Points;
        }

        public bool IsSmallExhibition()
        {
            return Points < 5;
        }

        public bool CheckRequiredArtifacts(Dictionary<IDigSite, List<IToken>> tokensObtained)
        {
            foreach (IDigSite requirement in ArtifactsRequired)
            {
                if (ArtifactsRequired.Where(x => x == requirement).Count() > tokensObtained[requirement].Where(y => y is IArtifactToken).Count())
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckRequiredArtifacts(Func<IDigSite, double> tokensObtained)
        {
            foreach (IDigSite requirement in ArtifactsRequired)
            {
                if (ArtifactsRequired.Where(x => x == requirement).Count() > tokensObtained(requirement))
                {
                    return false;
                }
            }
            return true;
        }

        public override string ToString()
        {
            string str = $"Exhibition +{Points} at {Place} for {Weeks} requiring: ";
            foreach (IDigSite digSite in ArtifactsRequired)
            {
                str += $"{digSite.Name}, ";

            }
            return str;
        }
    }






    // ----------------------------------TOKENS------------------------------------------------






    public interface IToken : IItem
    {
        IDigSite DigSite { get; }
    }

    public interface ISpecializedKnowledgeToken : IToken
    {
        int KnowledgeAmount { get; }
        IDigSite KnowledgeDigSite { get; }
    }

    public interface IGeneralKnowledgeToken : IToken
    {
        int KnowledgeAmount { get; }
    }

    public interface IArtifactToken : IToken
    {
        int Points { get; }
        string Name { get; }
    }


    [Serializable]
    public abstract class Token : Item, IToken
    {
        public IDigSite DigSite { get; private set; }

        public Token(string id, IDigSite digSite) : base(id)
        {
            this.DigSite = digSite;
        }
    }

    [Serializable]
    public class SpecializedKnowledgeToken : Token, ISpecializedKnowledgeToken
    {
        public int KnowledgeAmount { get; set; }
        public IDigSite KnowledgeDigSite { get; set; }

        public SpecializedKnowledgeToken(string id, IDigSite digSite, int knowledgeAmount, IDigSite knowledgeDigSite) : base(id, digSite)
        {
            this.KnowledgeAmount = knowledgeAmount;
            this.KnowledgeDigSite = knowledgeDigSite;
        }

        public override void UpdateStats(IPlayer player)
        {
            player.SpecializedKnowledge[KnowledgeDigSite] += KnowledgeAmount;
        }

        public override string ToString()
        {
            return "+" + KnowledgeAmount + KnowledgeDigSite;
        }
    }

    [Serializable]
    public class GeneralKnowledgeToken : Token, IGeneralKnowledgeToken
    {
        public int KnowledgeAmount { get; set; }

        public GeneralKnowledgeToken(string id, IDigSite digSite, int knowledgeAmount) : base(id, digSite)
        {
            this.KnowledgeAmount = knowledgeAmount;
        }

        public override void UpdateStats(IPlayer player)
        {
            player.GeneralKnowledge += KnowledgeAmount;
        }

        public override string ToString()
        {
            return "+" + KnowledgeAmount + "general";
        }
    }

    [Serializable]
    public class ArtifactToken : Token, IArtifactToken
    {
        public int Points { get; set; }
        public string Name { get; set; }

        public ArtifactToken(string id, IDigSite digSite, int points, string name) : base(id, digSite)
        {
            this.Points = points;
            this.Name = name;
        }

        public override void UpdateStats(IPlayer player)
        {
            player.Points += Points;
        }

        public override string ToString()
        {
            return "+" + Points + "p";
        }
    }

    public interface IDirtToken : IToken
    {
    }

    [Serializable]
    public class DirtToken : Token, IDirtToken
    {
        public DirtToken(string id, IDigSite digSite) : base(id, digSite) { }
        public override void UpdateStats(IPlayer player)
        {
            // player's stats don't change
        }

        public override string ToString()
        {
            return "dirt";
        }
    }
}