using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rBody;
    private Animator _animator;
    private float _maxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _rBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _maxSpeed = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate requested movement
        var direction = GetDirectionFromInputs();
        var velocity = GetVelocity(direction);
        Debug.Log($"direction.x: {direction.x}");

        // Update player position
        _rBody.velocity = velocity;

        // Update sprite animation
        _animator.SetInteger("xMoveDirection", direction.x);
        _animator.SetInteger("yMoveDirection", direction.y);
    }

    private Vector2Int GetDirectionFromInputs()
    {
        var direction = new Vector2Int(0, 0);

        if (Input.GetKey(KeyCode.UpArrow))
        {
            direction.y++;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            direction.y--;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction.x--;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            direction.x++;
        }

        return direction;
    }

    private Vector2 GetVelocity(Vector2Int direction)
    {
        var velocity = new Vector2(0f, 0f);

        velocity.x += direction.x * _maxSpeed;
        velocity.y += direction.y * _maxSpeed;

        return velocity;
    }
}
