using UnityEngine;

/// <summary>
/// A singleton responsible for managing the user interface.
/// Manages UI, HUD, menus, popups, buttons, screen transitions, and decouples UI events from gameplay logic.
/// </summary>
public class InterfaceManager : MonoBehaviour
{
    public static InterfaceManager Instance { get; private set; }
    private LevelUpUI levelUpUI;
    public LevelUpUI GetLevelUpUI()
    {
        return levelUpUI;
    }
    public void SetLevelUpUI(LevelUpUI levelUpUI)
    {
        this.levelUpUI = levelUpUI;
    }


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


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
