using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private List<UpgradeData> allUpgrades;
    // This is used when there is no more upgrades available
    [SerializeField] private HealUpgradeData healUpgrade;
    private const int UPGRADE_OPTION = 3;

    public List<UpgradeData> GetRandomUpgradeOptions(UpgradeReceiverComponent receiver)
    {
        // Looks like Javascript!!
        List<UpgradeData> availableUpgrades = allUpgrades
            .Where(upgrade => !receiver.IsMaxLevel(upgrade))
            .ToList();

        List<UpgradeData> selectedUpgrades = new();

        int countToPick = Mathf.Min(UPGRADE_OPTION, availableUpgrades.Count);
        for (int i = 0; i < countToPick; i++)
        {
            int randomIndex = Random.Range(0, availableUpgrades.Count);
            selectedUpgrades.Add(availableUpgrades[randomIndex]);
            
            availableUpgrades.RemoveAt(randomIndex);
        }

        while (selectedUpgrades.Count < UPGRADE_OPTION)
        {
            selectedUpgrades.Add(healUpgrade);
        }

        return selectedUpgrades;
    }
}