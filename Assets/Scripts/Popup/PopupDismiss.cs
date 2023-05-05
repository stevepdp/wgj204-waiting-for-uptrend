using System;
using UnityEngine;

public class PopupDismiss : MonoBehaviour
{
    public static event Action OnDismissPopup;

    public void DismissPopup()
    {
        OnDismissPopup?.Invoke();
    }
}
