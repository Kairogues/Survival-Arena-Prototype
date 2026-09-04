using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{
    [SerializeField] private float SECONDS_PER_DAMAGE_TICK = 0.25f;
    public event Action<HurtboxComponent> HitHurtbox;
    public event Action<HurtboxComponent> HurtboxExit;
    public event Action HitObstacle;
    private Dictionary<HurtboxComponent, float> nextHitTimes = new();
    [SerializeField] private StatComponent statComponent;



    private void OnTriggerStay2D(Collider2D hurtboxInfo)
    {
        int obstacleLayer = LayerMask.NameToLayer("Obstacle");
        if (hurtboxInfo.gameObject.layer == obstacleLayer)
        {
            RegisterObstacleHit();
            return;
        }

        if (!hurtboxInfo.TryGetComponent(out HurtboxComponent hurtbox)) 
        {
            return;
        }

        if (nextHitTimes.TryGetValue(hurtbox, out float nextValidTime))
        {
            if (Time.time < nextValidTime) 
            {
                return;
            }
        }

        hurtbox.TakeDamge(GetDamageAmount());
        nextHitTimes[hurtbox] = Time.time + SECONDS_PER_DAMAGE_TICK;

        RegisterHurtboxHit(hurtbox);
    }

    private void OnTriggerExit2D(Collider2D hurtboxInfo)
    {
        if (hurtboxInfo.TryGetComponent(out HurtboxComponent hurtbox))
        {
            nextHitTimes.Remove(hurtbox);
        }
    }


    public float GetDamageAmount()
    {
        return statComponent.GetStat(StatType.ATTACK).GetCurrentValue();
    }


    public void RegisterHurtboxHit(HurtboxComponent hurtbox)
    {
        HitHurtbox?.Invoke(hurtbox);
    }


    public void RegisterObstacleHit()
    {
        HitObstacle?.Invoke();
    }
}

