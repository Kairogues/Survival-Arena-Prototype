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
        float newHealth = oldHealth + amount;
        newHealth = Mathf.Min(newHealth, healthStat.GetMaxValue());

        healthStat.UpdateStat(newHealth);

        HealthChanged?.Invoke(oldHealth, newHealth, healthStat.GetMaxValue());
    }


    public void Damage(float amount)
    {
        ApplicationManager.Instance.audioManager.PlaySoundFX(hurtSoundFX, transform, 1f);

        float oldHealth = healthStat.GetCurrentValue();
        float newHealth = oldHealth - amount;
        newHealth = Mathf.Max(newHealth, 0f);

        healthStat.UpdateStat(newHealth);

        HealthChanged?.Invoke(oldHealth, newHealth, healthStat.GetMaxValue());

        if (newHealth <= 0)
        {
            Die();
        }
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
