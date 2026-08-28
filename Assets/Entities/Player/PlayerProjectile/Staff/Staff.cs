using System;
using UnityEngine;

public class Staff : Projectile
{
    private const float TIME_ALIVE = 2.0f;
    [SerializeField] private HitboxComponent hitboxComponent;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private MovementComponent movementComponent;
    private float timeAlive = 3.0f;



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
        SelfDestruct();
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
