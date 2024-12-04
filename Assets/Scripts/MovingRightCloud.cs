using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRightCloud : MonoBehaviour
{
    public float speed = 5f; // Speed at which the sprite moves
    private float spriteWidth; // Width of the sprite
    private Camera mainCamera;

    void Start()
    {
        // Get the main camera
        mainCamera = Camera.main;

        // Calculate the width of the sprite in world units
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // Move the sprite to the right
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Get the position of the sprite and the right edge of the camera
        float spriteLeftEdge = transform.position.x - spriteWidth / 2;
        float cameraRightEdge = mainCamera.ViewportToWorldPoint(Vector3.right).x;

        // Check if the sprite is off the right edge of the screen
        if (spriteLeftEdge > cameraRightEdge)
        {
            // Calculate the left edge of the camera
            float cameraLeftEdge = mainCamera.ViewportToWorldPoint(Vector3.zero).x;

            // Move the sprite to the left side of the screen
            transform.position = new Vector2(cameraLeftEdge - spriteWidth / 2, transform.position.y);
        }
    }
}
