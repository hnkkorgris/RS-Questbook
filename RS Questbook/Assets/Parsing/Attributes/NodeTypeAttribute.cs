using System;

namespace Assets.Parsing.Attributes
{
    public class NodeTypeAttribute : Attribute
    {
        public string Name { get; set; }

        public NodeTypeAttribute(string name)
        {
            Name = name;
        }
    }
}
