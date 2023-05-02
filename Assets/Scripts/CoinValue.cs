using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinValue : MonoBehaviour
{
    private GameManager _gameManager;

    [SerializeField]
    private Text _coinValueText;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        DisplayCoinValue();
    }
    void DisplayCoinValue()
    {
        float coinValue = _gameManager.GetCoinValue();
        _coinValueText.text = "$" + coinValue.ToString();
    }
}
