using UnityEngine;
using System.Collections.Generic;

public class Respawner : MonoBehaviour
{
    public static Respawner Instance;

    // List to hold all respawn points
    private List<Transform> respawnPoints = new List<Transform>();

    private void Awake()
    {
        // Implement singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple Respawner instances detected. Destroying the new one.");
            Destroy(this);
            return;
        }

        // Find all respawner points in the scene by looking for RespawnPoint components
        RespawnPoint[] points = FindObjectsOfType<RespawnPoint>();
        foreach (var point in points)
        {
            respawnPoints.Add(point.transform);
            Debug.Log($"Respawner added: {point.name} at position {point.transform.position}");
        }

        if (respawnPoints.Count == 0)
        {
            Debug.LogError("Respawner: No respawn points found. Please add RespawnPoint components to respawner GameObjects.");
        }
    }

    /// <summary>
    /// Retrieves the closest respawn point to the given position.
    /// </summary>
    public Vector3 GetClosestRespawnPoint(Vector3 position)
    {
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (Transform respawn in respawnPoints)
        {
            float distance = Vector3.Distance(position, respawn.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = respawn;
            }
        }

        if (closest != null)
        {
            Debug.Log($"Closest respawn point to {position} is {closest.name} at {closest.position}");
            return closest.position;
        }
        else
        {
            Debug.LogWarning("Respawner: No closest respawn point found. Using Vector3.zero as fallback.");
            return Vector3.zero;
        }
    }

    /// <summary>
    /// Retrieves the respawn point with the smallest Y-coordinate within the camera's view.
    /// </summary>
    public Vector3 GetLowestYRespawnPointInView(Camera camera)
    {
        Transform selectedRespawner = null;
        float smallestY = Mathf.Infinity;

        foreach (Transform respawn in respawnPoints)
        {
            Vector3 viewportPoint = camera.WorldToViewportPoint(respawn.position);
            // Check if the respawn point is within the camera's viewport
            if (viewportPoint.z > 0 && viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1)
            {
                if (respawn.position.y < smallestY)
                {
                    smallestY = respawn.position.y;
                    selectedRespawner = respawn;
                }
            }
        }

        if (selectedRespawner != null)
        {
            Debug.Log($"Selected respawn point: {selectedRespawner.name} at {selectedRespawner.position}");
            return selectedRespawner.position;
        }
        else
        {
            Debug.LogWarning("No respawner within camera view. Falling back to the closest respawn point.");
            return GetClosestRespawnPoint(camera.transform.position);
        }
    }
}
