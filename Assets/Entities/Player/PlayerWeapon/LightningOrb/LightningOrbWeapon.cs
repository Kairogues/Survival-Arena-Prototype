using UnityEngine;
using System.Collections.Generic;

public class LightningOrbWeapon : Weapon
{
    [System.Serializable]
    public struct LevelConfig
    {
        public float bonusSpeedPercent;
        public int projectileCount;
        public float bonusAttackDamage;
    }
    [SerializeField] private List<LevelConfig> levelConfigList = new List<LevelConfig>
    {
        new LevelConfig
        {
            bonusSpeedPercent = 0.0f,
            projectileCount = 1,
            bonusAttackDamage = 0
        },
        new LevelConfig
        {
            bonusSpeedPercent = 0.2f,
            projectileCount = 1,
            bonusAttackDamage = 10
        },
        new LevelConfig
        {
            bonusSpeedPercent = 0.4f,
            projectileCount = 2,
            bonusAttackDamage = 15
        },
        new LevelConfig
        {
            bonusSpeedPercent = 0.6f,
            projectileCount = 2,
            bonusAttackDamage = 20
        },
        new LevelConfig
        {
            bonusSpeedPercent = 0.8f,
            projectileCount = 3,
            bonusAttackDamage = 30
        }
    };
    private LevelConfig currentLevelConfig;

    [SerializeField] private LightningOrbProjectile projectilePrefab;
    [SerializeField] private Transform firePoint;
    private bool hasSpawnedForThisLevel = false;



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
        hasSpawnedForThisLevel = false;
    }


    private void ExecuteAttack(AttackContext context)
    {
        if (hasSpawnedForThisLevel == false) {
            LightningOrbProjectile.ResetOrb();

            for (int i = 0; i < currentLevelConfig.projectileCount; i++)
            {
                SpawnOrb(context.position, context.rotationFromHeadingDirection);
            }

            hasSpawnedForThisLevel = true;
        }
    }


    public void SpawnOrb(Vector3 position, Quaternion rotation)
    {
        GameObject spawnedProjectile = GameManager.Instance.poolManager.Spawn(
                projectilePrefab.gameObject, 
                position, 
                rotation, 
                GameManager.Instance.entityManager.transform
        );

        if (spawnedProjectile.TryGetComponent<LightningOrbProjectile>(out var projectileInstance))
        {
            if (currentLevelConfig.bonusAttackDamage > 0)
            {
                projectileInstance.AddStatBuff(new StatBuff(StatType.ATTACK, StatBuffType.ADD, currentLevelConfig.bonusAttackDamage));
            }

            if (currentLevelConfig.bonusSpeedPercent > 0)
            {
                projectileInstance.AddStatBuff(new StatBuff(StatType.MOVEMENT_SPEED, StatBuffType.MULTIPLY, currentLevelConfig.bonusSpeedPercent));
            }
        }
    }
}