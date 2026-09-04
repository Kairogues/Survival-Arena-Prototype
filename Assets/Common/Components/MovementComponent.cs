using System;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    public void SetBody(Rigidbody2D body)
    {
        this.body = body;
    }
    
    [SerializeField] private StatComponent statComponent;
    private Stat movementStat;
    private Vector2 currentDirection;
    public void UpdateDirection(Vector2 newDirection)
    {
        currentDirection = newDirection;
    }
    
    private float currentSpeed = 0.0f;
    public float GetSpeed()
    {
        return currentSpeed;
    }
    public void SetSpeed(float newSpeed)
    {
        currentSpeed = newSpeed;
    }



    private void Start()
    {
        movementStat = statComponent.GetStat(StatType.MOVEMENT_SPEED);
        currentSpeed = movementStat.GetCurrentValue();
        SubscribeToMovementSpeedChanged(UpdateSpeedAfterChanged);
    }


    private void FixedUpdate()
    {
        body.linearVelocity = currentDirection * currentSpeed;
    }


    private void UpdateSpeedAfterChanged(float oldValue, float currentValue, float maxValue)
    {
        SetSpeed(currentValue);
    }


    private void SubscribeToMovementSpeedChanged(Action<float, float, float> listener) 
    {
        statComponent.SubscribeToStat(StatType.MOVEMENT_SPEED, listener);
    }
}
