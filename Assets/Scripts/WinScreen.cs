
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WinScreenController : MonoBehaviour
{
    public TMP_Text winnerText;
    public string gameplaySceneName = "GameScene";

    void Start()
    {
        string winner = PlayerPrefs.GetString("Winner", "Unknown");
        if (winnerText != null)
        {
            winnerText.text = $"Winner: {winner}";
        }
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(gameplaySceneName);
    }
}
