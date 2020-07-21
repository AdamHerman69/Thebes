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

    [Serializable]
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

    public interface IDigSite : IPlace
    {
        List<IToken> Tokens { get; }
    }

    [Serializable]
    public class DigSite : Place, IDigSite
    {
        public List<IToken> Tokens { get; set; }
        //private static Random random = new Random();

        public DigSite(string name, int index) : base(name, index)
        {
            Tokens = new List<IToken>();
        }
    }

    public interface ICardChangePlace : IPlace
    {

    }

    [Serializable]
    public class CardChangePlace : Place, ICardChangePlace
    {
        public CardChangePlace(string name, int index) : base(name, index) { }
    }

    public interface IUniversity : IPlace
    {

    }

    [Serializable]
    public class University : Place, IUniversity
    {
        public University(string name, int index) : base(name, index) { }
    }
}
