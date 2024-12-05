using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyController : MonoBehaviour
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

        float directionX = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            directionX = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            directionX = 1f;
        }

        player.velocity = new Vector2(directionX * speed, player.velocity.y);

        if (Input.GetKeyDown(KeyCode.W) && isTouchingGround)
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
