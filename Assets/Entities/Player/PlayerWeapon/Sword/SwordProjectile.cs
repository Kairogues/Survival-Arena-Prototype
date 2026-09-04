using System;
using UnityEngine;

public class Sword : Projectile
{
    private const float TIME_ALIVE = 0.1f;
    [SerializeField] private HitboxComponent hitboxComponent;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private MovementComponent movementComponent;
    private float timeAlive = 0.1f;



    private void Awake()
    {
        movementComponent.SetBody(body);
    }


    private void OnEnable()
    {
        hitboxComponent.HitHurtbox += ProcessHitHurtbox;
        hitboxComponent.HitObstacle += ProcessHitObstacle;
    }


    private void OnDisable()
    {
        hitboxComponent.HitHurtbox -= ProcessHitHurtbox;
        hitboxComponent.HitObstacle -= ProcessHitObstacle;
    }


    private void Update()
    {
        timeAlive -= Time.deltaTime;
        if (timeAlive <= 0.0f)
        {
            SelfDestruct();
        }
    }


    public override void OnSpawn()
    {
        base.OnSpawn();
        timeAlive = TIME_ALIVE;
        movementComponent.UpdateDirection(transform.right);
    }


    protected override void ReleaseToPool()
    {
        base.ReleaseToPool();
    }


    private void ProcessHitHurtbox(HurtboxComponent hurtboxComponent)
    {
        // SelfDestruct();
    }


    private void ProcessHitObstacle()
    {
        SelfDestruct();
    }


    private void SelfDestruct()
    {
        ReleaseToPool();
    }
}
