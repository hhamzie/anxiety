using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private Collider2D platformCollider;
    private bool isPlayerAbove = false;

    void Start()
    {
        platformCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (isPlayerAbove && platformCollider.enabled)
        {
            platformCollider.enabled = false;
        }
        else if (!isPlayerAbove)
        {
            platformCollider.enabled = true; 
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null && rb.velocity.y > 0) // Moving up
            {
                isPlayerAbove = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerAbove = false;
            platformCollider.enabled = true;
        }
    }
}
