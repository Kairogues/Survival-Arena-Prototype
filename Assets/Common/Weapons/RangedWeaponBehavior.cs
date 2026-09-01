using UnityEngine;

[CreateAssetMenu(fileName = "RangedWeaponBehavior", menuName = "Scriptable Objects/Weapon/RangedWeaponBehavior")]
public class RangedWeaponBehavior : WeaponBehavior
{
    public override void Attack(AttackContext attackContext)
    {
        /*
        if (!CanAttack())
        {
            return;
        }
        
        if (weaponData == null)
        {
            return;
        }

        // Quaternion spawnRotation = Quaternion.Euler(initRotation);
        GameManager.Instance.poolManager.Spawn(
                    weaponData.projectile.gameObject,
                    initPosition,
                    initRotation,
                    GameManager.Instance.entityManager.transform);
        nextCanAttackTime = Time.time + weaponData.cooldown;
        //StartCoroutine(CoolingDown());
        */
    }
}
