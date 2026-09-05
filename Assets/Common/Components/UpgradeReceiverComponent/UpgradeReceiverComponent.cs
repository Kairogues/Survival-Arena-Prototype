using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeReceiverComponent : MonoBehaviour
{
    private Dictionary<UpgradeData, int> upgradeLevels = new();

    public event Action<UpgradeData, int> OnUpgradeApplied;

    public int GetUpgradeLevel(UpgradeData upgrade)
    {
        return upgradeLevels.TryGetValue(upgrade, out int level) ? level : 0;
    }

    public bool IsMaxLevel(UpgradeData upgrade)
    {
        if (GetUpgradeLevel(upgrade) >= upgrade.GetMaxLevel())
        {
            return true;
        }

        return false; 
    }

    public void ApplyUpgrade(UpgradeData upgrade)
    {
        if (upgrade == null || IsMaxLevel(upgrade))
        {
            return;
        }

        int nextLevel = GetUpgradeLevel(upgrade) + 1;
        upgradeLevels[upgrade] = nextLevel;

        upgrade.Apply(gameObject, nextLevel);

        OnUpgradeApplied?.Invoke(upgrade, nextLevel);
    }
}