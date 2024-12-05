using UnityEngine;
using TMPro; // For TextMeshPro support

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winnerText; // Reference to the TextMeshPro UI component

    void Start()
    {
        // Retrieve the winner's name from PlayerPrefs
        string winner = PlayerPrefs.GetString("Winner", "No Winner");
        winnerText.text = winner; // Display the winner
    }
}
