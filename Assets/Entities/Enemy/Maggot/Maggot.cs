using UnityEngine;

public class Maggot : Enemy
{
    [SerializeField] private MovementComponent movementComponent;
    [SerializeField] private LifeComponent lifeComponent;
    [SerializeField] private AttackComponent attackComponent;



    private void OnEnable()
    {
        lifeComponent.Died += Die;
    }


    private void OnDisable()
    {
        lifeComponent.Died -= Die;
    }



    void Update()
    {
        attackComponent.AttackAll();
    }


    public override void OnSpawn()
    {
        base.OnSpawn();
    }


    public override void OnDespawn()
    {
        base.OnDespawn();
    }


    protected override void Die()
    {
        base.Die();
    }
}
