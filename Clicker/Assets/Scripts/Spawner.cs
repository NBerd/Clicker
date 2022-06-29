using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameArea _gameArea;
    [SerializeField] private Enemy[] _enemyPrefabs;
    [SerializeField] private Buster[] _busterPrefabs;
    [SerializeField] private float _enemySpawnTime;
    [SerializeField] private float _minEnemySpawnTime;
    [SerializeField] private float _busterSpawnTime;

    public static List<Enemy> enemys = new List<Enemy>();

    private Transform _enemyContainer;

    public static Spawner Instance;
    public bool IsFreeze { get; set; } = false;

    #region Singleton
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    #endregion

    private void Start()
    {
        _enemyContainer = new GameObject("Enemy Container").transform;

        StartCoroutine(SpawnEnemyCoroutine());
        StartCoroutine(SpawnBusterCoroutine());
    }

    IEnumerator SpawnBusterCoroutine() 
    {
        while (true)
        {
            if (Buster.BusterAvailavle == false) 
            {
                yield return new WaitForSeconds(_busterSpawnTime);

                SpawnBuster();
            }

            yield return null;
        }
    }

    private void SpawnBuster()
    {
        Vector3 position = _gameArea.GetRandomPosition();
        Instantiate(GetRandomValue(_busterPrefabs), position, Quaternion.identity);
    }

    IEnumerator SpawnEnemyCoroutine() 
    {
        while (true) 
        {
            if (IsFreeze == false) 
            {
                SpawnEnemy();

                float spawnTime = Mathf.Clamp(_enemySpawnTime / GameManager.GameDifficulty, _minEnemySpawnTime, _enemySpawnTime);

                yield return new WaitForSeconds(spawnTime);
            }

            yield return null;
        }
    }

    private void SpawnEnemy() 
    {
        Vector3 position = _gameArea.GetRandomPosition();
        Enemy enemy = Instantiate(GetRandomValue(_enemyPrefabs), position, Quaternion.identity);
        enemy.transform.SetParent(_enemyContainer);
        enemy.Controller.GameArea = _gameArea;

        AddEnemy(enemy);
    }

    private T GetRandomValue<T>(T[] array) 
    {
        int randomIndex = Random.Range(0, array.Length);

        return array[randomIndex];
    }

    public static void AddEnemy(Enemy enemy) 
    {
        enemys.Add(enemy);
    }

    public static void RemoveEnemy(Enemy enemy) 
    {
        enemys.Remove(enemy);
    }
}