using UnityEngine;

public class EmotionEngine : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;

    void Update()
    {
        float coinVal = GameManager.Instance.GetCoinValue();

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
