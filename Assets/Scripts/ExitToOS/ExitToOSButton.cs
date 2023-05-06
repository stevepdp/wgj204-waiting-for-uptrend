using UnityEngine;

public class ExitToOSButton : MonoBehaviour
{
    void Awake()
    {
        DisableButton();
    }

    public void ExitToOS() => Application.Quit();

    void DisableButton()
    {
        #if UNITY_EDITOR || UNITY_WEBGL
                gameObject.SetActive(false);
        #endif
    }
}
