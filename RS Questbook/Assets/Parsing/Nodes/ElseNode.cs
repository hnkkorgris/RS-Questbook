using Assets.Parsing.Attributes;
using Assets.Parsing.Nodes.Base;

namespace Assets.Parsing.Nodes
{
    [NodeType("ELSE")]
    public class ElseNode : EvaluatedNode
    {
        public override bool IsConditionTrue()
        {
            return true;
        }
    }
}
