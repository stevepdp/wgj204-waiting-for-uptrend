using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour
{
    Text gameOverText;

    void Awake()
    {
        gameOverText = GetComponent<Text>();
    }

    void Start()
    {
        SetGameOverText();
    }

    void SetGameOverText()
    {
        if (gameOverText != null && GameManager.Instance != null)
        {
            GameEndType end = GameManager.Instance.GameEnding;

            if (end == GameEndType.ZERO)
                gameOverText.text = "$GMJM went to zero!";
            else if (end == GameEndType.LOSS)
                gameOverText.text = "You made a loss!";
            else if (end == GameEndType.PROFIT)
                gameOverText.text = "You made a profit!";
            else if (end == GameEndType.SIGNIFICANT_PROFIT)
                gameOverText.text = "You made a significant profit!";
            else if (end == GameEndType.TO_THE_MOON)
                gameOverText.text = "To the moon!";
        }
    }
}
