#if PLATFORM_STANDALONE
using UnityEngine;

public class FullscreenManager : MonoBehaviour
{
    static FullscreenManager instance;
    public static FullscreenManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<FullscreenManager>();
            if (instance == null)
                instance = Instantiate(new GameObject("FullscreenManager")).AddComponent<FullscreenManager>();
            return instance;
        }
    }

    const int DEFAULT_WIDTH = 1280;
    const int DEFAULT_HEIGHT = 720;

    void Awake()
    {
        EnforceSingleInstance();
    }

    void Update()
    {
        ToggleFullScreen();
    }

    void DisableFullScreen() => Screen.SetResolution(DEFAULT_WIDTH, DEFAULT_HEIGHT, false);

    void EnableFullScreen() => Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);

    void EnforceSingleInstance()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void ToggleFullScreen()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            DisableFullScreen();

        if ((Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Return)) ||
            (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.F)) ||
            (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Return)) ||
            (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F)) ||
            (Input.GetKey(KeyCode.LeftCommand) && Input.GetKeyDown(KeyCode.Return)) ||
            (Input.GetKey(KeyCode.LeftCommand) && Input.GetKeyDown(KeyCode.F)))
        {
            if (Screen.fullScreen)
                DisableFullScreen();
            else
                EnableFullScreen();
        }
    }
}
#endif