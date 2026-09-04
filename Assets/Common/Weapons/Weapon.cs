using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private string weaponName;
    public string GetWeaponName()
    {
        return weaponName;
    }
    [SerializeField] protected float baseCooldown = 2.0f;
    public float GetBaseCooldown()
    {
        return baseCooldown;
    }
    private float nextCanAttackTime;
    private int currentWeaponLevel = 1;
    public int GetCurrentWeaponLevel()
    {
        return currentWeaponLevel;
    }
    protected Action<AttackContext> onAttackAction;



    protected virtual void Awake()
    {
        // Initialize the weapon at level 1
        ConfigureForLevel(GetCurrentWeaponLevel());
    }


    protected bool CanAttack()
    {
        if (Time.time <= nextCanAttackTime)
        {
            return false;
        }

        return true;
    }


    public void Attack(AttackContext context)
    {
        if (!CanAttack()) 
        {
            return;
        }

        onAttackAction?.Invoke(context);

        nextCanAttackTime = Time.time + baseCooldown;
    }


    public void LevelUp()
    {
        currentWeaponLevel++;
        ConfigureForLevel(GetCurrentWeaponLevel());
    }

    
    protected abstract void ConfigureForLevel(int level);
}