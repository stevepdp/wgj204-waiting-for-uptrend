using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    private GameManager _gameManager;

    [SerializeField]
    private Text _clockText;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        DisplayTime();
    }

    void DisplayTime()
    {
        float timeToDisplay = _gameManager.GetTimeElapsed();

        if (timeToDisplay < 0) timeToDisplay = 0; // lock at 0

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        _clockText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
