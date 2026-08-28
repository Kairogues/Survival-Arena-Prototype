using UnityEngine;

public class XPPickup : Pickupable
{
    [SerializeField] private int amount;
    private bool isAttracted = false;
    private Transform targetTransform;
    private float attractionSpeed = 15.0f;


    protected virtual void Update()
    {
        if (isAttracted == true && targetTransform != null)
        {
            transform.position = Vector3.MoveTowards(
                    transform.position, 
                    targetTransform.position, 
                    attractionSpeed * Time.deltaTime
            );
        }  
    }


    public override void OnSpawn()
    {
        base.OnSpawn();
        GameManager.Instance.entityManager.RegisterXPOrb(this);
    }


    public override void OnDespawn()
    {
        base.OnDespawn();
        GameManager.Instance.entityManager.UnregisterXPOrb(this);
    }

    
    public override void OnDrop()
    {
        base.OnDrop();
        pickupCollider.enabled = true;
        isAttracted = false;
    }


    public override void OnPickup()
    {
        base.OnPickup();
        pickupCollider.enabled = false;

        if (!isAttracted)
        {
            // Play pickup animation
        }

        // Play SFX
    }


    public override void ProcessPickup(PickUpItemComponent actor)
    {
        OnPickup();

        GameManager.Instance.playerManager.GainXP(amount);

        ReleaseToPool();
    }


    public void AttractToActor(PickUpItemComponent actor)
    {
        targetTransform = actor.transform;
        isAttracted = true;
    }
}
