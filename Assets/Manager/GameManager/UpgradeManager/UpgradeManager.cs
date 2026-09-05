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
        // Filter out any upgrade the player has already maxed out
        // Looks like Javascript!!
        List<UpgradeData> availableUpgrades = allUpgrades
            .Where(upgrade => !receiver.IsMaxLevel(upgrade))
            .ToList();

        List<UpgradeData> selectedUpgrades = new();

        if (availableUpgrades.Count < UPGRADE_OPTION)
        {
            for (int i = 0; i < availableUpgrades.Count; i++)
            {
                int randomIndex = Random.Range(0, availableUpgrades.Count);
                selectedUpgrades.Add(availableUpgrades[randomIndex]);
                availableUpgrades.RemoveAt(randomIndex);
            }

            for (int i = 0; i < UPGRADE_OPTION - availableUpgrades.Count; i++)
            {
                selectedUpgrades.Add(healUpgrade);
            }
        }

        for (int i = 0; i < UPGRADE_OPTION; i++)
        {
            int randomIndex = Random.Range(0, availableUpgrades.Count);
            selectedUpgrades.Add(availableUpgrades[randomIndex]);
            availableUpgrades.RemoveAt(randomIndex);
        }

        return selectedUpgrades;
    }
}