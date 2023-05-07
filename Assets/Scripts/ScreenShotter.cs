#if UNITY_EDITOR
using System.IO;
using UnityEngine;

public class ScreenShotter : MonoBehaviour
{
    static ScreenShotter instance;
    public static ScreenShotter Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<ScreenShotter>();
            if (instance == null)
                instance = Instantiate(new GameObject("ScreenShotter")).AddComponent<ScreenShotter>();
            return instance;
        }
    }

    [Header("Filename Settings")]

    [SerializeField, Tooltip("Set the destination path. This folder will be placed at the top of your project directory.")]
    string fileDestination = "Screenshots/";

    [SerializeField, Tooltip("Set the start of the file name.")]
    string filePrefix = "screenshot_";

    [SerializeField, Tooltip("Set date/time format")]
    string fileDateFormat = "yyyy-MM-dd_HH-mm-ss";

    [SerializeField, Tooltip("Set the file extension. Note that it'll be a PNG regardless however.")]
    string fileExt = ".png";

    [Header("Quality Settings")]
    [SerializeField, Range(1, 4), Tooltip("1x is standard. 4 is 4x the standard resolution.")]
    int scaleFactor = 1;

    [Header("Other Settings")]
    [SerializeField, Tooltip("The number of seconds between each capture.")]
    float captureInterval = 3f;
    float timeSinceCapture;
    [SerializeField] bool isEnabled;

    void Awake()
    {
        EnforceSingleInstance();
    }

    void Update()
    {
        CreateScreenshotsDir();
        TakeScreenshot();
    }

    void CreateScreenshotsDir()
    {
        if (!Directory.Exists(fileDestination) && isEnabled)
            Directory.CreateDirectory(fileDestination);
    }
    void EnforceSingleInstance()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


    void TakeScreenshot()
    {
        if (isEnabled)
        {
            timeSinceCapture += Time.deltaTime;

            if (timeSinceCapture >= captureInterval)
            {
                timeSinceCapture -= captureInterval;
                string fileName = filePrefix + System.DateTime.Now.ToString(fileDateFormat) + fileExt;
                string filePath = Path.Combine(fileDestination, fileName);
                ScreenCapture.CaptureScreenshot(filePath, scaleFactor);
            }
        }
        
    }
}
#endif