using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MenuManager _menuManager;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private int _numberOfEnemysToDefeat;
    [SerializeField] private float _startGameDifficulty;
    [SerializeField] private float _maxGameDifficulty;
    [SerializeField] private float _difficultyMultiplier;

    private float _gameTime;

    public static float GameDifficulty { get; private set; }
    public static bool IsGameStarted { get; private set; }

    private void Start()
    {
        Spawner.OnEnemySpawn += DefeatCondition;
    }

    public void StartGame() 
    {
        IsGameStarted = true;
        GameDifficulty = _startGameDifficulty;
        _gameTime = 0f;

        _scoreManager.SetScore(0);
        _scoreManager.EnableScoreText(true);
        _menuManager.EnableMenu(false);

        Spawner.Instance.StartSpawn();
    }

    private void Update()
    {
        if (IsGameStarted == false) 
            return;

        _gameTime += Time.deltaTime;

        ChangeDifficulty();
    }

    private void ChangeDifficulty() 
    {
        float difficulty = _gameTime * _difficultyMultiplier;
        difficulty = Mathf.Clamp(difficulty, _startGameDifficulty, _maxGameDifficulty);

        GameDifficulty = difficulty;
    }

    private void DefeatCondition() 
    {
        if (Spawner.Enemies.Count >= _numberOfEnemysToDefeat)
            Defeat();
    }

    private void Defeat() 
    {
        IsGameStarted = false;

        _menuManager.EnableMenu(true);
        _scoreManager.SaveData();
        _scoreManager.EnableScoreText(false);

        Spawner.Instance.DestroyAllEnemys();
    }
}