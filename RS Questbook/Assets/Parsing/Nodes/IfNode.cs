using Assets.Parsing.Attributes;
using Assets.Parsing.Nodes.Base;
using System;

namespace Assets.Parsing.Nodes
{
    [NodeType("IF")]
    public class IfNode : EvaluatedNode
    {
        public string FlagName;
        public string ConditionValue;

        public override bool IsConditionTrue()
        {
            return Flags.Get(FlagName) == ConditionValue;
        }

        protected override void ParseNodeText(string rawText)
        {
            var splitText = rawText.Split();
            if (splitText.Length != 2)
                throw new ArgumentException("Invalid IF statement found in script.");

            this.FlagName = splitText[0];
            this.ConditionValue = splitText[1];
        }
    }
}
