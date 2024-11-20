using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpSpeed = 8f;
    private Rigidbody2D player;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public bool isTouchingGround;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();

        // Lock rotation to prevent rolling
        player.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        float directionX = 0f;

        // Movement controls for the boy (WASD)
        if (Input.GetKey(KeyCode.A))
        {
            directionX = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            directionX = 1f;
        }

        player.velocity = new Vector2(directionX * speed, player.velocity.y);

        // Jumping with W key
        if (Input.GetKey(KeyCode.W) && isTouchingGround)
        {
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
        }
    }
}
