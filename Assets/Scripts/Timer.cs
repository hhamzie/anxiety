using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro; // Import TextMeshPro namespace

public class Timer : MonoBehaviour, IPointerClickHandler // Implement IPointerClickHandler for event handling
{
    [SerializeField] private Image uiFill;
    [SerializeField] private TextMeshProUGUI uiText; // Use TextMeshProUGUI instead of Text
    public int Duration;

    private bool Pause;
    private int remainingDuration;

    public void OnPointerClick(PointerEventData eventData)
    {
        Pause = !Pause;
    }

    void Start()
    {
        // Ensure the sorting order is set to 1
        //SetSortingOrder(1);

        Begin(Duration);
    }

    private void Begin(int second)
    {
        remainingDuration = second;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (remainingDuration >= 0)
        {
            if (!Pause)
            {
                // Update the TextMeshProUGUI text
                uiText.text = $"{remainingDuration / 60:00} : {remainingDuration % 60:00}";
                // Update the fill amount of the UI image
                uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
                remainingDuration--;
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }
        OnEnd();
    }

    private void OnEnd()
    {
        Debug.Log("End");
    }

    private void SetSortingOrder(int order)
    {
        // Get or add a Canvas component to the GameObject
        Canvas canvas = GetComponent<Canvas>();
        if (canvas == null)
        {
            canvas = gameObject.AddComponent<Canvas>();
        }

        // Set the sorting order
        canvas.overrideSorting = true;
        canvas.sortingOrder = order;

        // Ensure a CanvasRenderer exists for proper rendering
        if (GetComponent<CanvasRenderer>() == null)
        {
            gameObject.AddComponent<CanvasRenderer>();
        }
    }
}