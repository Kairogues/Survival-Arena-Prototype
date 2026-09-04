using UnityEngine;

[CreateAssetMenu(fileName = "AttackInFacingDirectionBehavior", menuName = "Scriptable Objects/Weapon/Behavior/Attack In Facing Direction")]
public class AttackInFacingDirection : WeaponBehavior
{
    [SerializeField] private Projectile projectilePrefab;
    public override void Attack(AttackContext attackContext)
    {
        float angle = Mathf.Atan2(attackContext.facingDirection.y, attackContext.facingDirection.x) * Mathf.Rad2Deg;
        float offsetAmount = attackContext.facingDirection.x;
        
        GameManager.Instance.poolManager.Spawn(
                projectilePrefab.gameObject,
                attackContext.position + new Vector3(offsetAmount, 0, 0),
                Quaternion.Euler(0f, 0f, angle),
                GameManager.Instance.entityManager.transform);
    }
}
