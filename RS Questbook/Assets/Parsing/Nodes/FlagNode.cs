using Assets.Parsing.Attributes;
using Assets.Parsing.Nodes.Base;
using System;

namespace Assets.Parsing.Nodes
{
    [NodeType("FLAG")]
    public class FlagNode : LinearNode
    {
        public string FlagName { get; set; }
        public string FlagValue { get; set; }

        public FlagNode() : base()
        {
            this.IsDisplayed = false;
        }

        public override Node GetNext()
        {
            // Before moving to the next node, we want to actually set the flag.
            Flags.Set(FlagName, FlagValue);

            return base.GetNext();
        }

        protected override void ParseNodeText(string rawText)
        {
            var splitText = rawText.Split();
            if (splitText.Length != 2)
                throw new ArgumentException("Invalid FLAG statement found in script.");

            this.FlagName = splitText[0];
            this.FlagValue = splitText[1];
        }
    }
}
