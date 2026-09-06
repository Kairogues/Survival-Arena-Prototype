using System;
using UnityEngine;

/// <summary>
///  PlayerManager is used to track everything about the player such as the player's alive state, position,...
/// </summary>
public class PlayerManager : MonoBehaviour
{
    public event Action PlayerDied;
    [SerializeField] private PlayerXPManager playerXPManger;
    public PlayerXPManager GetPlayerXPManager()
    {
        return playerXPManger;
    }
    [SerializeField] private PlayerGoldManager playerGoldManager;
    public PlayerGoldManager GetPlayerGoldManager()
    {
        return playerGoldManager;
    }
    public Player currentPlayer { get; private set; }
    public LifeComponent currentPlayerLifeComponent { get; private set; }



    public void GainXP(int amount)
    {
        playerXPManger.AddXP(amount);    
    }


    public void RegisterPlayer(Player player, LifeComponent playerLifeComponent)
    {
        if (currentPlayer == null)
        {
            currentPlayer = player;
            Debug.Log("Player successfully registered to PlayerManager.");
        }
        else if (currentPlayer != player)
        {
            Debug.LogWarning("A player is already registered! Overwriting reference.");
            currentPlayer = player;
        }

        currentPlayerLifeComponent = playerLifeComponent;
        currentPlayerLifeComponent.Died += AnnouncePlayerDeath;
    }


    private void AnnouncePlayerDeath()
    {
        PlayerDied?.Invoke();
        currentPlayerLifeComponent.Died -= AnnouncePlayerDeath;
    }
}
