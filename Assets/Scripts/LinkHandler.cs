using UnityEngine;

public class LinkHandler : MonoBehaviour
{
    public void OpenURL(string url) => Application.OpenURL(url);
}
