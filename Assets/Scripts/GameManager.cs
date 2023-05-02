using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // Game Values
    [SerializeField]
    const float _coinInWallet = 1;
    [SerializeField]
    private float _coinValue;
    [SerializeField]
    private float _coinValueStart;
    [SerializeField]
    private float _timeValue;
    [SerializeField]
    private string _postText;

    // Game States
    private bool _hodler = true;
    private int _hodlMinim = 60;
    private bool _gameInProgress = true;

    // IEnumerators
    private IEnumerator _marketEffectIEnumerator;
    private bool _stopMarketEffectEnum;

    void Awake()
    {
        //GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");

        /*if (objs.Length > 1)
        {
            //Destroy(this.gameObject);
            Destroy(objs[0]); // destroy the old one
        }*/

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        // Enforce game defaults
        _coinValueStart = 1000f; // 1 $GMJM coin is equal to $1000 fiat
        _coinValue = _coinValueStart;
        _stopMarketEffectEnum = false;
        _timeValue = 120; // 2 minutes game time remaining

        _marketEffectIEnumerator = MarketEffectIEnumerator();
        StartCoroutine(_marketEffectIEnumerator);
    }

    void Update()
    {
        if (_timeValue > 0 && _gameInProgress)
        {
            _timeValue -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if (_timeValue <= 0 && _gameInProgress)
        {
            OnGameOver();
        }

        if (_coinValue <= 0 && _gameInProgress)
        {
            _coinValue = 0; // lock at zero
            OnGameOver();
        }
    }

    IEnumerator MarketEffectIEnumerator()
    {
        while (_stopMarketEffectEnum == false)
        {
            cpuReduceCoinValue();

            // wait x seconds before running again
            yield return new WaitForSeconds(1f);
        }
    }

    void cpuReduceCoinValue()
    {
        int roll = Random.Range(1, 4);
        float impact = 0;

        switch (roll) {
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

        if ((_hodlMinim >= _timeValue) && _hodler)
        {
            _coinValue += impact;
            //Debug.Log("B: It's past 60 seconds and the player is hodling.");
        }
        else if (_timeValue > _hodlMinim && !_hodler)
        {
            _coinValue -= impact;
            //Debug.Log("C: CPU basic impact roll: -" + impact.ToString());
        }
        else
        {
            _coinValue -= impact;
            //Debug.Log("A: CPU basic impact roll: -" + impact.ToString());
        }

        // to the moon condition
        if ((_hodlMinim >= _timeValue) && _hodler && _timeValue < 3)
        {
            Debug.Log("To the moon!");
            _coinValue += Random.Range(250000,5000000);
        }

        if (_coinValue <= 0)
        {
            _coinValue = 0; // lock at zero
            OnGameOver();
        }
    }
    public float GetCoinValue()
    {
        return this._coinValue;
    }

    public string GetPostText()
    {
        return this._postText;
    }

    public float GetTimeElapsed()
    {  
        return this._timeValue;
    }

    public void OnGameOver()
    {
        _gameInProgress = false;

        //Debug.Log("GameOver condition met:");
        
        if(_coinValue < _coinValueStart) // loss
        {
            if (_coinValue <= 0) // zero
            {
                //Debug.Log("Going to 95_Zero");
                SceneManager.LoadScene("95_Zero");
            }
            else
            {
                //Debug.Log("Going to 96_Loss");
                SceneManager.LoadScene("96_Loss");
            }
        }
       

        if (_coinValue >= _coinValueStart * 100) // to the moon!
        {
            //Debug.Log("Going to 99_Moon");
            SceneManager.LoadScene("99_Moon");
        }
        else if (_coinValue >= _coinValueStart * 2) // doubled
        {
            //Debug.Log("Going to 98_Doubled");
            SceneManager.LoadScene("98_Doubled");
        }
        else if (_coinValue > _coinValueStart) // profit
        {
            //Debug.Log("Going to 97_Profit");
            SceneManager.LoadScene("97_Profit");
        }

        _stopMarketEffectEnum = true;
        StopCoroutine(_marketEffectIEnumerator);
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
                _postText = "keep calm and hodl";
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
                _postText = "you sent a tasty meme:\n\n\"";
                _postText += memes[Random.Range(0, memes.Length)];
                _postText += "\"\n\nthe market reacted positively";
                _coinValue += playerImpact;
                _hodler = false;
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
                _postText = "you sent a positive vibe:\n\n\"";
                _postText += vibes[Random.Range(0, vibes.Length)];
                _postText += "\"\n\nit shaped the actions of many";
                _coinValue += playerImpact;
                _hodler = false;
                break;
        }
    }
}