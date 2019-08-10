﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplorationManager : MonoBehaviour
{
    public Image CharacterSprite;

    private float velocity;

    // Start is called before the first frame update
    void Start()
    {
        velocity = 0.04f;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        var newPosition = CharacterSprite.transform.position;

        if(Input.GetKey(KeyCode.UpArrow))
        {
            newPosition.y += velocity;
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            newPosition.y -= velocity;
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition.x -= velocity;
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            newPosition.x += velocity;
        }

        CharacterSprite.transform.SetPositionAndRotation(newPosition, Quaternion.identity);
    }
}
