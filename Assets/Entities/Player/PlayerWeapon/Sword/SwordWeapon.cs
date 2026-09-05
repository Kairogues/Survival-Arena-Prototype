using UnityEngine;
using System.Collections.Generic;

public class SwordWeapon : Weapon
{
    [System.Serializable]
    public struct LevelConfig
    {
        public float bonusAttackDamage;
    }
    [SerializeField] private List<LevelConfig> levelConfigList = new List<LevelConfig>
    {
        new LevelConfig
        {
            bonusAttackDamage = 0
        },
        new LevelConfig
        {
            bonusAttackDamage = 0
        },
        new LevelConfig
        {
            bonusAttackDamage = 10
        },
        new LevelConfig
        {
            bonusAttackDamage = 10
        },
        new LevelConfig
        {
            bonusAttackDamage = 20
        }
    };

    private LevelConfig currentLevelConfig;
    public LevelConfig GetLevelConfig(int currentLevel)
    {
        return levelConfigList[currentLevel];
    }
    [SerializeField] private SwordProjectile projectilePrefab;
    [SerializeField] private Transform firePointFront;
    [SerializeField] private Transform firePointBack;



    protected override void ConfigureForLevel(int level)
    {
        if (levelConfigList.Count != 0)
        {
            currentLevelConfig = levelConfigList[level - 1];
        }

        switch (level)
        {
            case 1:
                onAttackAction = AttackLevel1;
                break;
            case 2:
                onAttackAction = AttackLevel2;
                break;
            case 3:
                onAttackAction = AttackLevel3;
                break;
            case 4:
                onAttackAction = AttackLevel4;
                break;
            case 5:
                onAttackAction = AttackLevel5;
                break;
            default:
                onAttackAction = AttackLevel5;
                break;
        }
    }


    private void AttackLevel1(AttackContext context)
    {
        float facingAngle = Mathf.Atan2(context.facingDirection.y, context.facingDirection.x) * Mathf.Rad2Deg;
        Quaternion facingAngleQuaternionFront = Quaternion.Euler(0f, 0f, facingAngle);

        SpawnBlade(firePointFront.position, facingAngleQuaternionFront);
    }

    private void AttackLevel2(AttackContext context)
    {
        float facingAngle = Mathf.Atan2(context.facingDirection.y, context.facingDirection.x) * Mathf.Rad2Deg;
        Quaternion facingAngleQuaternionFront = Quaternion.Euler(0f, 0f, facingAngle);
        Quaternion facingAngleQuaternionBack = Quaternion.Euler(0f, 0f, 180.0f + facingAngle);

        SpawnBlade(firePointFront.position, facingAngleQuaternionFront);
        SpawnBlade(firePointBack.position, facingAngleQuaternionBack);
    }

    private void AttackLevel3(AttackContext context)
    {
        float facingAngle = Mathf.Atan2(context.facingDirection.y, context.facingDirection.x) * Mathf.Rad2Deg;
        Quaternion facingAngleQuaternionFront = Quaternion.Euler(0f, 0f, facingAngle);
        Quaternion facingAngleQuaternionBack = Quaternion.Euler(0f, 0f, 180.0f + facingAngle);

        SpawnBlade(firePointFront.position, facingAngleQuaternionFront);
        SpawnBlade(firePointBack.position, facingAngleQuaternionBack);
    }


    private void AttackLevel4(AttackContext context)
    {
        float facingAngle = Mathf.Atan2(context.facingDirection.y, context.facingDirection.x) * Mathf.Rad2Deg;
        Quaternion facingAngleQuaternionFront = Quaternion.Euler(0f, 0f, facingAngle);
        Quaternion facingAngleQuaternionBack = Quaternion.Euler(0f, 0f, 180.0f + facingAngle);
   
        SpawnBlade(firePointFront.position, facingAngleQuaternionFront * Quaternion.Euler(0, 0, -10f));
        SpawnBlade(firePointFront.position, facingAngleQuaternionFront * Quaternion.Euler(0, 0, 10f));

        SpawnBlade(firePointBack.position, facingAngleQuaternionBack);
    }


    private void AttackLevel5(AttackContext context)
    {
        float facingAngle = Mathf.Atan2(context.facingDirection.y, context.facingDirection.x) * Mathf.Rad2Deg;
        Quaternion facingAngleQuaternionFront = Quaternion.Euler(0f, 0f, facingAngle);
        Quaternion facingAngleQuaternionBack = Quaternion.Euler(0f, 0f, 180.0f + facingAngle);
   
        SpawnBlade(firePointFront.position, facingAngleQuaternionFront * Quaternion.Euler(0, 0, -15f));
        SpawnBlade(firePointFront.position, facingAngleQuaternionFront);
        SpawnBlade(firePointFront.position, facingAngleQuaternionFront * Quaternion.Euler(0, 0, 15f));

        SpawnBlade(firePointBack.position, facingAngleQuaternionBack);
    }


    public void SpawnBlade(Vector3 position, Quaternion rotation)
    {
        GameObject spawnedProjectile = GameManager.Instance.poolManager.Spawn(
                projectilePrefab.gameObject, 
                position, 
                rotation, 
                GameManager.Instance.entityManager.transform
        );

        if (spawnedProjectile.TryGetComponent<SwordProjectile>(out var projectileInstance))
        {
            if (currentLevelConfig.bonusAttackDamage > 0)
            {
                projectileInstance.AddStatBuff(new StatBuff(StatType.ATTACK, StatBuffType.ADD, currentLevelConfig.bonusAttackDamage));
            }
        }
    }
}