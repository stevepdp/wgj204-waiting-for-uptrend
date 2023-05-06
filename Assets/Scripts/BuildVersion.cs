using UnityEngine;
using UnityEngine.UI;

public class BuildVersion : MonoBehaviour
{
    Text buildVersionText;

    void Awake()
    {
        buildVersionText = GetComponent<Text>();
    }

    void Start()
    {
        SetVersionText();
    }

    void SetVersionText()
    {
        if (buildVersionText != null)
            buildVersionText.text = "v" + Application.version;
    }
}
