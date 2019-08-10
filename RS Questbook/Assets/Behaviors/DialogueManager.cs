using Assets.Parsing;
using Assets.Parsing.Nodes.Base;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text GameText;
    public Image CharacterLeft;
    public Image CharacterRight;
    public Image TextBox;
    public string CurrentQuest;

    private Node _currentNode;

    private static List<KeyCode> _customKeys = new List<KeyCode>
    {
        KeyCode.DownArrow,
        KeyCode.UpArrow,
    };

    // Start is called before the first frame update
    void Start()
    {
        _currentNode = new DialogueFileReader(CurrentQuest).StartNode;
        CharacterLeft.canvasRenderer.SetAlpha(0);
        CharacterRight.canvasRenderer.SetAlpha(0);
    }

    void OnEnable()
    {
        Paint();
    }

    void OnDisable()
    {
        // Hide the talksprites
        CharacterLeft.canvasRenderer.SetAlpha(0);
        CharacterRight.canvasRenderer.SetAlpha(0);

        // Hide the textbox
        GameText.text = "";
        TextBox.canvasRenderer.SetAlpha(0);
    }

    // Update is called once per frame
    void Update()
    {
        // Do nothing if dialogue is disabled.
        if (!enabled) return;

        // Check for end of script first.
        if (_currentNode == null)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        // Z to advance text.
        if (Input.GetKeyDown(KeyCode.Z))
        {
            MoveToNextScreen();
        }

        // Node-specific handling.
        foreach (var key in _customKeys)
        {
            var needsRepaint = false;
            if (Input.GetKeyDown(key))
                needsRepaint = needsRepaint || _currentNode.HandleKey(key);

            if (needsRepaint)
                Paint();
        }
    }

    void MoveToNextScreen()
    {
        // Get the next child node from the current node.
        _currentNode = _currentNode.GetNext();

        // Refresh with new node's content.
        Paint();
    }

    void Paint()
    {
        if (_currentNode == null) return;

        // If current node is not a display node, 
        // then keep going until a displayable node is found.
        if (!_currentNode.IsDisplayed)
            MoveToNextScreen();

        // Update dialogue text.
        GameText.text = _currentNode.GetDisplayText();

        // Hide any existing sprites before loading new ones.
        CharacterLeft.canvasRenderer.SetAlpha(0);
        CharacterRight.canvasRenderer.SetAlpha(0);

        // Load new sprites, if present.
        var character = _currentNode.GetCharacter();
        if (character != null)
        {
            var sprite = Resources.Load<Sprite>($@"Talksprites/{character.Sprite}");

            if (sprite != null)
            {
                var toMakeVisible = character.IsOnLeft ? CharacterLeft : CharacterRight;
                toMakeVisible.sprite = sprite;
                toMakeVisible.GetComponent<AspectRatioFitter>().aspectRatio = sprite.rect.width / sprite.rect.height;
                toMakeVisible.canvasRenderer.SetAlpha(1);
            }
        }
    }
}
