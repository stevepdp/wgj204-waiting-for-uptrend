using UnityEngine;
using UnityEngine.UI;

public class CoinValue : MonoBehaviour
{
    [SerializeField]
    private Text _coinValueText;

    void Update()
    {
        DisplayCoinValue();
    }

    void DisplayCoinValue()
    {
        float coinValue = GameManager.Instance.GetCoinValue();
        _coinValueText.text = "$" + coinValue.ToString();
    }
}
