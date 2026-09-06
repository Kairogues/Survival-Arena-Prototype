using System;
using UnityEngine;

public class LifeComponent : MonoBehaviour
{
    public event Action Died;
    public event Action<float, float, float> HealthChanged;
    [SerializeField] private AudioClip hurtSoundFX;
    [SerializeField] private StatComponent statComponent;
    private Stat healthStat;



    private void Start()
    {
        healthStat = statComponent.GetStat(StatType.HEALTH);
        healthStat.MaximizeCurrentStat();
    }


    public void Heal(float amount)
    {
        float oldHealth = healthStat.GetCurrentValue();
        float newHealth = healthStat.GetCurrentValue() + amount;
        healthStat.UpdateStat(newHealth);
        if (newHealth > healthStat.GetMaxValue()) newHealth = healthStat.GetMaxValue();
        HealthChanged?.Invoke(oldHealth, newHealth, healthStat.GetMaxValue());
    }


    public void Damage(float amount)
    {
        ApplicationManager.Instance.audioManager.PlaySoundFX(hurtSoundFX, transform, 1f);
        float oldHealth = healthStat.GetCurrentValue();
        float newHealth = healthStat.GetCurrentValue() - amount;
        
        if (newHealth <= 0)
        {
            healthStat.UpdateStat(0);
            Die();
        }

        healthStat.UpdateStat(newHealth);

        if (newHealth > healthStat.GetMaxValue()) newHealth = healthStat.GetMaxValue();
        HealthChanged?.Invoke(oldHealth, newHealth, healthStat.GetMaxValue());
    }


    public void SubscribeToHealthChanged(Action<float, float, float> listener) 
    {
        statComponent.SubscribeToStat(StatType.HEALTH, listener);
    }


    private void Die()
    {
        Died?.Invoke();
    }
}
