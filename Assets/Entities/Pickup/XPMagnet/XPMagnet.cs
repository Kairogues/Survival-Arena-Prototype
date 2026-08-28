using System.Collections.Generic;
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
        pickupCollider.enabled = true;
    }


    public override void OnPickup()
    {
        base.OnPickup();
        pickupCollider.enabled = false;
        // Play pickup animation
        // Play SFX
    }


    public override void ProcessPickup(PickUpItemComponent actor)
    {
        OnPickup();

        List<XPPickup> currentXPOrbList = GameManager.Instance.entityManager.GetCurrentXPOrbList();
        foreach (XPPickup xpOrb in currentXPOrbList)
        {
            xpOrb.AttractToActor(actor);
        }

        ReleaseToPool();
    }
}
