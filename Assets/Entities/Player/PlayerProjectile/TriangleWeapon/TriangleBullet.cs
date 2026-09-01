using System;
using UnityEngine;

public class TriangleBullet : MonoBehaviour
{
    [SerializeField] private HitboxComponent hitboxComponent;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private MovementComponent movementComponent;
    private float timeFly = 0.2f;
    private float spinTime = 3.0f;
    private bool isSpinning = false;
    


    private void Awake()
    {
        movementComponent.SetBody(body);
        movementComponent.UpdateDirection(transform.right);
    }


    private void OnEnable()
    {
        hitboxComponent.HitHurtbox += ProcessHitHurtbox;
    }


    private void OnDisable()
    {
        hitboxComponent.HitHurtbox -= ProcessHitHurtbox;
    }


    private void Update()
    {
        timeFly -= Time.deltaTime;
        if (timeFly <= 0.0f)
        {
            SpinInPlace();
            spinTime -= Time.deltaTime;
            if (spinTime <= 0.0f)
            {
                SelfDestruct();
            }
        }

        if (isSpinning)
        {
            
        }
    }


    private void ProcessHitHurtbox(HurtboxComponent hurtboxComponent)
    {
        spinTime += timeFly;
        timeFly = 0.0f;
    }


    private void SpinInPlace()
    {
        movementComponent.UpdateDirection(Vector2.zero);
        isSpinning = true;
    }


    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
