using Assets.Models;
using Assets.Parsing.Attributes;
using Assets.Parsing.Nodes.Base;
using System;

namespace Assets.Parsing.Nodes
{
    [NodeType("DIALOGUE")]
    public class DialogueNode : LinearNode
    {
        public Character Character { get; set; }

        public DialogueNode(string characterSprite = null)
        {
            Character = new Character
            {
                Sprite = characterSprite,

                // Only the player (labeled 'you' or 'player') is on the left - all others on the right.
                IsOnLeft = characterSprite.StartsWith("you", StringComparison.CurrentCultureIgnoreCase) ||
                    characterSprite.StartsWith("player", StringComparison.CurrentCultureIgnoreCase),
            };
        }

        public override Character GetCharacter()
        {
            return Character;
        }
    }
}
