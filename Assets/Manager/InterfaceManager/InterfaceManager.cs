using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A singleton responsible for managing the user interface.
/// Manages UI, HUD, menus, popups, buttons, screen transitions, and decouples UI events from gameplay logic.
/// </summary>
public class InterfaceManager : MonoBehaviour
{
    public static InterfaceManager Instance { get; private set; }
    [SerializeField] private GameOverTransition gameOverTransition;
    public void SetGameOverTransition(GameOverTransition gameOverTransition)
    {
        this.gameOverTransition = gameOverTransition;
    }
    private LevelUpUI levelUpUI;
    public LevelUpUI GetLevelUpUI()
    {
        return levelUpUI;
    }
    public void SetLevelUpUI(LevelUpUI levelUpUI)
    {
        this.levelUpUI = levelUpUI;
    }
    private InfoHUD infoHUD;
    public InfoHUD GetInfoHUD()
    {
        return infoHUD;
    }
    public void SetInfoHUD(InfoHUD infoHUD)
    {
        this.infoHUD = infoHUD;
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


    public void OnPlayerDeath()
    {
        gameOverTransition.PlayColorAnimation(() => GameManager.Instance.LoadGameOverScene());
    }
}
