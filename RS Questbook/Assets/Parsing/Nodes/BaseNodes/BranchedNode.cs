using System.Collections.Generic;

namespace Assets.Parsing.Nodes.Base
{
    public abstract class BranchedNode : LinearNode
    {
        // A button can be a leaf if its depth is equal to next line depth,
        // in addition to base class logic.
        protected override bool IsLeaf(Queue<string> lines)
        {
            return base.IsLeaf(lines) || this.Depth == ParseLineDepth(lines.Peek());
        }
    }
}
