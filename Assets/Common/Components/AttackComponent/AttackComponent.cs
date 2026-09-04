using UnityEngine;
using System.Collections.Generic;

public class AttackComponent : MonoBehaviour
{
    [SerializeField] private Transform weaponMountPoint;
    [SerializeField] private List<Weapon> startingWeaponPrefabList;
    private List<Weapon> equippedWeaponList = new();
    public List<Weapon> GetEquippedWeaponList()
    {
        return equippedWeaponList;
    }
    private Vector2 movingDirection;
    public Vector2 GetMovingDirection()
    {
        return movingDirection;
    }
    public void SetMovingDirection(Vector2 newMovingDirection)
    {
        if (newMovingDirection == Vector2.zero)
        {
            return;
        }
        movingDirection = newMovingDirection.normalized;

        if (movingDirection.x > 0)
        {
            facingDirection = Vector2.right;
        } else if (movingDirection.x < 0)
        {
            facingDirection = Vector2.left;
        }
    }
    private Vector2 facingDirection;



    private void Awake()
    {
        if (weaponMountPoint == null) weaponMountPoint = transform;

        foreach (Weapon prefab in startingWeaponPrefabList)
        {
            if (prefab != null) EquipWeapon(prefab.gameObject);
        }
    }


    public Weapon EquipWeapon(GameObject weaponPrefab)
    {
        GameObject weaponObject = Instantiate(weaponPrefab, weaponMountPoint);
        weaponObject.transform.localPosition = Vector3.zero;
        weaponObject.transform.localRotation = Quaternion.identity;

        if (weaponObject.TryGetComponent(out Weapon weapon))
        {
            equippedWeaponList.Add(weapon);
            return weapon;
        }

        Destroy(weaponObject);
        return null;
    }

    public void UnequipWeapon(Weapon weapon)
    {
        if (equippedWeaponList.Remove(weapon))
        {
            Destroy(weapon.gameObject);
        }
    }

    public void AttackAll()
    {
        AttackContext context = CreateAttackContext();
        for (int i = 0; i < equippedWeaponList.Count; i++)
        {
            equippedWeaponList[i].Attack(context);
        }
    }

    public void LevelUpWeapon(int weaponIndex)
    {
        if (weaponIndex >= 0 && weaponIndex < equippedWeaponList.Count)
        {
            equippedWeaponList[weaponIndex].LevelUp();
        }
    }

    private AttackContext CreateAttackContext()
    {
        float angle = Mathf.Atan2(movingDirection.y, movingDirection.x) * Mathf.Rad2Deg;
        Quaternion projectileRotation = Quaternion.Euler(0f, 0f, angle); 

        return new AttackContext
        {
            position = transform.position,
            headingDirection = movingDirection,
            rotationFromHeadingDirection = projectileRotation,
            facingDirection = facingDirection,
            owner = gameObject
        };
    }
    // ================================================
    /*
    [SerializeReference] private List<WeaponData> weaponDataList;
    private List<Weapon> currentWeaponList = new();
    private Vector2 movingDirection;


    
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


    private AttackContext CreateAttackContext()
    {
        Vector2 facing = movingDirection.x != 0 ?
                (movingDirection.x > 0 ? Vector2.right : Vector2.left) 
                : (Vector2)transform.right;

        float angle = Mathf.Atan2(movingDirection.y, movingDirection.x) * Mathf.Rad2Deg;
        Quaternion projectileRotation = Quaternion.Euler(0f, 0f, angle); 

        return new AttackContext
        {
            position = transform.position,
            headingDirection = movingDirection,
            rotationFromHeadingDirection = projectileRotation,
            facingDirection = facing,
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
    */
}
