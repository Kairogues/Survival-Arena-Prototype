using UnityEngine;

public class XPPickup : Pickupable
{
    [SerializeField] private int amount;


    public override void OnSpawn()
    {
        GameManager.Instance.entityManager.RegisterXPOrb(this);
    }


    public override void OnDespawn()
    {
        GameManager.Instance.entityManager.UnregisterXPOrb(this);
    }

    
    public override void OnDrop()
    {
        base.OnDrop();
        pickupCollider.enabled = true;
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

        GameManager.Instance.playerManager.GainXP(amount);

        ReleaseToPool();
    }
}
