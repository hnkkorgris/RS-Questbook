using Assets.Parsing.Attributes;
using Assets.Parsing.Nodes.Base;
using System;

namespace Assets.Parsing.Nodes
{
    [NodeType("CONDITION")]
    public class ConditionNode : MultiOptionNode
    {
        public ConditionNode() : base()
        {
            this.IsDisplayed = false;
        }

        public override Node GetNext()
        {
            foreach(var child in Children)
            {
                // Children of conditional node must all be ConditionNode type.
                if (!(child is EvaluatedNode condition))
                    throw new ArgumentException($"Unable to parse node of type {child.GetType()} under CONDITION statement.");

                if (condition.IsConditionTrue()) return child;
            }

            // If none of the child nodes evaluate to true, then just throw.
            throw new ArgumentException("No path in script found for condition. Please supply all paths or an ELSE statement.");
        }
    }
}
