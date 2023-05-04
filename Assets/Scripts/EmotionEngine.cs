using UnityEngine;

enum Emotion
{
    CRYING,
    PENSIVE,
    COOL,
    ALERT,
    MINDBLOWN
}

public class EmotionEngine : MonoBehaviour
{
    const int CRYING_THRESHOLD = 100;
    const int PENSIVE_THRESHOLD = 500;
    const int COOL_THRESHOLD = 1000;
    const int ALERT_THRESHOLD = 9000;

    [SerializeField] Sprite[] spriteArray;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        GameManager.OnCoinValueImpacted += CalcEmotion;
    }

    void OnDisable()
    {
        GameManager.OnCoinValueImpacted -= CalcEmotion;
    }

    void CalcEmotion()
    {
        float coinVal = GameManager.Instance.CoinValue;

        if (coinVal < CRYING_THRESHOLD)
            SetEmotionSprite(Emotion.CRYING);
        else if (coinVal <= PENSIVE_THRESHOLD)
            SetEmotionSprite(Emotion.PENSIVE);
        else if (coinVal > PENSIVE_THRESHOLD && coinVal <= COOL_THRESHOLD)
            SetEmotionSprite(Emotion.COOL);
        else if (coinVal > COOL_THRESHOLD && coinVal < ALERT_THRESHOLD)
            SetEmotionSprite(Emotion.ALERT);
        else if (coinVal >= ALERT_THRESHOLD)
            SetEmotionSprite(Emotion.MINDBLOWN);
        else
            SetEmotionSprite(Emotion.COOL);
    }

    void SetEmotionSprite(Emotion emotionIndex)
    {
        int spriteChoice = (int) emotionIndex;

        if (spriteRenderer != null)
        {
            if (spriteRenderer.sprite != spriteArray[spriteChoice])
                spriteRenderer.sprite = spriteArray[spriteChoice];
        }
    }
}
