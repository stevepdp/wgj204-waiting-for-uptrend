using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    Text clockText;

    void Awake()
    {
        clockText = GetComponent<Text>();
    }

    void OnEnable()
    {
        GameManager.OnClockTick += DisplayTime;
    }

    void OnDisable()
    {
        GameManager.OnClockTick -= DisplayTime;
    }

    void DisplayTime()
    {
        if (clockText != null && GameManager.Instance != null)
        {
            float timeToDisplay = GameManager.Instance.TimeRemaining;

            if (timeToDisplay < 0) timeToDisplay = 0; // lock at 0

            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            clockText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
