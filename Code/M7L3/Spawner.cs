
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Corn _cornTarget;
    [SerializeField] private Transform _topBorder;
    [SerializeField] private Transform _bottomBorder;
    [SerializeField] private float _spawnIntervalMax = 3.5f;
    [SerializeField] private float _spawnIntervalMin = 1f;
    [SerializeField] private int _baseSpawnCount = 1;

    private float _spawnTimer;

    public int SpawnCounter { get; private set; }

    private void Awake()
    {
        if (!ValidateDependencies()) enabled = false;
    }

    private void Start()
    {
        SpawnCounter = _baseSpawnCount + LevelController.Level;
    }

    private void Update()
    {
        if (LevelController.IsFinished) return;
        if (SpawnCounter <= 0) return;

        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer <= 0) SpawnEnemy();
    }

    public void InitSpawner(int level)
    {
        SpawnCounter = _baseSpawnCount + level;
    }

    private bool ValidateDependencies()
    {
        bool valid = _enemyPrefab && _cornTarget && _topBorder && _bottomBorder;

        if (!valid) Debug.LogError("Spawner dependencies not set", this);

        return valid;
    }

    private void SpawnEnemy()
    {
        SpawnCounter--;
        _spawnTimer = Random.Range(_spawnIntervalMin, _spawnIntervalMax);

        float randomY = Random.Range(_bottomBorder.position.y, _topBorder.position.y);
        Vector2 spawnPos = new(transform.position.x, randomY);

        Enemy enemy = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
        enemy.SetTarget(_cornTarget);
    }
}
