using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/Weapon/WeaponData")]
public class WeaponData : ScriptableObject
{
    [SerializeField] public string weaponName;
    [SerializeField] public float cooldown;
    [SerializeField] private WeaponBehavior weaponBehavior;

    public void TriggerWeaponBehavior(AttackContext context)
    {
        weaponBehavior.Attack(context);
    }
}
