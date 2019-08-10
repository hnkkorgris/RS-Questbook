using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Parsing.Nodes.Base
{
    public abstract class LinearNode : Node
    {
        public override Node GetNext()
        {
            // Single option nodes only ever have one child.
            return Children.SingleOrDefault();
        }

        public override Node BuildTree(Queue<string> lines)
        {
            if (IsLeaf(lines)) return this;

            // For dialogue nodes, there is only one child.
            var nodeToAdd = Node.Create(lines.Dequeue());
            this.Children.Add(nodeToAdd.BuildTree(lines));
            return this;
        }
    }
}
