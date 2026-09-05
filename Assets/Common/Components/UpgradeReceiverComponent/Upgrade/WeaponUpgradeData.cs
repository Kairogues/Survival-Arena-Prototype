using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Weapon Upgrade")]
public class WeaponUpgradeData : UpgradeData
{
    [SerializeField] private GameObject weaponPrefab;

    public override void Apply(GameObject target, int targetLevel)
    {
        if (!target.TryGetComponent(out AttackComponent attackComponent)) return;

        // Check if the weapon is already equipped
        Weapon existingWeapon = null;
        foreach (Weapon weapon in attackComponent.GetEquippedWeaponList())
        {
            if (weapon.GetWeaponName() == weaponPrefab.GetComponent<Weapon>().GetWeaponName())
            {
                existingWeapon = weapon;
                break;
            }
        }

        if (existingWeapon == null)
        {
            attackComponent.EquipWeapon(weaponPrefab);
        }
        else
        {
            existingWeapon.LevelUp();
        }
    }
}