using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IPoolable
{
    [SerializeField] protected PooledObject pooledObjectComponent;
    [SerializeField] private int spawnWeight;
    [SerializeField] protected StatComponent statComponent;
    public int GetSpawnWeight()
    {
        return spawnWeight;
    }
    public void SetSpawnWeight(int newSpawnWeight)
    {
        spawnWeight = newSpawnWeight;
    }

    

    // IPoolable
    public virtual void OnSpawn()
    {
        statComponent.RefreshStatDictionary();
        GameManager.Instance.entityManager.RegisterEnemy(this);
    }


    public virtual void OnDespawn()
    {
        GameManager.Instance.entityManager.UnregisterEnemy(this);
    }


    protected virtual void Die()
    {
        pooledObjectComponent.ReleaseToPool();
    }
}
