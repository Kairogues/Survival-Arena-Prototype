using System;
using UnityEngine;

public class StaffProjectile : Projectile
{
    private float baseTimeAlive = 2.0f;
    [SerializeField] private HitboxComponent hitboxComponent;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private MovementComponent movementComponent;
    [SerializeField] private StatComponent statComponent;
    private float timeAlive = 3.0f;
    private int piercingCount = 1;
    public void SetPiercingCount(int newPiercingCount)
    {
        piercingCount = newPiercingCount;
    }



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
        timeAlive = baseTimeAlive;
        movementComponent.UpdateDirection(transform.right);
    }


    protected override void ReleaseToPool()
    {
        base.ReleaseToPool();
    }


    private void ProcessHitHurtbox(HurtboxComponent hurtboxComponent)
    {
        piercingCount--;

        if (piercingCount == 0)
        {
            SelfDestruct();
        }

    }


    private void ProcessHitObstacle()
    {
        SelfDestruct();
    }


    private void SelfDestruct()
    {
        ReleaseToPool();
    }


    public void AddStatBuff(StatBuff statBuff)
    {
        statComponent.AddBuff(statBuff);
    }
}
