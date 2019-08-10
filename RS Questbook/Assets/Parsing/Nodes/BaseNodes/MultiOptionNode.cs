using System.Collections.Generic;
using System.Linq;

namespace Assets.Parsing.Nodes.Base
{
    public abstract class MultiOptionNode : Node
    {
        public override Node BuildTree(Queue<string> lines)
        {
            if (IsLeaf(lines)) return this;

            // For multi-option nodes, there are many children.
            // Continuously read child subtrees until a child is found with same depth as this.
            var nextNode = Node.Create(lines.Dequeue());
            while (nextNode.Depth > this.Depth)
            {
                this.Children.Add(nextNode.BuildTree(lines));

                // It is possible to end script inside a decision.
                // If this happens, return without adding children to leaves.
                if (!lines.Any()) return this;

                nextNode = Node.Create(lines.Dequeue());
            }

            // Once we return to the original common thread, we must append it to each leaf node created.
            foreach (var leaf in this.GetLeaves())
            {
                leaf.Children.Add(nextNode.BuildTree(lines));
            }

            return this;
        }
    }
}
