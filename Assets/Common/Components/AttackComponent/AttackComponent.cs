using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class AttackComponent : MonoBehaviour
{
    [SerializeReference] private List<WeaponData> weaponDataList;
    private List<Weapon> currentWeaponList = new();
    private Vector2 movingDirection;
    public Vector2 GetFacingDirection()
    {
        return movingDirection;
    }
    public void UpdateFacingDirection(Vector2 newFacingDirection)
    {
        if (newFacingDirection == Vector2.zero)
        {
            return;
        }
        movingDirection = newFacingDirection.normalized;
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
        float angle = Mathf.Atan2(movingDirection.y, movingDirection.x) * Mathf.Rad2Deg;

        Quaternion projectileRotation = Quaternion.Euler(0f, 0f, angle); 

        return projectileRotation;
    }


    private AttackContext CreateAttackContext()
    {
        Vector2 facing = movingDirection.x != 0 ?
                (movingDirection.x > 0 ? Vector2.right : Vector2.left) 
                : (Vector2)transform.right;

        return new AttackContext
        {
            position = transform.position,
            rotation = RotationFromDirection(),
            direction = facing,
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
