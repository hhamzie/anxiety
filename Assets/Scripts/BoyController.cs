using UnityEngine;
using UnityEngine.SceneManagement;

public class BoyController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpSpeed = 50f;
    private Rigidbody2D player;
    public Transform GroundCheck;
    public float GroundCheckRadius = 0.2f;
    public LayerMask GroundLayer;
    private bool isTouchingGround;
    private Collider2D currentCollider;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        player.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, GroundLayer);

        float directionX = 0f;

        // Handle left movement with collision check
        if (Input.GetKey(KeyCode.A) && !IsBlocked(Vector2.left))
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            directionX = -1f;
        }
        // Handle right movement with collision check
        else if (Input.GetKey(KeyCode.D) && !IsBlocked(Vector2.right))
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            directionX = 1f;
        }

        // Handle jumping
        if (Input.GetKeyDown(KeyCode.W) && isTouchingGround)
        {
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
        }

        // Apply movement
        player.velocity = new Vector2(directionX * speed, player.velocity.y);
    }

    private bool IsBlocked(Vector2 direction)
    {
        // Use Rigidbody2D's own collision detection by checking whether there is a block in the given direction
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.5f, GroundLayer);
        return hit.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ghost"))
        {
            Debug.Log("Boy touched the ghost. Girl wins!");
            PlayerPrefs.SetString("Winner", "Girl Wins!");
            SceneManager.LoadScene("GameOver");
        }
    }
}
