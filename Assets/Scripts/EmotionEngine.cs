using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionEngine : MonoBehaviour
{
    private GameManager _gameManager;

    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;

    void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {

    }

    void Update()
    {
        float coinVal = _gameManager.GetCoinValue();

        if (coinVal < 100)
        {
            ChangeSprite(0); // crying
        }
        else if (coinVal <= 500)
        {
            ChangeSprite(1); // pensive
        }
        else if (coinVal > 500 && coinVal <= 1000)
        {
            ChangeSprite(2); // cool
        }
        else if (coinVal > 1000 && coinVal < 9000)
        {
            ChangeSprite(3); // alert
        }
        else if (coinVal >= 9000)
        {
            ChangeSprite(4); // mind blown
        }
        else
        {
            ChangeSprite(2); // default
        }
    }

    void ChangeSprite(int spriteNumber)
    {
        spriteRenderer.sprite = spriteArray[spriteNumber];
    }
}
