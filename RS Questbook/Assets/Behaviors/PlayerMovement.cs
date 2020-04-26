using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rBody;
    private BoxCollider2D _collider;
    private Animator _animator;

    private float _maxSpeed;
    private Vector2 _counterVelocity;

    // Start is called before the first frame update
    void Start()
    {
        _rBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();

        _maxSpeed = 8f;
        _counterVelocity = new Vector2(0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate requested movement
        var direction = GetDirectionFromInputs();
        var velocity = GetVelocity(direction);

        // Update player position
        _rBody.velocity = velocity;

        // Update sprite animation
        _animator.SetInteger("xMoveDirection", direction.x);
        _animator.SetInteger("yMoveDirection", direction.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _counterVelocity = collision.relativeVelocity;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _counterVelocity = new Vector2(0f, 0f);
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

        velocity.x += direction.x * _maxSpeed + _counterVelocity.x;
        velocity.y += direction.y * _maxSpeed + _counterVelocity.y;

        return velocity;
    }
}
