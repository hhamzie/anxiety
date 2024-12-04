using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCloud : MonoBehaviour
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
        // Move the sprite to the left
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Get the position of the sprite and the left edge of the camera
        float spriteRightEdge = transform.position.x + spriteWidth / 2;
        float cameraLeftEdge = mainCamera.ViewportToWorldPoint(Vector3.zero).x;

        // Check if the sprite is off the left edge of the screen
        if (spriteRightEdge < cameraLeftEdge)
        {
            // Calculate the right edge of the camera
            float cameraRightEdge = mainCamera.ViewportToWorldPoint(Vector3.right).x;

            // Move the sprite to the right side of the screen
            transform.position = new Vector2(cameraRightEdge + spriteWidth / 2, transform.position.y);
        }
    }
}
