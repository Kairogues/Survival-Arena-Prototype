using UnityEngine;

public struct AttackContext
{
    public Vector3 position;
    public Vector3 headingDirection;
    public Quaternion rotationFromHeadingDirection;
    public Vector2 facingDirection;
    public GameObject owner;
}

public abstract class WeaponBehavior : ScriptableObject
{
    public abstract void Attack(AttackContext attackContext);
}
