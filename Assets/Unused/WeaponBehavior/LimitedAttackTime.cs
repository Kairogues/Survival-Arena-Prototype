using UnityEngine;

[CreateAssetMenu(fileName = "LimitedAttackTimeBehavior", menuName = "Scriptable Objects/Weapon/Behavior/Limited Attack Time")]
public class LimitedAttackTime : WeaponBehavior
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private int maxAttackCount;
    private int currentAttackCount = 0;



    public override void Attack(AttackContext attackContext)
    {
        while (currentAttackCount < maxAttackCount) {
            GameManager.Instance.poolManager.Spawn(
                    projectilePrefab.gameObject,
                    attackContext.position,
                    Quaternion.identity,
                    GameManager.Instance.entityManager.transform);
                    
            currentAttackCount++;
        }
    }
}
