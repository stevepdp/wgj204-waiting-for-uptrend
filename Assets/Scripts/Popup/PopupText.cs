using Random = UnityEngine.Random;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupText : MonoBehaviour
{
    public static event Action OnPopupChange;

    const byte PLAYER_IMPACT_LOW = 2;
    const byte PLAYER_IMPACT_MED = 5;
    const byte PLAYER_IMPACT_HIGH = 10;

    Text popupText;
    byte[] playerImpactValues = new byte[] {
        PLAYER_IMPACT_LOW,
        PLAYER_IMPACT_MED,
        PLAYER_IMPACT_HIGH
    };
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

    void Awake()
    {
        popupText = GetComponent<Text>();
    }

    void OnEnable()
    {
        PlayerInput.OnPlayerChoice += SetPostText;
    }

    void OnDisable()
    {
        PlayerInput.OnPlayerChoice -= SetPostText;
    }

    void SetPostText(int postType)
    {
        int randomIndex = Random.Range(0, playerImpactValues.Length);
        byte chosenPlayerImpact = playerImpactValues[randomIndex];

        GameManager.Instance.IsHolding = false;
        GameManager.Instance.CoinValue += chosenPlayerImpact;

        if (postType == (int)PlayerChoiceType.HODL)
        {
            popupText.text = "keep calm and hodl";
        }
        else if (postType == (int)PlayerChoiceType.MEME)
        {
            popupText.text = "you sent a tasty meme:\n\n\"";
            popupText.text += memes[Random.Range(0, memes.Length)];
            popupText.text += "\"\n\nthe market reacted positively";
        }
        else if (postType == (int)PlayerChoiceType.VIBE)
        {
            popupText.text = "you sent a positive vibe:\n\n\"";
            popupText.text += vibes[Random.Range(0, vibes.Length)];
            popupText.text += "\"\n\nit shaped the actions of many";
        }

        OnPopupChange?.Invoke();
    }
}
