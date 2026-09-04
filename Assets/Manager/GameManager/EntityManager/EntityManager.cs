using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// A Manager responsible for registry, tracking, and monitoring of active in-level entities.
/// </summary>
public class EntityManager : MonoBehaviour
{
    private const int MAX_MONSTER_WEIGHT = 300;
    private const float XP_MERGE_RANGE = 200.0F;
    private const float XP_DESPAWN_RANGE = 300.0F;
    private const float ENEMY_DESPAWN_RANGE = 300.0F;

    // =============== ENEMY ===============
    [SerializeField] private List<Enemy> currentEnemyList = new();
    [SerializeField] private int currentMonsterWeight = 0;
    public int GetCurrentMonsterWeight()
    {
        return currentMonsterWeight;
    }
    public void RegisterEnemy(Enemy enemy)
    { 
        currentEnemyList.Add(enemy);
        currentMonsterWeight += enemy.GetSpawnWeight();
    }
    public void UnregisterEnemy(Enemy enemy)
    {
        currentMonsterWeight -= enemy.GetSpawnWeight();
        currentEnemyList.Remove(enemy);
    }
    // =====================================

    // ============ PROJECTILE =============
    [SerializeField] private List<Projectile> currentProjectileList = new();
    public void RegisterProjectile(Projectile projectile)
    {
        currentProjectileList.Add(projectile);;
    }
    public void UnregisterProjectile(Projectile projectile)
    {
        currentProjectileList.Remove(projectile);
    }
    // =====================================

    // ============== XP ORB ===============
    [SerializeField] private List<XPPickup> currentXPOrbList = new();
    public List<XPPickup> GetCurrentXPOrbList() {
        return currentXPOrbList;
    }
    public void RegisterXPOrb(XPPickup xpOrb)
    {
        currentXPOrbList.Add(xpOrb);
    }
    public void UnregisterXPOrb(XPPickup xpOrb)
    {
        currentXPOrbList.Remove(xpOrb);
    }
    // =====================================

    // ============ PICKUPABLE =============
    [SerializeField] private List<Pickupable> currentPickupableList = new();
    public void RegisterPickupable(Pickupable pickupable)
    {
        currentPickupableList.Add(pickupable);
    }
    public void UnregisterPickupable(Pickupable pickupable)
    {
        currentPickupableList.Remove(pickupable);
    }
    // =====================================



    private void Awake()
    {
        // enemySpawner.SetEntityManager(this);
        // projectileSpawner.SetEntityManager(this);
        // xpOrbSpawner.SetEntityManager(this);
        // pickupableSpawner.SetEntityManager(this);
    }


    public bool ReachMobCap()
    {
        if (currentMonsterWeight >= MAX_MONSTER_WEIGHT)
        {
            return true;
        }

        return false;
    }
}
