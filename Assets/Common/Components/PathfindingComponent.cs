using System;
using UnityEngine;
using UnityEngine.AI;

// I only implement "Run straight to player" AI so this is enough
public class PathfindingComponent : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    
    [SerializeField] private StatComponent statComponent;
    
    private Stat movementStat;
    private float currentSpeed = 0.0f;
    public void SetSpeed(float newSpeed)
    {
        currentSpeed = newSpeed;
    }
    private Vector2 currentDirection;

    public Vector2 GetCurrentDirection()
    {
        return currentDirection.normalized;
    }



    private void Start()
    {
        agent.updateRotation = false;
		agent.updateUpAxis = false;
        
        //agent.updatePosition = false;

        movementStat = statComponent.GetStat(StatType.MOVEMENT_SPEED);
        currentSpeed = movementStat.GetCurrentValue();
        SubscribeToMovementSpeedChanged(UpdateSpeedAfterChanged);
    }


    private void Update()
    {
        agent.speed = currentSpeed;
        agent.SetDestination(GameManager.Instance.playerManager.currentPlayer.transform.position);
    
        Vector3 nextPosition = agent.steeringTarget;
        Vector2 direction = (nextPosition - transform.position).normalized;

        currentDirection = direction;
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
