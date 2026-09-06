using System;
using UnityEngine;

public class LifeComponent : MonoBehaviour
{
    public event Action Died;
    [SerializeField] private StatComponent statComponent;
    private Stat healthStat;



    private void Start()
    {
        healthStat = statComponent.GetStat(StatType.HEALTH);
        healthStat.MaximizeCurrentStat();
    }


    public void Heal(float amount)
    {
        float newHealth = healthStat.GetCurrentValue() + amount;
        healthStat.UpdateStat(newHealth);
    }


    public void Damage(float amount)
    {
        float newHealth = healthStat.GetCurrentValue() - amount;
        
        if (newHealth <= 0)
        {
            healthStat.UpdateStat(0);
            Die();
        }

        healthStat.UpdateStat(newHealth);
        Debug.Log(name + " took " + amount + " damage");
    }


    private void SubscribeToHealthChanged(Action<float, float, float> listener) 
    {
        statComponent.SubscribeToStat(StatType.HEALTH, listener);
    }


    private void Die()
    {
        Died?.Invoke();
    }
}
