using UnityEngine;
using TMPro;

public class InfoHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI xpText;



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
        healthText.text = "Health: " + newHealth + "/" + maxHealth;
    }


    private void OnLevelUp(int newLevel)
    {
        levelText.text = "Level: " + newLevel;
    }


    private void OnGainedXP(int currentAmount, int maxAmount)
    {
        xpText.text = "XP " + currentAmount + "/" + maxAmount;
    }
}
