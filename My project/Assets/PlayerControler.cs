using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _jumpSpeed;

    [SerializeField] Rigidbody2D _rigidbody;

    Vector2 _direction;

    public bool isGrounded;

    public void SetDirection(Vector2 vector)
    {
        _direction = vector;
    }
    void Move()
    {
        _rigidbody.velocity = new(_direction.x * _speed * _speed/2, _rigidbody.velocity.y);
    }
    void Jump()
    {
        bool isJumping = _direction.y > 0;
        if (isJumping)
        {
            if (isGrounded)
            {
                _rigidbody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
            }  
        }
        else if (_rigidbody.velocity.y > 0)
        {
            _rigidbody.velocity = new(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f);
        }
    }
    private void FixedUpdate()
    {
        Move();
        Jump();
    }
}
