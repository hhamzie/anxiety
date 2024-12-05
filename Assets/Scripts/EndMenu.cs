using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public TMP_Text winnerText;

    void Start()
    {
        string winner = PlayerPrefs.GetString("Winner");
        if (winnerText != null)
        {
            winnerText.text = $"Winner: {winner}";
        }
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
