using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Stop all other behaviors
        GetComponent<DialogueManager>().enabled = false;
        GetComponent<ExplorationManager>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
