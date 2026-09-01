using UnityEngine;

public struct AttackContext
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector2 direction;
    public GameObject owner;
}

public abstract class WeaponBehavior : ScriptableObject
{
    public abstract void Attack(AttackContext attackContext);
}
