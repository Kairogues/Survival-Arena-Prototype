using UnityEngine;

public class Pickupable : MonoBehaviour, IPoolable
{
    [SerializeField] protected PooledObject pooledObjectComponent;
    protected Collider2D pickupCollider;



    private void Awake()
    {
        pickupCollider = GetComponent<Collider2D>();
    }


    public virtual void OnSpawn()
    {
        GameManager.Instance.entityManager.RegisterPickupable(this);
        OnDrop();
    }


    public virtual void OnDespawn()
    {
        GameManager.Instance.entityManager.UnregisterPickupable(this);
    }


    protected virtual void ReleaseToPool()
    {
        pooledObjectComponent.ReleaseToPool();
    }


    public virtual void OnDrop()
    {
        
    }


    public virtual void OnPickup()
    {
        
    }


    public virtual void ProcessPickup(PickUpItemComponent actor)
    {
        
    }
}
