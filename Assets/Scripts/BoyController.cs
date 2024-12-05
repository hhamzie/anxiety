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
