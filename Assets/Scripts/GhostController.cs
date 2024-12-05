using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostController : MonoBehaviour
{
    public Transform boy; // Reference to the boy sprite
    public Transform girl; // Reference to the girl sprite
    public float speed = 0.6f; // Speed at which the ghost moves
    private Transform target; // Current target (boy or girl)
    private Camera mainCamera; // Reference to the main camera
    private bool isActive = false; // Tracks if the ghost has entered the camera's view
    private bool gameEnded = false; // To prevent multiple game endings

    void Start()
    {
        mainCamera = Camera.main; // Assign the main camera
        target = null; // Ghost has no target until it's active
    }

    void Update()
    {
        if (!gameEnded)
        {
            if (!isActive && IsInCameraView())
            {
                // Ghost becomes active and targets either boy or girl
                isActive = true;
                target = Random.Range(0, 2) == 0 ? boy : girl;
            }

            if (isActive && target != null)
            {
                // Move toward the target
                Vector3 direction = (target.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
            }
        }
    }

    private bool IsInCameraView()
    {
        // Convert the ghost's position to viewport coordinates
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);

        // Check if the ghost is within the camera's view
        return viewportPos.x > 0 && viewportPos.x < 1 && viewportPos.y > 0 && viewportPos.y < 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameEnded)
        {
            if (collision.transform == boy)
            {
                Debug.Log("Boy touched by ghost. Girl wins!");
                PlayerPrefs.SetString("Winner", "Girl Wins!");
                EndGame();
            }
            else if (collision.transform == girl)
            {
                Debug.Log("Girl touched by ghost. Boy wins!");
                PlayerPrefs.SetString("Winner", "Boy Wins!");
                EndGame();
            }
        }
    }

    private void EndGame()
    {
        gameEnded = true; // Prevent multiple triggers
        SceneManager.LoadScene("GameOver"); // Load the GameOver scene
    }
}
