﻿using System;
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

    public abstract class Item : IItem
    {
        public string Id { get; set; }

        public Item(string id)
        {
            this.Id = id;
        }

        public abstract void UpdateStats(IPlayer player);
        public abstract override string ToString();
    }

    public interface ICard : IItem
    {
        IUniversity Place { get; }
        int Weeks { get; }
        string Description { get; }
    }

    public abstract class Card : Item, ICard
    {
        public IUniversity Place { get; }
        public int Weeks { get; }
        public string Description { get { return ToString(); } }

        public Card(string id, IUniversity place, int weeks) : base(id)
        {
            this.Place = place;
            this.Weeks = weeks;
        }
    }

    public interface ISpecializedKnowledgeCard : ICard
    {
        IDigSiteSimpleView digSite { get; }
        int KnowledgeAmount { get; }
    }

    public class SpecializedKnowledgeCard : Card, ISpecializedKnowledgeCard
    {
        public int KnowledgeAmount { get; set; }
        public IDigSiteSimpleView digSite { get; set; }

        public SpecializedKnowledgeCard(string id, IUniversity place, int weeks, int knowledgeAmount, IDigSiteSimpleView digSite) : base(id, place, weeks)
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
        IDigSiteSimpleView digSite { get; set; }
        int KnowledgeAmount { get; set; }
    }

    public class RumorsCard : Card, IRumorsCard
    {
        public int KnowledgeAmount { get; set; }
        public IDigSiteSimpleView digSite { get; set; }

        public RumorsCard(string id, IUniversity place, int weeks, int knowledgeAmount, IDigSiteSimpleView digSite) : base(id, place, weeks)
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

    public interface IAssistentCard : ICard
    {
    }

    public class AssistentCard : Card, IAssistentCard
    {
        public AssistentCard(string id, IUniversity place, int weeks) : base(id, place, weeks) { }
        public override void UpdateStats(IPlayer player)
        {
            player.Assistents++;
        }

        public override string ToString()
        {
            return "assistent at " + this.Place + " for " + this.Weeks + " weeks";
        }
    }

    public interface IShovelCard : ICard
    {
    }

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

    public class CongressCard : Card, ICongressCard
    {
        public CongressCard(string id, IUniversity place, int weeks) : base(id, place, weeks) { }

        public override void UpdateStats(IPlayer player)
        {
            player.Congresses++;
        }

        public override string ToString()
        {
            return "congress at " + this.Place + " for " + this.Weeks + " weeks";
        }
    }

    public interface IExhibitionCard : ICard
    {
        Dictionary<IDigSiteSimpleView, int> ArtifactsRequired { get; }

        bool CheckRequiredArtifacts(Dictionary<IDigSiteSimpleView, List<IToken>> tokensObtained);
        bool IsSmallExhibition();
        int Points { get; }
    }

    public class ExhibitionCard : Card, IExhibitionCard
    {
        public int Points { get; private set; }
        public Dictionary<IDigSiteSimpleView, int> ArtifactsRequired { get; private set; }

        public ExhibitionCard(string id, IUniversity place, int weeks, int points, Dictionary<IDigSiteSimpleView, int> artifactsRequired) : base(id, place, weeks)
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

        public bool CheckRequiredArtifacts(Dictionary<IDigSiteSimpleView, List<IToken>> tokensObtained)
        {

            foreach (KeyValuePair<IDigSiteSimpleView, int> requirement in ArtifactsRequired)
            {
                if (requirement.Value > tokensObtained[requirement.Key].Where(x => x is IArtifactToken).Count())
                {
                    return false;
                }
            }
            return true;
        }

        public override string ToString()
        {
            string str = $"Exhibition +{Points} at {Place} for {Weeks} requiring: ";
            foreach (KeyValuePair<IDigSiteSimpleView, int> kvp in ArtifactsRequired)
            {
                if (kvp.Value > 0)
                {
                    str += $"{kvp.Value} {kvp.Key}, ";
                }
            }
            return str;
        }
    }

    public interface IToken : IItem
    {
        IDigSiteSimpleView DigSite { get; }
    }

    public interface ISpecializedKnowledgeToken : IToken
    {
        int KnowledgeAmount { get; }
        IDigSiteSimpleView KnowledgeDigSite { get; }
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


    public abstract class Token : Item, IToken
    {
        public IDigSiteSimpleView DigSite { get; private set; }

        public Token(string id, IDigSiteSimpleView digSite) : base(id)
        {
            this.DigSite = digSite;
        }
    }

    public class SpecializedKnowledgeToken : Token, ISpecializedKnowledgeToken
    {
        public int KnowledgeAmount { get; set; }
        public IDigSiteSimpleView KnowledgeDigSite { get; set; }

        public SpecializedKnowledgeToken(string id, IDigSiteSimpleView digSite, int knowledgeAmount, IDigSiteSimpleView knowledgeDigSite) : base(id, digSite)
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

    public class GeneralKnowledgeToken : Token, IGeneralKnowledgeToken
    {
        public int KnowledgeAmount { get; set; }

        public GeneralKnowledgeToken(string id, IDigSiteSimpleView digSite, int knowledgeAmount) : base(id, digSite)
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

    public class ArtifactToken : Token, IArtifactToken
    {
        public int Points { get; set; }
        public string Name { get; set; }

        public ArtifactToken(string id, IDigSiteSimpleView digSite, int points, string name) : base(id, digSite)
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

    public interface IDirtToken
    {
        void UpdateStats(IPlayer player);
    }

    public class DirtToken : Token, IDirtToken
    {
        public DirtToken(string id, IDigSiteSimpleView digSite) : base(id, digSite) { }
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