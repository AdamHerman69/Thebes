using System;
using System.Collections.Generic;
using System.Text;

namespace ThebesCore
{
    public interface IPlace
    {
        int Index { get; set; }
        string Name { get; }
    }

    public abstract class Place : IPlace
    {
        public string Name { get; private set; }
        public int Index { get; set; }

        public Place(string name, int index)
        {
            Name = name;
            Index = index;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public interface IDigSiteSimpleView : IPlace
    {
        List<IToken> Tokens { get; }
    }

    public interface IDigSiteFullView : IDigSiteSimpleView
    {
        List<IToken> DrawTokens(int tokenAmount);
    }

    public class DigSite : Place, IDigSiteFullView
    {
        public List<IToken> Tokens { get; set; }
        private static Random random = new Random();

        public DigSite(string name, int index) : base(name, index)
        {
            Tokens = new List<IToken>();
        }

        public List<IToken> DrawTokens(int tokenAmount)
        {
            List<IToken> tokensDrawn = new List<IToken>();
            for (int i = 0; i < tokenAmount; i++)
            {
                IToken tokenDrawn = Tokens[random.Next(0, Tokens.Count)];
                if (! (tokenDrawn is IDirtToken))
                {
                    Tokens.Remove(tokenDrawn);
                }
                tokensDrawn.Add(tokenDrawn);
            }
            return tokensDrawn;
        }
    }

    public interface ICardChangePlace : IPlace
    {

    }

    public class CardChangePlace : Place, ICardChangePlace
    {
        public CardChangePlace(string name, int index) : base(name, index) { }
    }

    public interface IUniversity : IPlace
    {

    }

    public class University : Place, IUniversity
    {
        public University(string name, int index) : base(name, index) { }
    }
}
