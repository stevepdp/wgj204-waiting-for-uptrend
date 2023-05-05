using UnityEngine;

public class PopupFrame : MonoBehaviour, IHideable
{
    SpriteRenderer framePart;

    void Awake()
    {
        framePart = GetComponent<SpriteRenderer>();
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

    public void Hide() {
        if (framePart != null)
            framePart.enabled = false;
    }

    public void Show()
    {
        if (framePart != null)
            framePart.enabled = true;
    }
}
