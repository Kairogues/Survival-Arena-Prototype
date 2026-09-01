using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RangedWeaponBehavior", menuName = "Scriptable Objects/Weapon/RangedWeaponBehavior")]
public class RangedWeaponBehavior : WeaponBehavior
{
    [SerializeField] private Projectile projectilePrefab;
    public override void Attack(AttackContext attackContext)
    {
        GameManager.Instance.poolManager.Spawn(
                    projectilePrefab.gameObject,
                    attackContext.position,
                    attackContext.rotation,
                    GameManager.Instance.entityManager.transform);
    }
}
