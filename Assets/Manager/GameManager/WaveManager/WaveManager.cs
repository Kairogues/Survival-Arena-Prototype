using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// WaveManager is used to track the progression of the level
/// Manages waves progression, enemy pools, wave spawning
/// Issues enemies from the ObjectPool and hand them to EntityManager
/// </summary>
public class WaveManager : MonoBehaviour
{
    private const float MINIMUM_SPAWN_DISTANCE = 15.0F;
    private const float MAXIMUM_SPAWN_DISTANCE = 25.0F;
    public event Action<WaveData> WaveStarted;
    public event Action<WaveData> WaveEnded;
    public event Action<int> WaveCountdowned;
    [SerializeField] private List<WaveData> waveDatas;
    private WaveData currentWave;
    private int currentWaveIndex = -1;
    private float currentSpawnCountdown = 0.0f;
    private float currentWaveCountdown = 0.0f;



    public void StartGame()
    {
        StartNewWave();
    }


    public void ProgressWave()
    {
        SpawnIntervalCountdown(Time.deltaTime);
        WaveCountdown(Time.deltaTime);

        SpawnUntilReachMinimumWeight();
    }


    private void StartNewWave()
    {
        if ((currentWaveIndex + 1) != waveDatas.Count) {
            currentWaveIndex += 1;
        }

        currentWave = waveDatas[currentWaveIndex];

        currentSpawnCountdown = currentWave.spawnInterval;
        currentWaveCountdown = currentWave.waveDuration;

        Debug.Log("Start wave " + (currentWaveIndex + 1));

        WaveStarted?.Invoke(currentWave);
    }


    private void EndCurrentWave()
    {
        WaveEnded?.Invoke(currentWave);
    }


    private void SpawnIntervalCountdown(float delta)
    {
        currentSpawnCountdown -= delta;

        if (currentSpawnCountdown <= 0.0f)
        {
            AttemptSpawnEnemy();
            currentSpawnCountdown = currentWave.spawnInterval;
        }
    }


    private void WaveCountdown(float delta)
    {
        currentWaveCountdown -= delta;

        if (currentWaveCountdown <= 0.0f)
        {
            EndCurrentWave();
            StartNewWave();
        }
    }


    private EnemySpawnEntry PickMonster()
    {
        int random = UnityEngine.Random.Range(0, 100);
        float accumulation = 0;
        
        foreach (EnemySpawnEntry enemy in currentWave.enemyPool)
        {
            if (random < (accumulation + enemy.spawnChance))
            {
                return enemy;
            }

            accumulation += enemy.spawnChance;
        }

        return currentWave.enemyPool[0];
    }


    private Vector3 PickSpawnLocation()
    {
        float randomDistance = UnityEngine.Random.Range(MINIMUM_SPAWN_DISTANCE, MAXIMUM_SPAWN_DISTANCE);
        float randomAngle = UnityEngine.Random.Range(0.0f, 360.0f) * Mathf.Deg2Rad;

        Vector3 spawnDirection = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0);
        Vector3 spawnPosition = GameManager.Instance.playerManager.currentPlayer.transform.position + (spawnDirection * randomDistance);

        return spawnPosition;
    }


    private void AttemptSpawnEnemy()
    {
        if (GameManager.Instance.entityManager.ReachMobCap())
        {
            return;
        }

        SpawnEnemy();
    }


    public void SpawnEnemy()
    {
        EnemySpawnEntry monster = PickMonster();
        Vector3 spawnPosition = PickSpawnLocation();

        GameObject spawnedEnemy = GameManager.Instance.poolManager.Spawn(monster.prefab.gameObject, spawnPosition, Quaternion.identity, GameManager.Instance.entityManager.transform);

        if (spawnedEnemy == null)
        {
            return;
        }
    }

    private void SpawnUntilReachMinimumWeight()
    {
        int enemyLeftToFullFill = currentWave.minimumWaveWeight - GameManager.Instance.entityManager.GetCurrentMonsterWeight();

        if (enemyLeftToFullFill <= 0)
        {
            return;
        }

        for (int i = 0; i < enemyLeftToFullFill; i++)
        {
            AttemptSpawnEnemy();
        }
    }
}
