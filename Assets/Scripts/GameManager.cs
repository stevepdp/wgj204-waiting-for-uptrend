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
    int coinValue;
    string popupText;

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

    // TODO: Sell button calls this too. To be replaced with events perhaps?
    public void GameOver()
    {
        CancelInvoke("ReduceCoinValue");
        CancelInvoke("ReduceTimeRemaining");
        gameIsActive = false;

        // TODO: Send to a single dynamic scene choosing from an array of graphics perhaps?
        if (coinValue < GMJM_START_VALUE) // loss
        {
            if (coinValue <= 0) // zero
                SceneManager.LoadScene("95_Zero");
            else
                SceneManager.LoadScene("96_Loss");
        }
        else if (coinValue >= GMJM_START_VALUE * 100) // to the moon!
        { 
            SceneManager.LoadScene("99_Moon");
        }
        else if (coinValue >= GMJM_START_VALUE * 2) // doubled
        {
            SceneManager.LoadScene("98_Doubled");
        }  
        else if (coinValue > GMJM_START_VALUE) // profit
        {
            SceneManager.LoadScene("97_Profit");
        }
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
        int randomIndex = UnityEngine.Random.Range(0, marketImpactValues.Length);
        byte chosenImpact = marketImpactValues[randomIndex];

        if ((HODL_PERKS_THRESHOLD >= timeRemaining) && isHolding)
            coinValue += chosenImpact;
        else if (timeRemaining > HODL_PERKS_THRESHOLD && !isHolding)
            coinValue -= chosenImpact;
        else
            coinValue -= chosenImpact;

        if ((HODL_PERKS_THRESHOLD >= timeRemaining) && isHolding && timeRemaining < MOON_PERKS_THRESHOLD)
            coinValue += Random.Range(MOON_RANGE_MIN, MOON_RANGE_MAX); // to the moon!

        OnCoinValueImpacted?.Invoke();

        if (coinValue <= 0)
        {
            coinValue = 0; // lock at zero
            GameOver();
        }
    }

    void ReduceTimeRemaining()
    {
        timeRemaining -= 1;

        if (timeRemaining == 0 && gameIsActive)
            GameOver();

        if (coinValue == 0 && gameIsActive)
            GameOver();
    }
}