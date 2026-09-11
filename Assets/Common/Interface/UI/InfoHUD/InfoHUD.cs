using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image healthbar;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Image levelBar;



    private void Start()
    {
        InterfaceManager.Instance.SetInfoHUD(this);
        GameManager.Instance.waveManager.WaveStarted += OnWaveStart;
        GameManager.Instance.playerManager.currentPlayerLifeComponent.HealthChanged += OnHealthChanged;
        GameManager.Instance.playerManager.GetPlayerXPManager().LeveledUp += OnLevelUp;
        GameManager.Instance.playerManager.GetPlayerXPManager().GainedXP += OnGainedXP;
    }


    private void OnWaveStart(WaveData waveData)
    {
        waveText.text = "WAVE " + waveData.waveIndex;
    }


    private void OnHealthChanged(float oldHealth, float newHealth, float maxHealth)
    {
        healthText.text = newHealth + "/" + maxHealth;
        healthbar.fillAmount = newHealth / maxHealth;
    }


    private void OnLevelUp(int newLevel)
    {
        levelText.text = "Lv " + newLevel;
        levelBar.fillAmount = 0.0f;
    }


    private void OnGainedXP(int currentAmount, int maxAmount)
    {
        levelBar.fillAmount = (float) currentAmount / maxAmount;
    }
}
