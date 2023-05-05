using UnityEngine;

public class PopupCanvas : MonoBehaviour, IHideable
{
    Canvas popupCanvas;

    void Awake()
    {
        popupCanvas = GetComponent<Canvas>();
    }

    void OnEnable()
    {
        PopupDismiss.OnDismissPopup += Hide;
        PopupText.OnPopupChange += Show;
    }

    void OnDisable()
    {
        PopupDismiss.OnDismissPopup -= Hide;
        PopupText.OnPopupChange -= Show;
    }

    public void Hide()
    {
        if (popupCanvas != null)
            popupCanvas.enabled = false;
    }

    public void Show()
    {
        if (popupCanvas != null)
            popupCanvas.enabled = true;
    }

}
