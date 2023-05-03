using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    [SerializeField]
    private Text _clockText;

    void Update()
    {
        DisplayTime(); // TODO: make this event driven - to tick every second opposed to every frame
    }

    void DisplayTime()
    {
        float timeToDisplay = GameManager.Instance.TimeRemaining;

        if (timeToDisplay < 0) timeToDisplay = 0; // lock at 0

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        _clockText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
