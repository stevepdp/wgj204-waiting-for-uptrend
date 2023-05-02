using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPostController : MonoBehaviour
{
    private GameManager _gameManager;

    [SerializeField]
    private Text _postText;

    void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        DisplayPostText(); // constantly read from the GameManager "database"
    }

    public void DisplayPostText()
    {
        string post = _gameManager.GetPostText();
        _postText.text = post;
    }

    public void HidePostUI()
    {
        //Debug.Log("Hide post UI");
        this.gameObject.SetActive(false);
    }

    public void ShowPostUI()
    {
        //Debug.Log("Show post UI");
        this.gameObject.SetActive(true);
    }
}
