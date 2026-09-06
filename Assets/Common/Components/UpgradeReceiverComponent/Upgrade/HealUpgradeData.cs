using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Heal Upgrade")]
public class HealUpgradeData : UpgradeData
{
    [SerializeField] private float amount = 20.0f;

    public override void Apply(GameObject target, int targetLevel)
    {
        target.GetComponent<LifeComponent>().Heal(amount);
    }
}