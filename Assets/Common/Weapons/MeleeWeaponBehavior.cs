using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeaponBehavior", menuName = "Scriptable Objects/Weapon/MeleeWeaponBehavior")]
public class MeleeWeaponBehavior : WeaponBehavior
{
    [SerializeField] private Projectile projectilePrefab;
    public override void Attack(AttackContext attackContext)
    {
        float angle = Mathf.Atan2(attackContext.direction.y, attackContext.direction.x) * Mathf.Rad2Deg;
        float offsetAmount = 2.0f * attackContext.direction.x;
        
        GameManager.Instance.poolManager.Spawn(
                projectilePrefab.gameObject,
                attackContext.position + new Vector3(offsetAmount, 0, 0),
                Quaternion.Euler(0f, 0f, angle),
                GameManager.Instance.entityManager.transform);
    }
}
