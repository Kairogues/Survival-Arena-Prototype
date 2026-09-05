using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Stat Upgrade")]
public class StatUpgradeData : UpgradeData
{
    [SerializeField] private StatType statToModify;
    [SerializeField] private StatBuffType statBuffType;
    [SerializeField] private float amount = 10.0f;

    public override void Apply(GameObject target, int targetLevel)
    {
        target.GetComponent<StatComponent>().AddBuff(new StatBuff(statToModify, statBuffType, amount));
    }
}