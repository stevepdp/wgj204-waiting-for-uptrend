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

    const float GMJM_START_VALUE = 1000f;
    const float GMJM_PRICE_DROP_DELAY = 1f;
    const float GMJM_PRICE_DROP_REPEATS = 1f;
    const byte HODL_MIN_TIME = 60;

    bool gameIsActive = true;
    bool isHolding = true;
    float coinValue = GMJM_START_VALUE;
    float timeRemaining = 120;
    string popupText;

    public float CoinValue
    {
        get { return coinValue; }
    }

    public string PopupText
    {
        get { return popupText; }
    }

    void Awake()
    {
        EnforceSingleInstance();
    }

    void Start()
    {
        NewGame();
    }

    void Update()
    {
        if (timeRemaining > 0 && gameIsActive)
            timeRemaining -= Time.deltaTime;
    }

    void LateUpdate()
    {
        if (timeRemaining <= 0 && gameIsActive)
            OnGameOver();

        if (coinValue <= 0 && gameIsActive)
        {
            coinValue = 0; // lock at zero
            OnGameOver();
        }
    }

    void EnforceSingleInstance()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void NewGame()
    {
        InvokeRepeating("ReduceCoinValue", GMJM_PRICE_DROP_DELAY, GMJM_PRICE_DROP_REPEATS);
    }

    void ReduceCoinValue()
    {
        int roll = Random.Range(1, 4);
        float impact = 0;

        switch (roll)
        {
            case 1:
                impact = 4;
                break;
            case 2:
                impact = 10;
                break;
            case 3:
                impact = 20;
                break;
            default:
                break;
        }

        if ((HODL_MIN_TIME >= timeRemaining) && isHolding)
        {
            coinValue += impact;
            //Debug.Log("B: It's past 60 seconds and the player is hodling.");
        }
        else if (timeRemaining > HODL_MIN_TIME && !isHolding)
        {
            coinValue -= impact;
            //Debug.Log("C: CPU basic impact roll: -" + impact.ToString());
        }
        else
        {
            coinValue -= impact;
            //Debug.Log("A: CPU basic impact roll: -" + impact.ToString());
        }

        // to the moon condition
        if ((HODL_MIN_TIME >= timeRemaining) && isHolding && timeRemaining < 3)
        {
            Debug.Log("To the moon!");
            coinValue += Random.Range(250000, 5000000);
        }

        if (coinValue <= 0)
        {
            coinValue = 0; // lock at zero
            OnGameOver();
        }
    }
    public float GetCoinValue()
    {
        return this.coinValue;
    }

    public float GetTimeElapsed()
    {
        return this.timeRemaining;
    }

    public void OnGameOver()
    {
        CancelInvoke("ReduceCoinValue");
        gameIsActive = false;

        if (coinValue < GMJM_START_VALUE) // loss
        {
            if (coinValue <= 0) // zero
            {
                SceneManager.LoadScene("95_Zero");
            }
            else
            {
                SceneManager.LoadScene("96_Loss");
            }
        }


        if (coinValue >= GMJM_START_VALUE * 100) // to the moon!
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

    public void SetPostText(string type)
    {
        int roll = Random.Range(1, 4);
        float playerImpact = 0;

        switch (roll)
        {
            case 1:
                playerImpact = 2;
                break;
            case 2:
                playerImpact = 5;
                break;
            case 3:
                playerImpact = 10;
                break;
            default:
                break;
        }

        switch (type)
        {
            case "hodl":
                popupText = "keep calm and hodl";
                break;

            case "meme":
                string[] memes = {
                    "this is fine",
                    "2021 GMJM hodlers!",
                    "i'm in GMJM for the tech",
                    "looking at my GMJM gains, aaand its gone",
                    "when you haven't checked GMJM in 2 minutes",
                    "just hodl it",
                    "hold the door!",
                    "GMJM is dead? Well... you must be new here",
                    "1.5 weeks in GMJM and loving it!",
                    "lets go to the moon!",
                    "market is going down and down. Me: this is fine.",
                    "plz keep crashing because we need to be able to buy a $400 GPU at RRP and not over $1k",
                    "stop holding crypto, hold me instead #cryptocrash",
                    "this is the mother of all crashes. #cryptocrash",
                    "sell everything. don't #hodl! #cryptocrash",
                    "while you are panic selling. They are buying",
                    "bear euphoria is really high",
                    "GMJM is hope.",
                    "#crypto be like: gettin' low low low low #gmjm",
                    "when you buy the dip and it wasn't the dip. It was the top of the next #downtrend",
                    "when it keeps dipping, you laugh it off in /r/cryptomemes and build a clicker game to cope",
                    "just checked my crypto balance and it told me to f**k off #cryptocrash #altcoin",
                    "it has been a privilege losing #GMJM with you today #cryptocrash #sinkingship"
                };
                popupText = "you sent a tasty meme:\n\n\"";
                popupText += memes[Random.Range(0, memes.Length)];
                popupText += "\"\n\nthe market reacted positively";
                coinValue += playerImpact;
                isHolding = false;
                break;

            case "vibe":
                string[] vibes = {
                    "keep calm, good news is just around the corner",
                    "dude invests $100M GMJM ecosystem fund",
                    "popular exchange adds GMJM to their alt roster",
                    "GMJM to be mined with renewable volcanic energy",
                    "dude invests $5m into solar-powered GMJM mining",
                    "country moves to classify GMJM an asset class",
                    "GMJM incentivizes renewable energy",
                    "funny how market sentiment can shift so quick",
                    "if GMJM were a cow, it'd be a nice jersey",
                    "the dip keeps dipping but I ain’t selling! #cryptocrash #HODL #cryptocurrency #buythedip",
                    "some people see #cryptocrash, while some people see #opportunities",
                    "you either #buythedip or HODL through the #Crypto dip!",
                    "GMJM is where investor stripes are earned #cryptocrash",
                    "just bought my first whole GMJM coin. I'm leveling up. No more micro shares! #cryptocrash",
                    "a $100,000 #GMJM is still possible in 2021!",
                    "\"#GMJM is the most advanced form of asset ever created.\"",
                    "nothing has changed, I'm still bullish! #gmjm",
                    "just want to check in and make sure everyone is doing ok? #gmjm #cryptocrash",
                    "what's everyone buying today? #cryptocrash",
                    "don't panic sell!! Everything will be fine!! Live to fight another day!! #cryptocrash #gmjm",
                    "you only lose when you sell #gmjm #altcoin",
                    "legends just hodl #hodling #gmjm"
                };
                popupText = "you sent a positive vibe:\n\n\"";
                popupText += vibes[Random.Range(0, vibes.Length)];
                popupText += "\"\n\nit shaped the actions of many";
                coinValue += playerImpact;
                isHolding = false;
                break;
        }
    }
}