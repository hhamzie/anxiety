using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostController : MonoBehaviour
{
    public Transform boy; // Reference to the boy sprite
    public Transform girl; // Reference to the girl sprite
    public float speed = 0.8f; // Speed at which the ghost moves
    public float respawnBuffer = 0.5f; // Buffer distance outside the camera borders for respawn

    private Transform target; // Current target (boy or girl)
    private Camera mainCamera; // Reference to the main camera
    private bool gameEnded = false; // To prevent multiple game endings

    void Start()
    {
        // Assign the main camera
        mainCamera = Camera.main;

        // Set the initial target to the boy or girl randomly
        target = Random.Range(0, 2) == 0 ? boy : girl;
    }

    void Update()
    {
        // Follow the current target
        if (target != null && !gameEnded)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }

        // Check if the ghost has left the camera's view
        if (!IsInCameraView() && !gameEnded)
        {
            RespawnAtCameraBorder();
        }
    }

    private bool IsInCameraView()
    {
        // Convert the ghost's position to viewport coordinates
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);

        // Check if the ghost is outside the camera's view
        return viewportPos.x > 0 && viewportPos.x < 1 && viewportPos.y > 0 && viewportPos.y < 1;
    }

    private void RespawnAtCameraBorder()
    {
        // Get the camera's bounds in world space
        Vector3 cameraBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z));
        Vector3 cameraTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));

        // Randomly choose a border (0 = left, 1 = right, 2 = top, 3 = bottom)
        int border = Random.Range(0, 4);
        Vector3 respawnPosition = transform.position;

        switch (border)
        {
            case 0: // Left border
                respawnPosition = new Vector3(cameraBottomLeft.x - respawnBuffer, Random.Range(cameraBottomLeft.y, cameraTopRight.y), transform.position.z);
                break;
            case 1: // Right border
                respawnPosition = new Vector3(cameraTopRight.x + respawnBuffer, Random.Range(cameraBottomLeft.y, cameraTopRight.y), transform.position.z);
                break;
            case 2: // Top border
                respawnPosition = new Vector3(Random.Range(cameraBottomLeft.x, cameraTopRight.x), cameraTopRight.y + respawnBuffer, transform.position.z);
                break;
            case 3: // Bottom border
                respawnPosition = new Vector3(Random.Range(cameraBottomLeft.x, cameraTopRight.x), cameraBottomLeft.y - respawnBuffer, transform.position.z);
                break;
        }

        // Set the ghost's new position
        transform.position = respawnPosition;

        // Randomly switch targets
        target = Random.Range(0, 2) == 0 ? boy : girl;

        Debug.Log($"Ghost respawned at border: {respawnPosition}");
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
