using UnityEngine;

public class Zombie : Enemy
{
    [SerializeField] private MovementComponent movementComponent;
    [SerializeField] private LifeComponent lifeComponent;
    [SerializeField] private AttackComponent attackComponent;
    [SerializeField] protected SpriteRenderer spriteRenderer;



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
        FlipSprite();
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


    protected void FlipSprite()
    {
        if (movementComponent.GetCurrentDirection().x < 0)
        {
            spriteRenderer.flipX = true;
        } else if (movementComponent.GetCurrentDirection().x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}
