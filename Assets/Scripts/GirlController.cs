using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpSpeed = 50f;
    private Rigidbody2D player;
    public Transform GroundCheck;
    public float GroundCheckRadius = 0.2f;
    public LayerMask GroundLayer;
    private bool isTouchingGround;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        player.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, GroundLayer);
        Debug.Log("isTouchingGround: " + isTouchingGround);

        // Checking for collision direction to avoid movement into sides, corners, and bottom
        bool canMoveLeft = !Physics2D.Raycast(new Vector2(transform.position.x - 0.5f, transform.position.y), Vector2.left, 0.1f, GroundLayer);
        bool canMoveRight = !Physics2D.Raycast(new Vector2(transform.position.x + 0.5f, transform.position.y), Vector2.right, 0.1f, GroundLayer);
        bool canMoveUp = !Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.5f), Vector2.up, 0.1f, GroundLayer);
        bool canMoveDown = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.5f), Vector2.down, 0.1f, GroundLayer);

        float directionX = 0f;

        if (Input.GetKey(KeyCode.LeftArrow) && canMoveLeft)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            directionX = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && canMoveRight)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            directionX = 1f;
        }

        // Apply horizontal movement
        player.velocity = new Vector2(directionX * speed, player.velocity.y);

        // Allow jumping only if player is on the ground
        if (Input.GetKeyDown(KeyCode.UpArrow) && isTouchingGround)
        {
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
        }
    }

    private void OnDrawGizmos()
    {
        if (GroundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(GroundCheck.position, GroundCheckRadius);
        }
    }
}
