using UnityEngine;

public class AttackWhenCloseWeapon : Weapon
{
    [SerializeField] private AttackWhenCloseProjectile projectilePrefab;
    [SerializeField] private Transform firePoint;
    private bool hasSpawned = false;



    protected override void ConfigureForLevel(int level)
    {
        onAttackAction = ExecuteAttack;
    }


    private void ExecuteAttack(AttackContext context)
    {
        if (hasSpawned == false) {
            SpawnDamageArea(firePoint.position, Quaternion.identity);
            hasSpawned = true;
        }
    }


    public void SpawnDamageArea(Vector3 position, Quaternion rotation)
    {
        GameManager.Instance.poolManager.Spawn(
                projectilePrefab.gameObject, 
                position, 
                rotation, 
                firePoint
        );
    }
}