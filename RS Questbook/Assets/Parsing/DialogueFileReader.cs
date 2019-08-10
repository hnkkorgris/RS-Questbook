using Assets.Parsing.Nodes.Base;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Parsing
{
    public class DialogueFileReader
    {
        public Node StartNode;

        private TextAsset _fileContent;

        public DialogueFileReader(string questName)
        {
            _fileContent = Resources.Load<TextAsset>($@"Quests/{questName}");
            StartNode = BuildDialogueTree();
        }

        private Node BuildDialogueTree()
        {
            var lines = new Queue<string>();

            using (var fileReader = new StringReader(_fileContent.text))
            {
                var currentLine = fileReader.ReadLine();

                // Loop through all lines in file and add to in-memory list.
                while(currentLine != null)
                {
                    // Skip blank spaces.
                    if (string.IsNullOrWhiteSpace(currentLine))
                    {
                        currentLine = fileReader.ReadLine();
                        continue;
                    }

                    lines.Enqueue(currentLine);
                    currentLine = fileReader.ReadLine();
                }
            }

            // Begin recursive build of tree.
            var startNode = Node.Create(lines.Dequeue());
            startNode.BuildTree(lines);
            return startNode;
        }
    }
}
