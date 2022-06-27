using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameArea _gameArea;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float _spawnTime;

    public static List<Enemy> enemys = new List<Enemy>();

    private Transform _enemyContainer;

    private void Start()
    {
        _enemyContainer = new GameObject("Enemy Container").transform;

        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine() 
    {
        while (true) 
        {
            Vector3 position = _gameArea.GetRandomPosition();
            Enemy enemy = Instantiate(_enemyPrefab, position, Quaternion.identity);
            enemy.transform.SetParent(_enemyContainer);
            enemy.Controller.GameArea = _gameArea;

            AddEnemy(enemy);

            yield return new WaitForSeconds(_spawnTime);
        }
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