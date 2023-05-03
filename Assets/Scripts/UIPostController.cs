using UnityEngine;
using UnityEngine.UI;

public class UIPostController : MonoBehaviour
{
    [SerializeField]
    private Text _postText;

    private void Update()
    {
        DisplayPostText(); // constantly read from the GameManager "database"
    }

    public void DisplayPostText()
    {
        _postText.text = GameManager.Instance.PopupText;
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
