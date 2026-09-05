using UnityEngine;

public abstract class UpgradeData : ScriptableObject
{
    [SerializeField] private string upgradeId;
    public string GetUpgradeID()
    {
        return upgradeId;
    }
    [SerializeField] private string title;
    public string GetTitle()
    {
        return title;
    }
    [TextArea] [SerializeField] private string description;
    public string GetDescription()
    {
        return description;
    }
    [SerializeField] private Sprite icon;
    public Sprite GetIcon()
    {
        return icon;
    }
    [SerializeField] private int maxLevel = 1;
    public int GetMaxLevel()
    {
        return maxLevel;
    }

    /// <summary>
    /// Executes the upgrade logic on the target entity.
    /// </summary>
    /// <param name="target">The entity receiving the upgrade (e.g., the Player).</param>
    /// <param name="targetLevel">The level this upgrade is being raised to (1, 2, 3...).</param>
    public abstract void Apply(GameObject target, int targetLevel);
}