using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A singleton responsible for orchestrating the active game loop and runtime rules.
/// Manages and orchestrates other gameplay managers to control the progression of the game, manages the state of the game
/// and the transition between different states
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] public PlayerManager playerManager;
    [SerializeField] public WaveManager waveManager;
    [SerializeField] public PoolManager poolManager;
    [SerializeField] public EntityManager entityManager;
    [SerializeField] public UpgradeManager upgradeManager;
    
    [SerializeField] public bool enableWaveSpawning = true;
    [SerializeField] public GameObject fakeEnemy;



    private void OnEnable()
    {
        playerManager.PlayerDied += OnPlayerDeath;
        waveManager.WaveEnded += ClearObjectPool;
    }


    private void OnDisable()
    {
        playerManager.PlayerDied -= OnPlayerDeath;
        waveManager.WaveEnded -= ClearObjectPool;
    }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        DontDestroyOnLoad(gameObject);
    }

    
    void Start()
    {
        if (enableWaveSpawning)
        {
            waveManager.StartGame();
        } else
        {
            poolManager.Spawn(fakeEnemy, new Vector3(-10, 0, 0), Quaternion.identity, entityManager.transform);
        }
    }

    private void Update()
    {
        if (enableWaveSpawning)
        {
            ProgressWave();
        }
    }


    private void ProgressWave()
    {
        waveManager.ProgressWave();
    }


    private void OnPlayerDeath()
    {
        Time.timeScale = 0;
        InterfaceManager.Instance.OnPlayerDeath();
    }


    public void LoadGameOverScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    private void ClearObjectPool(WaveData waveData)
    {
        poolManager.TryRemoveInactivePools();
    }
}