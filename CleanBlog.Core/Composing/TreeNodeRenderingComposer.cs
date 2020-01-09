using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Services;
using Umbraco.Web.Trees;

namespace CleanBlog.Core.Composing
{

    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class TreeNodeRenderingComposer : ComponentComposer<TreeNodeRenderingComponent>, IUserComposer
    { }

    public class TreeNodeRenderingComponent : IComponent
    {
        private readonly IContentTypeService _contentTypeService;

        public TreeNodeRenderingComponent(IContentTypeService contentTypeService)
        {
            _contentTypeService = contentTypeService;
        }

        public void Initialize()
        {
            TreeControllerBase.TreeNodesRendering += TreeControllerBase_TreeNodesRendering;
        }

        public void Terminate()
        { }

        private void TreeControllerBase_TreeNodesRendering(TreeControllerBase sender, TreeNodesRenderingEventArgs e)
        {
            if (sender.TreeAlias == "documentTypes")
            {
                var contentTypes = _contentTypeService.GetAll().ToDictionary(c => c.Id);
                foreach (var node in e.Nodes)
                {
                    if (node.NodeType == "documentTypes")
                    {
                        if (int.TryParse(node.Id.ToString(), out var nodeId) && nodeId > 0)
                        {
                            var contentType = contentTypes[nodeId];
                            node.Icon = contentType.Icon;
                        }
                    }
                }
            }
        }
    }
}