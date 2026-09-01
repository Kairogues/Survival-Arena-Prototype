using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class AttackComponent : MonoBehaviour
{
    [SerializeReference] private List<WeaponData> weaponDataList;
    [SerializeReference] private List<Weapon> currentWeaponList;
    private Vector2 facingDirection;
    public Vector2 GetFacingDirection()
    {
        return facingDirection;
    }
    public void UpdateFacingDirection(Vector2 newFacingDirection)
    {
        if (newFacingDirection == Vector2.zero)
        {
            return;
        }
        facingDirection = newFacingDirection.normalized;
    }


    
    public void Awake()
    {
        foreach (WeaponData weaponData in weaponDataList)
        {
            AddWeapon(new Weapon(weaponData));
        }
    }


    public void AddWeapon(Weapon newWeapon)
    {
        currentWeaponList.Add(newWeapon);
    }


    public void RemoveWeapon(Weapon newWeapon)
    {
        currentWeaponList.Remove(newWeapon);
    }


    private Quaternion RotationFromDirection()
    {
        float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;

        Quaternion projectileRotation = Quaternion.Euler(0f, 0f, angle); 

        return projectileRotation;
    }


    private AttackContext CreateAttackContext()
    {
        return new AttackContext
        {
            position = transform.position,
            rotation = RotationFromDirection(),
            direction = facingDirection,
            owner = gameObject
        };
    }


    public void AutoAttackAll()
    {
        foreach (Weapon weapon in currentWeaponList)
        {
            weapon.Attack(CreateAttackContext());
        }
    }


    public void AttackSingleWeapon()
    {
        currentWeaponList[0].Attack(CreateAttackContext());
    }
}
