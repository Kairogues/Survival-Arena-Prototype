using UnityEngine;

public class XPMagnet : Pickupable
{

    public override void OnSpawn()
    {
        base.OnSpawn();
    }


    public override void OnDespawn()
    {
        base.OnDespawn();
    }

    
    public override void OnDrop()
    {
        base.OnDrop();
    }


    public override void OnPickup()
    {
        pickupCollider.enabled = false;
        // Play pickup animation
        // Play SFX
    }


    public override void ProcessPickup(PickUpItemComponent actor)
    {
        OnPickup();

        GameManager.Instance.entityManager.CollectAllXPOrb(actor);

        ReleaseToPool();
    }
}
