using UnityEngine;
using UnityEngine.UI;

public class GameOverValueText : MonoBehaviour
{
    Text coinValueText;

    void Awake()
    {
        coinValueText = GetComponent<Text>();
    }

    void Start()
    {
        SetCoinValueText();
    }

    void SetCoinValueText()
    {
        if (coinValueText != null & GameManager.Instance != null)
            coinValueText.text = "End Value\n $" + GameManager.Instance.CoinValue.ToString();
    }
}
