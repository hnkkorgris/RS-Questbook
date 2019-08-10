using Assets.Parsing.Attributes;
using Assets.Parsing.Nodes.Base;

namespace Assets.Parsing.Nodes
{
    [NodeType("BUTTON")]
    public class ButtonNode : BranchedNode
    {
        public ButtonNode() : base()
        {
            this.IsDisplayed = false;
        }
    }
}
