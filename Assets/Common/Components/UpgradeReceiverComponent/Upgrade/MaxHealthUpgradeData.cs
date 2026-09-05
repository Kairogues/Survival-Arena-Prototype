using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Max Health Upgrade")]
public class MaxHealthUpgradeData : UpgradeData
{
    [SerializeField] private float amount = 10.0f;

    public override void Apply(GameObject target, int targetLevel)
    {
        Stat healthStat = target.GetComponent<StatComponent>().GetStat(StatType.HEALTH);
        healthStat.SetMaxValue(healthStat.GetMaxValue() + amount);
    }
}