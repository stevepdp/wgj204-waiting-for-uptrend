using UnityEngine;

public class ExitToOSButton : MonoBehaviour
{
    void Awake()
    {
        #if UNITY_EDITOR || UNITY_WEBGL
            DisableButton();
        #endif
    }

    public void ExitToOS() => Application.Quit();

    void DisableButton()
    {
        gameObject.SetActive(false);
    }
}
