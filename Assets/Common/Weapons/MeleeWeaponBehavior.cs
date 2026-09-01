using UnityEngine;

[CreateAssetMenu(fileName = "Melee", menuName = "Scriptable Objects/Weapon/WeaponData")]
public class MeleeWeaponBehavior : WeaponBehavior
{
    public override void Attack(AttackContext attackContext)
    {
        /*
        if (weaponData == null)
        {
            return;
        }

        bool isFacingLeft = Mathf.Abs(Mathf.DeltaAngle(initRotation.eulerAngles.z, 180f)) < 90f 
                            || initRotation.eulerAngles.y == 180f;

        float targetZAngle = isFacingLeft ? 180f : 0f;
        Quaternion spawnRotation = Quaternion.Euler(0f, 0f, targetZAngle);


        GameManager.Instance.poolManager.Spawn(
            weaponData.projectile.gameObject,
            initPosition,
            spawnRotation,
            GameManager.Instance.entityManager.transform
        );

        nextCanAttackTime = Time.time + weaponData.cooldown;
        */
    }
}
