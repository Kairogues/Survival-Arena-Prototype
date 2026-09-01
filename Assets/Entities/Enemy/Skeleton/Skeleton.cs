using UnityEngine;

public class Skeleton : Enemy
{
    [SerializeField] private MovementComponent movementComponent;
    [SerializeField] private LifeComponent lifeComponent;



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
