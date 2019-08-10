using Assets.Parsing.Attributes;
using Assets.Parsing.Nodes.Base;
using System;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Parsing.Nodes
{
    [NodeType("DECISION")]
    public class DecisionNode : MultiOptionNode
    {
        private int _selectedNode;

        public override string GetDisplayText()
        {
            // Decision nodes do not have node text - instead, display text is text of each button (child node).
            var sb = new StringBuilder();
            for(var i = 0; i < Children.Count; i++)
            {
                if (i == _selectedNode)
                    sb.Append("> ");

                else
                    sb.Append("  ");

                sb.Append(Children.ElementAt(i).NodeText);

                if (i != Children.Count)
                    sb.Append("\n");
            }
            return sb.ToString();
        }

        public override bool HandleKey(KeyCode key)
        {
            // Decision nodes must handle up and down arrow key inputs
            // for navigating the options in the dialgoue tree.
            if (key == KeyCode.DownArrow)
                _selectedNode++;
            else if (key == KeyCode.UpArrow)
                _selectedNode--;
            else
                return false;

            // Handle wraparound.
            if (_selectedNode < 0)
                _selectedNode = Children.Count - 1;
            else if (_selectedNode >= Children.Count)
                _selectedNode = 0;

            return true;
        }

        public override Node GetNext()
        {
            // Decision nodes have multiple decisions that must be selected by the user.
            return Children.ElementAt(_selectedNode);
        }

        public int GetSelectedDecision()
        {
            return _selectedNode;
        }

        public void SelectDecision(int index)
        {
            if (Children == null || Children.Count <= index)
                throw new InvalidOperationException($"Unable to select button {index} because it is out of bounds.");
            
            _selectedNode = index;
        }
    }
}
