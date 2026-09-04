using UnityEngine;

[CreateAssetMenu(fileName = "AttackInHeadingDirectionBehavior", menuName = "Scriptable Objects/Weapon/Behavior/Attack In Heading Direction")]
public class AttackInHeadingDirection : WeaponBehavior
{
    [SerializeField] private Projectile projectilePrefab;
    public override void Attack(AttackContext attackContext)
    {
        GameManager.Instance.poolManager.Spawn(
                projectilePrefab.gameObject,
                attackContext.position,
                attackContext.rotationFromHeadingDirection,
                GameManager.Instance.entityManager.transform);
    }
}
