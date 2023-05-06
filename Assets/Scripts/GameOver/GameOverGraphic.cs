using System;
using UnityEngine;
using UnityEngine.UI;

public class GameOverGraphic : MonoBehaviour
{
    [SerializeField] Sprite[] gameOverSprites;
    Image backgroundImage;

    void Awake()
    {
        backgroundImage = GetComponent<Image>();
    }

    void Start()
    {
        SetGameOverGraphic();
    }

    void SetGameOverGraphic()
    {
        int endingsCount = Enum.GetNames(typeof(GameEndType)).Length;

        if (backgroundImage != null && gameOverSprites.Length == endingsCount && GameManager.Instance != null)
        {
            int endingIndex = (int) GameManager.Instance.GameEnding;
            backgroundImage.sprite = gameOverSprites[endingIndex];
        }
    }
}
