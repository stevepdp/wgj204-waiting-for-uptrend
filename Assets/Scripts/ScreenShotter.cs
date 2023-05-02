using UnityEngine;
using System.IO;

public class ScreenShotter : MonoBehaviour
{
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

    void Start()
    {
        CreateScreenshotsDir();
    }

    void Update()
    {
        TakeScreenshot();
    }

    void CreateScreenshotsDir()
    {
        if (!Directory.Exists(fileDestination))
            Directory.CreateDirectory(fileDestination);
    }

    void TakeScreenshot()
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
