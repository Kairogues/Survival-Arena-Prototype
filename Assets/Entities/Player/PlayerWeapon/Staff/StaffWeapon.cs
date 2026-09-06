using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StaffWeapon : Weapon
{
    [System.Serializable]
    public struct LevelConfig
    {
        public int projectileCount;
        public int piercingCount;
        public float bonusAttackDamage;
    }

    [SerializeField] private List<LevelConfig> levelConfigList = new List<LevelConfig>
    {
        new LevelConfig
        {
            projectileCount = 1,
            piercingCount = 1,
            bonusAttackDamage = 0
        },
        new LevelConfig
        {
            projectileCount = 2,
            piercingCount = 1,
            bonusAttackDamage = 0
        },
        new LevelConfig
        {
            projectileCount = 4,
            piercingCount = 1,
            bonusAttackDamage = 10
        },
        new LevelConfig
        {
            projectileCount = 5,
            piercingCount = 2,
            bonusAttackDamage = 10
        },
        new LevelConfig
        {
            projectileCount = 6,
            piercingCount = 3,
            bonusAttackDamage = 20
        }
    };

    private LevelConfig currentLevelConfig;
    public LevelConfig GetLevelConfig(int currentLevel)
    {
        return levelConfigList[currentLevel];
    }

    [SerializeField] private StaffProjectile projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private AudioClip attackSoundFX;
    private WaitForSeconds baseProjectileDelayInterval = new WaitForSeconds(0.1f);



    protected override void ConfigureForLevel(int level)
    {
        if (levelConfigList.Count != 0 && level <= levelConfigList.Count)
        {
            currentLevelConfig = levelConfigList[level - 1];
        } else
        {
            Debug.LogWarning("WARNING: There is no config for this level");
        }

        onAttackAction = ExecuteAttack;
    }


    private void ExecuteAttack(AttackContext context)
    {
        StartCoroutine(FireBulletInBurst(context));
    }

    private IEnumerator FireBulletInBurst(AttackContext context)
    {
        for (int i = 0; i < currentLevelConfig.projectileCount; i++)
        {
            ApplicationManager.Instance.audioManager.PlaySoundFX(attackSoundFX, transform, 1f);
            SpawnBullet(context.position, context.rotationFromHeadingDirection);

            if (i < currentLevelConfig.projectileCount - 1)
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
            projectileInstance.SetPiercingCount(currentLevelConfig.piercingCount);
            if (currentLevelConfig.bonusAttackDamage > 0)
            {
                projectileInstance.AddStatBuff(new StatBuff(StatType.ATTACK, StatBuffType.ADD, currentLevelConfig.bonusAttackDamage));
            }
        }
    }
}