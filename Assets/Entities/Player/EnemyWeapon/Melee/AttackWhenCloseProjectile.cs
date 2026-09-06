using UnityEngine;

public class AttackWhenCloseProjectile : Projectile
{
    [SerializeField] private HitboxComponent hitboxComponent;
    [SerializeField] private StatComponent statComponent;



    public override void OnSpawn()
    {
        base.OnSpawn();
        statComponent.RefreshStatDictionary();
        hitboxComponent.ClearNextHitRecord();
    }


    protected override void ReleaseToPool()
    {
        base.ReleaseToPool();
    }


    private void SelfDestruct()
    {
        ReleaseToPool();
    }
}
