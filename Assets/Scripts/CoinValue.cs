using UnityEngine;
using UnityEngine.UI;

public class CoinValue : MonoBehaviour
{
    Text coinValueText;

    void Awake()
    {
        coinValueText = GetComponent<Text>();
    }

    void OnEnable()
    {
        GameManager.OnCoinValueImpacted += DisplayCoinValue;
    }

    void OnDisable()
    {
        GameManager.OnCoinValueImpacted -= DisplayCoinValue;
    }

    void DisplayCoinValue()
    {
        if (coinValueText != null && GameManager.Instance != null)
            coinValueText.text = "$" + GameManager.Instance.CoinValue.ToString();
    }
}
