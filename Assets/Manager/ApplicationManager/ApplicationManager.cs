using UnityEngine;

/// <summary>
/// A singleton responsible for high-level application services.
/// Orchestrates infrastructure subsystems such as save/load, audio management, input devices, network connectivity,...
/// </summary>
public class ApplicationManager : MonoBehaviour
{
    public static ApplicationManager Instance { get; private set; }
    [SerializeField] public AudioManager audioManager;



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        DontDestroyOnLoad(gameObject);
    }
}
