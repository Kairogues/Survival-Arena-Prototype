using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct EnemySpawnEntry
{
    public Enemy prefab;
    public float spawnChance; // The chance this enemy got spawned during a spawn attempt
}

[CreateAssetMenu(fileName = "WaveData", menuName = "Scriptable Objects/WaveData")]
public class WaveData : ScriptableObject
{   
    [SerializeField] public int waveIndex;
    [SerializeField] public float waveDuration;
    [SerializeField] public float spawnInterval;
    [SerializeField] public int minimumWaveWeight;
    [SerializeField] public List<EnemySpawnEntry> enemyPool;
}
