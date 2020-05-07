using System;
using System.Collections.Generic;
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

    public class SpecializedKnowledgeCardView : SpecializedKnowledgeCard, ItemView
    {
        public SpecializedKnowledgeCardView(string id, IUniversity place, int weeks, int knowledgeAmount, IDigSiteSimpleView digSite)
            : base(id, place, weeks, knowledgeAmount, digSite) { }
        
        public string FileName { get { return $"c_{Place}_sp_knowledge_{digSite}_{KnowledgeAmount}.png"; } }
    }

    public class GeneralKnowledgeCardView : GeneralKnowledgeCard, ItemView
    {
        public GeneralKnowledgeCardView(string id, IUniversity place, int weeks, int knowledgeAmount)
            : base(id, place, weeks, knowledgeAmount) { }

        public string FileName { get { return $"c_{Place}_g_knowledge_{KnowledgeAmount}.png"; } }
    }

    public class RumorsCardView : RumorsCard, ItemView
    {
        public RumorsCardView(string id, IUniversity place, int weeks, int knowledgeAmount, IDigSiteSimpleView digSite)
            : base(id, place, weeks, knowledgeAmount, digSite) { }

        public string FileName { get { return $"c_{Place}_rumors_{digSite}_{KnowledgeAmount}.png"; } }
    }

    public class ZeppelinCardView : ZeppelinCard, ItemView
    {
        public ZeppelinCardView(string id, IUniversity place, int weeks)
            : base(id, place, weeks) { }

        public string FileName { get { return $"c_{Place}_zeppelin.png"; } }
    }

    public class CarCardView : CarCard, ItemView
    {
        public CarCardView(string id, IUniversity place, int weeks)
            : base(id, place, weeks) { }

        public string FileName { get { return $"c_{Place}_car.png"; } }
    }

    public class AssistentCardView : AssistentCard, ItemView
    {
        public AssistentCardView(string id, IUniversity place, int weeks)
            : base(id, place, weeks) { }

        public string FileName { get { return $"c_{Place}_assistent.png"; } }
    }

    public class ShovelCardView : ShovelCard, ItemView
    {
        public ShovelCardView(string id, IUniversity place, int weeks)
            : base(id, place, weeks) { }

        public string FileName { get { return $"c_{Place}_shovel.png"; } }
    }

    public class SpecialPermissionCardView : SpecialPermissionCard, ItemView
    {
        public SpecialPermissionCardView(string id, IUniversity place, int weeks)
            : base(id, place, weeks) { }

        public string FileName { get { return $"c_{Place}_sp_permission.png"; } }
    }

    public class CongressCardView : CongressCard, ItemView
    {
        public CongressCardView(string id, IUniversity place, int weeks)
            : base(id, place, weeks) { }

        public string FileName { get { return $"c_{Place}_congress.png"; } }
    }

    public class ExhibitionCardView : ExhibitionCard, ItemView
    {
        public ExhibitionCardView(string id, IUniversity place, int weeks, int points, List<IDigSiteSimpleView>artifactsRequired)
            : base(id, place, weeks, points, artifactsRequired) { }

        public string FileName { get 
            {
                string fileName = $"c_{Place}_exhibition_{Points}";
                foreach (IDigSiteSimpleView requirement in ArtifactsRequired)
                {
                    fileName += "_" + requirement;
                }
                
                return fileName + ".png"; 
            } }
    }






    // ----------------------------------TOKENS------------------------------------------------






    public class SpecializedKnowledgeTokenView : SpecializedKnowledgeToken, ItemView
    {
        public SpecializedKnowledgeTokenView(string id, IDigSiteSimpleView digSite, int knowledgeAmount, IDigSiteSimpleView knowledgeDigSite)
            : base(id, digSite, knowledgeAmount, knowledgeDigSite) { }

        public string FileName { get { return $"t_{DigSite}_sp_knowledge_{KnowledgeDigSite}_{KnowledgeAmount}.png"; } }
    }

    public class GeneralKnowledgeTokenView : GeneralKnowledgeToken, ItemView
    {
        public GeneralKnowledgeTokenView(string id, IDigSiteSimpleView digSite, int knowledgeAmount)
            : base(id, digSite, knowledgeAmount) { }

        public string FileName { get { return $"t_{DigSite}_g_knowledge.png"; } }
    }

    public class ArtifactTokenView : ArtifactToken, ItemView
    {
        public ArtifactTokenView(string id, IDigSiteSimpleView digSite, int points, string name)
            : base(id, digSite, points, name) { }

        public string FileName { get { return $"t_{DigSite}_artifact_{Points}_{Name}.png"; } }
    }

    public class DirtTokenView : DirtToken, ItemView
    {
        public DirtTokenView(string id, IDigSiteSimpleView digSite)
            : base(id, digSite) { }

        public string FileName { get { return $"t_{DigSite}_dirt.png"; } }
    }
}
