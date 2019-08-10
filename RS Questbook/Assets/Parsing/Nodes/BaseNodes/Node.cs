using Assets.Models;
using Assets.Parsing.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Assets.Parsing.Nodes.Base
{
    public abstract class Node
    {
        public string NodeText;
        public List<Node> Children;
        public int Depth;
        public bool IsDisplayed = true;

        private static readonly IEnumerable<Type> NodeTypes = Assembly.GetAssembly(typeof(Node)).GetTypes()
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Node)));

        public Node()
        {
            Children = new List<Node>();
        }

        public static Node Create(string line)
        {
            // Each line is split into instruction and content, separated by colon.
            // Use first colon in line to split.
            var splitLine = line.Split(":".ToCharArray(), 2);
            var instruction = splitLine[0];
            var content = splitLine[1];

            // Use reflection to resolve a node of the specified type.
            Node node = null;
            foreach(var nodeType in NodeTypes)
            {
                var typeString = nodeType.GetCustomAttribute<NodeTypeAttribute>().Name;
                if(typeString.Equals(instruction.Trim(), StringComparison.InvariantCultureIgnoreCase))
                {
                    node = Activator.CreateInstance(nodeType) as Node;
                    break;
                }
            }

            // All other instructions are treated as dialogue.
            if (node == null)
                node = new DialogueNode(instruction.Trim());

            // Set common properties.
            node.ParseNodeText(content.Trim());
            node.Depth = ParseLineDepth(line);

            return node;
        }

        public virtual string GetDisplayText()
        {
            // For most nodes, the node text is the display text.
            return NodeText;
        }

        public virtual Character GetCharacter()
        {
            // Most nodes have no character sprite, so return null.
            return null;
        }

        public virtual bool HandleKey(KeyCode key)
        {
            // By default, don't handle any key codes.
            return false;
        }

        public abstract Node GetNext();

        public abstract Node BuildTree(Queue<string> lines);

        public List<Node> GetLeaves()
        {
            // Base case: current node is a leaf.
            if (!this.Children.Any()) return new List<Node> { this };

            // Recurse all children to get their leaves.
            var leaves = new List<Node>();
            foreach(var child in this.Children)
            {
                leaves.AddRange(child.GetLeaves());
            }
            return leaves;
        }

        protected virtual bool IsLeaf(Queue<string> lines)
        {
            // If script is ended or if root node is deeper than line, then this is a leaf.
            return !lines.Any() || this.Depth > ParseLineDepth(lines.Peek());
        }

        protected virtual void ParseNodeText(string rawText)
        {
            // By default, just set the node text directly.
            this.NodeText = rawText;
        }

        protected static int ParseLineDepth(string line)
        {
            // Every line in the script has a depth, delimited by tabs.
            return line.LastIndexOf('\t') + 1;
        }
    }
}
