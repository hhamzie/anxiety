using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro namespace
using UnityEngine.SceneManagement;


public class Timer : MonoBehaviour
{
    [SerializeField] private Image uiFill;
    [SerializeField] private TextMeshProUGUI uiText;
    public int Duration;

    public GameObject boy; // Reference to Boy sprite
    public GameObject girl; // Reference to Girl sprite
    public string gameOverSceneName = "GameOver"; // Name of the GameOver scene

    private int remainingDuration;

    void Start()
    {
        InitializeUI();
        Begin(Duration);
    }

    private void InitializeUI()
    {
        remainingDuration = Duration;
        uiText.text = $"{remainingDuration / 60:00} : {remainingDuration % 60:00}";
        uiFill.fillAmount = 1; // Start with a full circle
    }

    private void Begin(int seconds)
    {
        remainingDuration = seconds;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (remainingDuration > 0)
        {
            // Update the UI each second
            uiText.text = $"{remainingDuration / 60:00} : {remainingDuration % 60:00}";
            uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);

            remainingDuration--;
            yield return new WaitForSeconds(1f); // Wait for 1 second
        }

        // Timer reaches 0, switch to GameOver scene
        OnEnd();
    }

    private void OnEnd()
{
    Debug.Log("Timer ended. Switching to GameOver scene...");

    // Get the Y positions of both players
    float boyYPosition = boy.transform.position.y;
    float girlYPosition = girl.transform.position.y;

    // Determine the winner and store it in PlayerPrefs
    if (boyYPosition > girlYPosition)
    {
        Debug.Log("Boy wins!");
        PlayerPrefs.SetString("Winner", "Boy Wins!");
    }
    else if (girlYPosition > boyYPosition)
    {
        Debug.Log("Girl wins!");
        PlayerPrefs.SetString("Winner", "Girl Wins!");
    }
    else
    {
        Debug.Log("It's a tie!");
        PlayerPrefs.SetString("Winner", "It's a Tie!");
    }

    // Switch to the GameOver scene
    SceneManager.LoadScene(gameOverSceneName);
}

}
