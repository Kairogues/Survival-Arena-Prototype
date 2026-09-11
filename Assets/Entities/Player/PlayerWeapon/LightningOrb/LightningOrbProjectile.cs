using UnityEngine;
using System.Collections.Generic;

public class LightningOrbProjectile : Projectile
{
    private const float ORBIT_RADIUS = 2.5f;
    private const float SPEED_MULTIPLIER = 10.0f;
    [SerializeField] public Collider2D collision;
    [SerializeField] private HitboxComponent hitboxComponent;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private MovementComponent movementComponent;
    [SerializeField] private StatComponent statComponent;
    private Transform playerTransform;
    private static List<LightningOrbProjectile> ActiveOrbs = new List<LightningOrbProjectile>();
    public static List<LightningOrbProjectile> GetCurrentActiveOrbs()
    {
        return ActiveOrbs;
    }



    private void Awake()
    {
        movementComponent.SetBody(body);
        playerTransform = GameManager.Instance.playerManager.currentPlayer.transform;
    }

    /*
    private void Update()
    {
        if (playerTransform == null)
        {
            return;
        }

        Vector2 targetPosition = CalculateTargetOrbitPosition();
        Vector2 toTarget = targetPosition - (Vector2)transform.position;
        float distance = toTarget.magnitude;

        if (distance > 0.05f)
        {
            Vector2 steerDirection = Vector2.ClampMagnitude(toTarget, 1.0f);
            movementComponent.UpdateDirection(steerDirection);
        }
        else
        {
            movementComponent.UpdateDirection(Vector2.zero);
        }
    }
    */

    private void FixedUpdate()
    {
        Vector2 targetPosition = CalculateTargetOrbitPosition();

        body.MovePosition(targetPosition);
    }


    public override void OnSpawn()
    {
        base.OnSpawn();
        statComponent.RefreshStatDictionary();

        if (!ActiveOrbs.Contains(this))
        {
            ActiveOrbs.Add(this);
        }

        Vector2 targetPos = CalculateTargetOrbitPosition();
        transform.position = targetPos;
        body.position = targetPos;
        hitboxComponent.ClearNextHitRecord();
    }


    protected override void ReleaseToPool()
    {
        ActiveOrbs.Remove(this);
        base.ReleaseToPool();
    }


    private Vector2 CalculateTargetOrbitPosition()
    {
        if (ActiveOrbs.Count == 0 || playerTransform == null) 
        {
            return transform.position;
        }

        if (ActiveOrbs.IndexOf(this) == -1) 
        {
            return transform.position;
        }

        float baseAngle = Time.time * movementComponent.GetSpeed() * SPEED_MULTIPLIER;
        float angleStep = 360f / ActiveOrbs.Count;
        float thisAngleDeg = baseAngle + (ActiveOrbs.IndexOf(this) * angleStep);

        float angleRad = thisAngleDeg * Mathf.Deg2Rad;
        Vector2 offset = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)) * ORBIT_RADIUS;

        return (Vector2)playerTransform.position + offset;
    }


    public static void ResetOrb()
    {
        for (int i = ActiveOrbs.Count - 1; i >= 0; i--)
        {
            ActiveOrbs[i].SelfDestruct();
        }
    }


    public void AddStatBuff(StatBuff statBuff)
    {
        statComponent.AddBuff(statBuff);
    } 


    private void SelfDestruct()
    {
        ReleaseToPool();
    }
}
