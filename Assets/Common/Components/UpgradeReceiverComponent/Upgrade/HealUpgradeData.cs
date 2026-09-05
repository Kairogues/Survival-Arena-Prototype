using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Heal Upgrade")]
public class HealUpgradeData : UpgradeData
{
    [SerializeField] private float amount = 20.0f;

    public override void Apply(GameObject target, int targetLevel)
    {
        Stat healthStat = target.GetComponent<StatComponent>().GetStat(StatType.HEALTH);
        healthStat.UpdateStat(healthStat.GetCurrentValue() + amount);
    }
}