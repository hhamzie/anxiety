using UnityEngine;

public class ClockZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"{other.gameObject.name} has won the game!");
            Time.timeScale = 0f;
        }
    }
}
