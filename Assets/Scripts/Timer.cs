using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Timer : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image uiFill;
    [SerializeField] private TextMeshProUGUI uiText;
    public int Duration;

    private bool Pause;
    private int remainingDuration;

    public void OnPointerClick(PointerEventData eventData)
    {
        Pause = !Pause;
    }

    void Start()
    {
        InitializeUI();
        Begin(Duration);
    }

    // Initialize the UI elements before starting the timer
    private void InitializeUI()
    {
        remainingDuration = Duration;
        uiText.text = $"{remainingDuration / 60:00} : {remainingDuration % 60:00}";
        uiFill.fillAmount = 1; // Start with a full circle
    }

    private void Begin(int second)
    {
        remainingDuration = second;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (remainingDuration > 0)
        {
            if (!Pause)
            {
                // Update the UI each frame
                uiText.text = $"{remainingDuration / 60:00} : {remainingDuration % 60:00}";
                uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);

                remainingDuration--;
                yield return new WaitForSeconds(1f); // Wait for 1 second
            }
            else
            {
                // Ensure UI stays updated even when paused
                uiText.text = $"{remainingDuration / 60:00} : {remainingDuration % 60:00}";
                uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
                yield return null;
            }
        }
        OnEnd();
    }

    private void OnEnd()
    {
        Debug.Log("End");
        uiText.text = "00:00"; // Show "00:00" when the timer ends
        uiFill.fillAmount = 0; // Ensure the fill image is empty
    }
}