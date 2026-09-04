using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "OrbitWeaponBehavior", menuName = "Scriptable Objects/Weapon/OrbitWeaponBehavior")]
public class OrbitWeaponBehavior : WeaponBehavior
{
    [SerializeField] private Projectile projectilePrefab;
    public override void Attack(AttackContext attackContext)
    {
        if (LightningOrb.GetCurrentActiveOrbs().Count < LightningOrb.GetCurrentMaxActiveOrbs()) {
            GameManager.Instance.poolManager.Spawn(
                    projectilePrefab.gameObject,
                    attackContext.position,
                    Quaternion.identity,
                    GameManager.Instance.entityManager.transform);

        }
    }
}
