using UnityEngine;
using System.Collections;

public class SwordWeapon : Weapon
{
    [SerializeField] private StaffProjectile projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private int projectileCount = 1;
    [SerializeField] private int piercingCount = 1;
    [SerializeField] private float bonusAttackDamage = 0;
    private WaitForSeconds baseProjectileDelayInterval = new WaitForSeconds(0.1f);



    protected override void ConfigureForLevel(int level)
    {
        switch (level)
        {
            case 1:
                projectileCount = 1;
                piercingCount = 1;
                break;
            case 2:
                projectileCount = 2;
                break;
            case 3:
                bonusAttackDamage = 10;
                projectileCount = 4;
                break;
            case 4:
                projectileCount = 5;
                piercingCount = 2;
                break;
            case 5:
                bonusAttackDamage = 20;
                projectileCount = 6;
                piercingCount = 3;
                break;
            default:
                bonusAttackDamage = 30;
                projectileCount = 6;
                piercingCount = 3;
                break;
        }

        onAttackAction = ExecuteAttack;
    }


    private void ExecuteAttack(AttackContext context)
    {
        StartCoroutine(FireBulletInBurst(context));
    }

    private IEnumerator FireBulletInBurst(AttackContext context)
    {
        for (int i = 0; i < projectileCount; i++)
        {
            SpawnBullet(context.position, context.rotationFromHeadingDirection);

            if (i < projectileCount - 1)
            {
                yield return baseProjectileDelayInterval;
            }
        }
    }


    private void SpawnBullet(Vector3 position, Quaternion rotation)
    {
        Vector3 spawnOrigin;
        if (firePoint != null) 
        {
            spawnOrigin = firePoint.position;
        } else
        {
            spawnOrigin = position;
        }

        GameObject spawnedProjectile = GameManager.Instance.poolManager.Spawn(
                projectilePrefab.gameObject, 
                spawnOrigin, 
                rotation, 
                GameManager.Instance.entityManager.transform
        );

        if (spawnedProjectile.TryGetComponent<StaffProjectile>(out var projectileInstance))
        {
            projectileInstance.SetPiercingCount(piercingCount);
            if (bonusAttackDamage > 0)
            {
                projectileInstance.AddStatBuff(new StatBuff(StatType.ATTACK, StatBuffType.ADD, bonusAttackDamage));
            }
        }
    }
}