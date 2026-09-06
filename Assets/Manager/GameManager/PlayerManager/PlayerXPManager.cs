using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXPManager : MonoBehaviour
{
    public event Action<int, int> GainedXP;
    public event Action<int> LeveledUp;
    
    [SerializeField] private XPGrowthConfig xpGrowthConfig;
    private int currentXP = 0;
    private int currentMaxXP;
    private int currentLevel = 1;



    private void Awake()
    {
        currentMaxXP = GetXPRequiredForLevelUp(2);
    }
    

    public int GetXPRequiredForLevelUp(int level)
    {
        return xpGrowthConfig.GetXPForLevel(level);
    }


    public void AddXP(int amount)
    {
        currentXP += amount;

        while (currentXP >= currentMaxXP)
        {
            currentXP -= currentMaxXP;
            currentLevel++;

            LeveledUp?.Invoke(currentLevel);
            TriggerLevelUp();
            Debug.Log("Level up! Now in level " + currentLevel);

            currentMaxXP = GetXPRequiredForLevelUp(currentLevel + 1);
        }

        GainedXP?.Invoke(currentXP, currentMaxXP);
    }

    public void TriggerLevelUp()
    {
        UpgradeReceiverComponent currentUpgradeReceiverComponent = GameManager.Instance.playerManager.currentPlayer.GetUpgradeReceiverComponent();
        List<UpgradeData> options = GameManager.Instance.upgradeManager.GetRandomUpgradeOptions(currentUpgradeReceiverComponent);
        InterfaceManager.Instance.GetLevelUpUI().Show(options, currentUpgradeReceiverComponent);
    }
}
