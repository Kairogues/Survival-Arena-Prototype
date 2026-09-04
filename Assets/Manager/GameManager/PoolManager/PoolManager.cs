using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// A Manager responsible for GameObject pooling.
/// Manages instantiation, lifecycle dispatch (spawn/despawn), and memory cleanup
/// </summary>
public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    [SerializeField] private int defaultCapacity = 40;
    [SerializeField] private int maxPoolSize = 400;

    // Map the object to its pool
    // Each entry is a prefab
    private Dictionary<GameObject, IObjectPool<GameObject>> pools = new();

    // Map the object to its PooledObject component, so it will not have to GetComponent() the PooledObject everytime
    // Each entry is an instance
    private Dictionary<GameObject, PooledObject> instanceMap = new();



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }


    #region Spawn API
    public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (prefab == null) 
        {
            return null;
        }

        IObjectPool<GameObject> pool = GetOrCreatePool(prefab);
        GameObject instance = pool.Get();

        /*
        if (instance.TryGetComponent(out PooledObject pooledObj))
        {
            pooledObj.SetOriginPool(pool);
        }
        */

        instance.transform.SetPositionAndRotation(position, rotation);
        if (parent != null)
        {
            instance.transform.SetParent(parent);
        }

        if (instanceMap.TryGetValue(instance, out PooledObject pooledObj))
        {
            pooledObj.TriggerSpawn();
        }

        return instance;
    }
    #endregion


    #region Release API
    public void Release(GameObject instance)
    {
        if (instance == null) return;

        if (instanceMap.TryGetValue(instance, out PooledObject pooledObj))
        {
            pooledObj.ReleaseToPool();
        }
        else
        {
            Destroy(instance);
        }

        //Debug.Log("BACK TO THE POOL");
    }
    #endregion


    #region Internal Pool Factory
    private IObjectPool<GameObject> GetOrCreatePool(GameObject prefab)
    {
        if (pools.TryGetValue(prefab, out IObjectPool<GameObject> existingPool))
        {
            return existingPool;
        }

        IObjectPool<GameObject> newPool = null;

        newPool = new ObjectPool<GameObject>(
            createFunc: () =>
            {
                GameObject instance = Instantiate(prefab);

                if (instance.TryGetComponent(out PooledObject pooledObj))
                {
                    pooledObj.SetOriginPool(newPool);
                }

                instanceMap[instance] = pooledObj;

                //Debug.Log("Created " + instance.name);

                return instance;
            },
            OnGetFromPool,
            OnReleaseToPool,
            OnDestroyPoolObject,
            collectionCheck: true,
            defaultCapacity: defaultCapacity,
            maxSize: maxPoolSize
        );

        pools.Add(prefab, newPool);
        return newPool;
    }


    private void OnGetFromPool(GameObject pooledObject)
    {
        pooledObject.SetActive(true);

        /*
        if (instanceMap.TryGetValue(pooledObject, out PooledObject pooledObj))
        {
            pooledObj.TriggerSpawn();
        }
        */

        //Debug.Log("Get " + pooledObject.name + " from pool");
    }


    private void OnReleaseToPool(GameObject pooledObject)
    {
        if (instanceMap.TryGetValue(pooledObject, out PooledObject pooledObj))
        {
            pooledObj.TriggerDespawn();
        }

        pooledObject.SetActive(false);

        //Debug.Log("Release " + pooledObject.name + " to pool");
    }


    private void OnDestroyPoolObject(GameObject pooledObject)
    {
        instanceMap.Remove(pooledObject);
        Destroy(pooledObject);

        //Debug.Log("Destroy " + pooledObject.name);
    }
    #endregion


    #region Cleanup

    public void TryRemoveInactivePools()
    {
        HashSet<IObjectPool<GameObject>> activePools = new();

        // Collects all active pools
        foreach (var kvp in instanceMap)
        {
            GameObject instance = kvp.Key;
            PooledObject pooledObj = kvp.Value;

            if (instance != null && instance.activeInHierarchy && pooledObj != null)
            {
                IObjectPool<GameObject> origin = pooledObj.GetOriginPool();
                if (origin != null)
                {
                    activePools.Add(origin);
                }
            }
        }

        // Find the keys (prefabs) that coresponds to inactive pools
        List<GameObject> prefabsToRemove = new();
        List<IObjectPool<GameObject>> poolsToClear = new();

        foreach (var kvp in pools)
        {
            GameObject prefab = kvp.Key;
            IObjectPool<GameObject> pool = kvp.Value;

            if (!activePools.Contains(pool))
            {
                prefabsToRemove.Add(prefab);
                poolsToClear.Add(pool);
            }
        }

        // Collects all instances that belong to inactive pools
        List<GameObject> deadInstancesToUnmap = new();
        foreach (var kvp in instanceMap)
        {
            if (kvp.Value == null || poolsToClear.Contains(kvp.Value.GetOriginPool()))
            {
                deadInstancesToUnmap.Add(kvp.Key);
            }
        }

        // Clear all instance map entries that have instances belonging to inactive pools
        foreach (GameObject deadInstance in deadInstancesToUnmap)
        {
            instanceMap.Remove(deadInstance);
        }

        // Dispose and remove the inactive pools
        foreach (IObjectPool<GameObject> pool in poolsToClear)
        {
            pool.Clear(); // Triggers OnDestroyPoolObject on inactive pooled objects
        }

        foreach (GameObject prefab in prefabsToRemove)
        {
            pools.Remove(prefab);
        }
    }


    public void ClearUnusedPools(List<EnemySpawnEntry> currentEnemyPool)
    {
        foreach (EnemySpawnEntry weightedEnemy in currentEnemyPool)
        {
            ClearUnusedPool(weightedEnemy.prefab.gameObject);
        }
    }


    private void ClearUnusedPool(GameObject prefab)
    {
        
        if (prefab == null || !pools.TryGetValue(prefab, out IObjectPool<GameObject> pool))
        {
            return;
        }

        pool.Clear();

        pools.Remove(prefab);

        List<GameObject> instanceToUnmap = new();
        foreach (KeyValuePair<GameObject, PooledObject> kvp in instanceMap)
        {
            if (kvp.Value != null && kvp.Value.GetOriginPool() == pool)
            {
                kvp.Value.SetOriginPool(null);
                instanceToUnmap.Add(kvp.Key);
                
            }
        }

        foreach (GameObject instance in instanceToUnmap)
        {
            instanceMap.Remove(instance);
        }
    }

    private void Update()
    {
        // Debug.Log("CURRENT ENEMY IN POOL " + instanceMap.Count);
    }
    #endregion
}