using UnityEngine;

[System.Serializable]
public class Weapon
{
    [SerializeField] private WeaponData weaponData;
    // [SerializeField] private bool canAttack = true;
    [SerializeField] protected float nextCanAttackTime;



    public Weapon(WeaponData data)
    {
        weaponData = data;
    }


    public virtual bool CanAttack()
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

        weaponData.TriggerWeaponBehavior(context);
        nextCanAttackTime = Time.time + weaponData.cooldown;
    }
}
