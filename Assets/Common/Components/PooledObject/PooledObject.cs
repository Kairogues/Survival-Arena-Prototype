using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// This class is the composition way of enabling OnSpawn and OnDespawn in the object pool pattern instead of inheritence
/// </summary>
[DisallowMultipleComponent]
public class PooledObject : MonoBehaviour
{
    private IObjectPool<GameObject> originPool;
    public IObjectPool<GameObject> GetOriginPool()
    {
        return originPool;
    }
    public void SetOriginPool(IObjectPool<GameObject> pool)
    {
        originPool = pool;
    }

    private IPoolable[] poolablesInChildren;



    private void Awake()
    {
        poolablesInChildren = GetComponentsInChildren<IPoolable>(true);
    }


    public void TriggerSpawn()
    {
        for (int i = 0; i < poolablesInChildren.Length; i++)
        {
            //Debug.Log("Triggering spawn from PooledObject");
            poolablesInChildren[i].OnSpawn();
        }
    }


    public void TriggerDespawn()
    {
        for (int i = 0; i < poolablesInChildren.Length; i++)
        {
            //Debug.Log("Triggering despawn from PooledObject");
            poolablesInChildren[i].OnDespawn();
        }
    }


    public void ReleaseToPool()
    {
        if (originPool != null)
        {
            //Debug.Log("BACK TO THE POOL");
            originPool.Release(gameObject);
        }
        else
        {
            Debug.Log("No pool, destroy");
            TriggerDespawn();
            Destroy(gameObject);
        }
    }
}