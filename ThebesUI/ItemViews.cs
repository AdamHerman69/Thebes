using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThebesCore;

namespace ThebesUI
{
    public interface ItemView
    {
        string FileName { get; }
    }

    public interface ICardView : ItemView
    {
        ICard Card { get; }
        string Description { get; }
    }
    public interface ITokenView : ItemView
    {
        IToken Token { get; }
    }

    public class SpecializedKnowledgeCardView : ICardView
    {
        public string Description { get; }
        public ISpecializedKnowledgeCard _Card { get; private set; }
        public ICard Card { get { return _Card; } }
        public SpecializedKnowledgeCardView(ISpecializedKnowledgeCard card)
        {
            this._Card = card;
            this.Description = $"Gives you {_Card.KnowledgeAmount} knowledge about {_Card.digSite}.\nAt least one specialized knowledge is required to dig at any digsite.";
        }
        
        public string FileName { get { return $"c_{_Card.Place}_sp_knowledge_{_Card.digSite}_{_Card.KnowledgeAmount}.png"; } }
    }

    public class GeneralKnowledgeCardView : ICardView
    {
        public string Description { get; }
        public IGeneralKnowledgeCard _Card { get; private set; }
        public ICard Card { get { return _Card; } }
        public GeneralKnowledgeCardView(IGeneralKnowledgeCard card)
        {
            this._Card = card;
            this.Description = $"Gives you {_Card.KnowledgeAmount} general knowledge.\nGeneral knowledge can be used at any digsite, but does not contribute to the total knowledge amount to recieve points at the end of the game.";
        }

        public string FileName { get { return $"c_{_Card.Place}_g_knowledge_{_Card.KnowledgeAmount}.png"; } }
    }

    public class RumorsCardView : ICardView
    {
        public string Description { get; }
        public IRumorsCard _Card { get; private set; }
        public ICard Card { get { return _Card; } }
        public RumorsCardView(IRumorsCard card)
        {
            this._Card = card;
            this.Description = $"Gives you {_Card.KnowledgeAmount} single-use knowledge about {_Card.digSite}.\nSingle-use knowledge is same as specialized knowledge, but you can use it only once.";
        }

        public string FileName { get { return $"c_{_Card.Place}_rumors_{_Card.digSite}_{_Card.KnowledgeAmount}.png"; } }
    }

    public class ZeppelinCardView : ICardView
    {
        public string Description { get; }
        public IZeppelinCard _Card { get; private set; }
        public ICard Card { get { return _Card; } }
        public ZeppelinCardView(IZeppelinCard card)
        {
            this._Card = card;
            this.Description = $"Zeppelin\nYou can use a zeppelin once, to travel any distance instantly without spending any weeks.";
        }

        public string FileName { get { return $"c_{_Card.Place}_zeppelin.png"; } }
    }

    public class CarCardView : ICardView
    {
        public string Description { get; }
        public ICarCard _Card { get; private set; }
        public ICard Card { get { return _Card; } }
        public CarCardView(ICarCard card)
        {
            this._Card = card;
            this.Description = $"Car\nAnytime you travel for more than 3 weeks without stopping. The journey will take one week less";
        }

        public string FileName { get { return $"c_{_Card.Place}_car.png"; } }
    }

    public class AssistantCardView : ICardView
    {
        public string Description { get; }
        public IAssistantCard _Card { get; private set; }
        public ICard Card { get { return _Card; } }
        public AssistantCardView(IAssistantCard card)
        {
            this._Card = card;
            this.Description = $"Assistant\nTwo assistants equal one general knowledge, three assistants equal two general knowledge.";
        }

        public string FileName { get { return $"c_{_Card.Place}_assistant.png"; } }
    }

    public class ShovelCardView : ICardView
    {
        public string Description { get; }
        public IShovelCard _Card { get; private set; }
        public ICard Card { get { return _Card; } }
        public ShovelCardView(IShovelCard card)
        {
            this._Card = card;
            this.Description = $"Shovel\nTwo shovels give you one token bonus on every dig, three give you two.";
        }

        public string FileName { get { return $"c_{_Card.Place}_shovel.png"; } }
    }

    public class SpecialPermissionCardView : ICardView
    {
        public string Description { get; }
        public ISpecialPermissionCard _Card { get; private set; }
        public ICard Card { get { return _Card; } }
        public SpecialPermissionCardView(ISpecialPermissionCard card)
        {
            this._Card = card;
            this.Description = $"You can use this card to dig at a digsite you no longer have permission for. (Single-use)";
        }

        public string FileName { get { return $"c_{_Card.Place}_sp_permission.png"; } }
    }

    public class CongressCardView : ICardView
    {
        public string Description { get; }
        public ICongressCard _Card { get; private set; }
        public ICard Card { get { return _Card; } }
        public CongressCardView(ICongressCard card)
        {
            this._Card = card;
            this.Description = $"For every congress card you take, you'll get points equal to the amount of congress cards you have (including the one you just took)";
        }

        public string FileName { get { return $"c_{_Card.Place}_congress.png"; } }
    }

    public class ExhibitionCardView : ICardView
    {
        public string Description { get; }
        public IExhibitionCard _Card { get; private set; }
        public ICard Card { get { return _Card; } }
        public ExhibitionCardView(IExhibitionCard card)
        {
            this._Card = card;
            this.Description = $"You can execute an exhibition if you have the artifacts required. You'll get {_Card.Points} points for it";
        }

        public string FileName { get 
            {
                string fileName = $"c_{_Card.Place}_exhibition_{_Card.Points}";
                foreach (IDigSite requirement in _Card.ArtifactsRequired)
                {
                    fileName += "_" + requirement;
                }
                
                return fileName + ".png"; 
            } }
    }


    // ----------------------------------TOKENS------------------------------------------------






    public class SpecializedKnowledgeTokenView : ITokenView
    {
        public ISpecializedKnowledgeToken _Token { get; private set; }
        public IToken Token { get { return _Token; } }
        public SpecializedKnowledgeTokenView(ISpecializedKnowledgeToken token)
        {
            this._Token = token;
        }

        public string FileName { get { return $"t_{_Token.DigSite}_sp_knowledge_{_Token.KnowledgeDigSite}_{_Token.KnowledgeAmount}.png"; } }
    }

    public class GeneralKnowledgeTokenView : ITokenView
    {
        public IGeneralKnowledgeToken _Token { get; private set; }
        public IToken Token { get { return _Token; } }
        public GeneralKnowledgeTokenView(IGeneralKnowledgeToken token)
        {
            this._Token = token;
        }

        public string FileName { get { return $"t_{_Token.DigSite}_g_knowledge.png"; } }
    }

    public class ArtifactTokenView : ITokenView
    {
        public IArtifactToken _Token { get; private set; }
        public IToken Token { get { return _Token; } }
        public ArtifactTokenView(IArtifactToken token)
        {
            this._Token = token;
        }

        public string FileName { get { return $"t_{_Token.DigSite}_artifact_{_Token.Points}_{_Token.Name}.png"; } }
    }

    public class DirtTokenView : ITokenView
    {
        public IDirtToken _Token { get; private set; }
        public IToken Token { get { return _Token; } }
        public DirtTokenView(IDirtToken token)
        {
            this._Token = token;
        }

        public string FileName { get { return $"t_{_Token.DigSite}_dirt.png"; } }
    }
}
