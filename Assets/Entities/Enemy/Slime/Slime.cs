using UnityEngine;

public class Slime : Enemy
{
    [SerializeField] private LifeComponent lifeComponent;
    [SerializeField] private AttackComponent attackComponent;
    [SerializeField] private PathfindingComponent pathfindingComponent;



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
