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
    }
    public interface ITokenView : ItemView
    {
        IToken Token { get; }
    }

    public class SpecializedKnowledgeCardView : ICardView
    {
        public ISpecializedKnowledgeCard _Card { get; private set; }
        public ICard Card { get { return _Card; } }
        public SpecializedKnowledgeCardView(ISpecializedKnowledgeCard card)
        {
            this._Card = card;
        }
        
        public string FileName { get { return $"c_{_Card.Place}_sp_knowledge_{_Card.digSite}_{_Card.KnowledgeAmount}.png"; } }
    }

    public class GeneralKnowledgeCardView : ICardView
    {
        public IGeneralKnowledgeCard _Card { get; private set; }
        public ICard Card { get { return _Card; } }
        public GeneralKnowledgeCardView(IGeneralKnowledgeCard card)
        {
            this._Card = card;
        }

        public string FileName { get { return $"c_{_Card.Place}_g_knowledge_{_Card.KnowledgeAmount}.png"; } }
    }

    public class RumorsCardView : ICardView
    {
        public IRumorsCard _Card { get; private set; }
        public ICard Card { get { return _Card; } }
        public RumorsCardView(IRumorsCard card)
        {
            this._Card = card;
        }

        public string FileName { get { return $"c_{_Card.Place}_rumors_{_Card.digSite}_{_Card.KnowledgeAmount}.png"; } }
    }

    public class ZeppelinCardView : ICardView
    {
        public IZeppelinCard _Card { get; private set; }
        public ICard Card { get { return _Card; } }
        public ZeppelinCardView(IZeppelinCard card)
        {
            this._Card = card;
        }

        public string FileName { get { return $"c_{_Card.Place}_zeppelin.png"; } }
    }

    public class CarCardView : ICardView
    {
        public ICarCard _Card { get; private set; }
        public ICard Card { get { return _Card; } }
        public CarCardView(ICarCard card)
        {
            this._Card = card;
        }

        public string FileName { get { return $"c_{_Card.Place}_car.png"; } }
    }

    public class AssistantCardView : ICardView
    {
        public IAssistantCard _Card { get; private set; }
        public ICard Card { get { return _Card; } }
        public AssistantCardView(IAssistantCard card)
        {
            this._Card = card;
        }

        public string FileName { get { return $"c_{_Card.Place}_assistant.png"; } }
    }

    public class ShovelCardView : ICardView
    {
        public IShovelCard _Card { get; private set; }
        public ICard Card { get { return _Card; } }
        public ShovelCardView(IShovelCard card)
        {
            this._Card = card;
        }

        public string FileName { get { return $"c_{_Card.Place}_shovel.png"; } }
    }

    public class SpecialPermissionCardView : ICardView
    {
        public ISpecialPermissionCard _Card { get; private set; }
        public ICard Card { get { return _Card; } }
        public SpecialPermissionCardView(ISpecialPermissionCard card)
        {
            this._Card = card;
        }

        public string FileName { get { return $"c_{_Card.Place}_sp_permission.png"; } }
    }

    public class CongressCardView : ICardView
    {
        public ICongressCard _Card { get; private set; }
        public ICard Card { get { return _Card; } }
        public CongressCardView(ICongressCard card)
        {
            this._Card = card;
        }

        public string FileName { get { return $"c_{_Card.Place}_congress.png"; } }
    }

    public class ExhibitionCardView : ICardView
    {
        public IExhibitionCard _Card { get; private set; }
        public ICard Card { get { return _Card; } }
        public ExhibitionCardView(IExhibitionCard card)
        {
            this._Card = card;
        }

        public string FileName { get 
            {
                string fileName = $"c_{_Card.Place}_exhibition_{_Card.Points}";
                foreach (IDigSiteSimpleView requirement in _Card.ArtifactsRequired)
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
