using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    [SerializeField] private int targetWidth = 540;
    [SerializeField] private int targetHeight = 960;

    void Awake()
    {
        // For portrait phone on PC desktop (fits 1080p monitors):
        Screen.SetResolution(540, 960, FullScreenMode.Windowed);

        // If landscape instead, uncomment this line:
        // Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
    }
}