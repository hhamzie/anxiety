using UnityEngine;
using System.Collections;

public class PlayerRespawner : MonoBehaviour
{
    private Camera mainCamera;
    private Renderer characterRenderer;
    private bool isRespawning = false;

    // Optional: Delay before respawning
    public float respawnDelay = 1.0f;

    private void Start()
    {
        mainCamera = Camera.main;
        characterRenderer = GetComponent<Renderer>();

        if (characterRenderer == null)
        {
            Debug.LogError("PlayerRespawner: No Renderer found on the character.");
        }

        if (Respawner.Instance == null)
        {
            Debug.LogError("PlayerRespawner: Respawner instance not found in the scene.");
        }
    }

    private void Update()
    {
        if (isRespawning) return;

        if (IsCharacterVisible())
        {
            // Character is visible; no action needed
            return;
        }
        else
        {
            // Start respawn process
            StartCoroutine(RespawnCoroutine());
        }
    }

    private bool IsCharacterVisible()
    {
        if (characterRenderer == null) return false;

        // Check if any part of the renderer is visible by the main camera
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        return GeometryUtility.TestPlanesAABB(planes, characterRenderer.bounds);
    }

    private IEnumerator RespawnCoroutine()
    {
        isRespawning = true;

        Debug.Log("PlayerRespawner: Character is not visible. Initiating respawn.");

        // Optional: Play death animation or effects here

        // Wait for the specified delay
        yield return new WaitForSeconds(respawnDelay);

        // Get the respawner with the smallest Y within camera view
        Vector3 respawnPosition = Respawner.Instance.GetLowestYRespawnPointInView(mainCamera);

        // Move the character to the respawn position
        transform.position = respawnPosition;

        Debug.Log($"PlayerRespawner: Character respawned at {respawnPosition}");

        // Optional: Reset character state (e.g., health, status effects)

        isRespawning = false;
    }
}
