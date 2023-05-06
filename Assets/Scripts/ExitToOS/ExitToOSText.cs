#if UNITY_STANDALONE
    using UnityEngine;
    using UnityEngine.UI;

    public class ExitToOSText : MonoBehaviour
    {
        Text exitButtonText;

        void Awake()
        {
            exitButtonText = GetComponent<Text>();
        }

        void Start()
        {
            SetButtonText();
        }

        void SetButtonText()
        {
            if (exitButtonText != null)
            {
                #if UNITY_STANDALONE_WIN
                    exitButtonText.text = "Exit to Windows";
                #elif UNITY_STANDALONE_OSX
                    exitButtonText.text = "Exit to macOS";
                #elif UNITY_STANDALONE_LINUX
                    exitButtonText.text = "Exit to Linux";
                #endif
            }
        }
    }
#endif