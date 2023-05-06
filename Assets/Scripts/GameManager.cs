using Random = UnityEngine.Random;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<GameManager>();
            if (instance == null)
                instance = Instantiate(new GameObject("GameManager")).AddComponent<GameManager>();
            return instance;
        }
    }

    public static event Action OnCoinValueImpacted;
    public static event Action OnClockTick;

    const byte HODL_PERKS_THRESHOLD = 60;
    const byte MOON_PERKS_THRESHOLD = 3;
    const byte MARKET_IMPACT_LOW = 4;
    const byte MARKET_IMPACT_MED = 10;
    const byte MARKET_IMPACT_HIGH = 20;
    const byte GAME_TIME_SECONDS = 120;
    const float PRICE_DROP_START_DELAY = 1f;
    const float PRICE_DROP_REPEAT_RATE = 1f;
    const float TIMEOVER_START_DELAY = 0f;
    const float TIMEOVER_REPEAT_RATE = 1f;
    const int GMJM_START_VALUE = 1000;
    const int MOON_RANGE_MIN = 250000;
    const int MOON_RANGE_MAX = 5000000;

    bool gameIsActive;
    bool isHolding;
    byte timeRemaining;
    byte[] marketImpactValues = new byte[] { MARKET_IMPACT_LOW, MARKET_IMPACT_MED, MARKET_IMPACT_HIGH };
    GameEndType gameEnding;
    int coinValue;
    string popupText;

    public GameEndType GameEnding {
        get { return gameEnding; }
    }

    public int CoinValue
    {
        get { return coinValue; }
        set { coinValue = value; }
    }

    public bool IsHolding
    {
        get { return isHolding; }
        set { isHolding = value; }
    }

    public string PopupText
    {
        get { return popupText; }
    }

    public byte TimeRemaining
    {
        get { return timeRemaining; }
    }

    void Awake()
    {
        EnforceSingleInstance();
    }

    void Start()
    {
        NewGame();
    }

    void EnforceSingleInstance()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void GameOver()
    {
        CancelInvoke("ReduceCoinValue");
        CancelInvoke("ReduceTimeRemaining");
        gameIsActive = false;

        if (coinValue < GMJM_START_VALUE)
        {
            if (coinValue == 0)
                gameEnding = GameEndType.ZERO;
            else if (coinValue > 0 && coinValue < GMJM_START_VALUE)
                gameEnding = GameEndType.LOSS;
        }
        else if (coinValue > GMJM_START_VALUE && coinValue < GMJM_START_VALUE * 2)
        {
            gameEnding = GameEndType.PROFIT;
        }
        else if (coinValue >= GMJM_START_VALUE * 2 && coinValue < MOON_RANGE_MIN)
        { 
            gameEnding = GameEndType.SIGNIFICANT_PROFIT;
        }
        else if (coinValue >= MOON_RANGE_MIN)
        {
            gameEnding = GameEndType.TO_THE_MOON;
        }

        SceneManager.LoadScene("99_GameOver");
    }

    void NewGame()
    {
        gameIsActive = true;
        coinValue = GMJM_START_VALUE;
        isHolding = true;
        timeRemaining = GAME_TIME_SECONDS;

        InvokeRepeating("MarketReduceCoinValue", PRICE_DROP_START_DELAY, PRICE_DROP_REPEAT_RATE);
        InvokeRepeating("ReduceTimeRemaining", TIMEOVER_START_DELAY, TIMEOVER_REPEAT_RATE);
    }

    void MarketReduceCoinValue()
    {
        int randomIndex = Random.Range(0, marketImpactValues.Length);
        byte chosenImpact = marketImpactValues[randomIndex];

        if ((HODL_PERKS_THRESHOLD >= timeRemaining) && isHolding)
            coinValue += chosenImpact;
        else
            coinValue -= chosenImpact;

        if ((HODL_PERKS_THRESHOLD >= timeRemaining) && isHolding && timeRemaining <= MOON_PERKS_THRESHOLD)
            coinValue += Random.Range(MOON_RANGE_MIN, MOON_RANGE_MAX); // to the moon!

        OnCoinValueImpacted?.Invoke();

        if (coinValue <= 0)
        {
            coinValue = 0; // lock at zero
            OnCoinValueImpacted?.Invoke();
            GameOver();
        }
    }

    void ReduceTimeRemaining()
    {
        timeRemaining -= 1;
        OnClockTick?.Invoke();

        if (timeRemaining == 0 && gameIsActive)
            GameOver();

        if (coinValue == 0 && gameIsActive)
            GameOver();
    }
}