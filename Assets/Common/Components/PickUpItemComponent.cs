using System;
using UnityEngine;

public class PickUpItemComponent : MonoBehaviour
{
    public event Action<Pickupable> PickedUpItem;
    [SerializeField] private AudioClip pickupFX;



    private void OnTriggerEnter2D(Collider2D pickup)
    {
        Pickupable pickupable = pickup.GetComponent<Pickupable>();

        if (pickupable != null)
        {
            ApplicationManager.Instance.audioManager.PlaySoundFX(pickupFX, transform, 1f);
            PickedUpItem?.Invoke(pickupable);
            pickupable.ProcessPickup(this);
        }
    }
}
