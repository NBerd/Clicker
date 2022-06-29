using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _startGameDifficulty;
    [SerializeField] private float _maxGameDifficulty;
    [SerializeField] private float _difficultyMultiplier;

    public static float GameDifficulty { get; private set; }

    private float _gameTime;
    private bool _gameStarted = true;

    public void StartGame() 
    {
        GameDifficulty = _startGameDifficulty;
        _gameTime = 0f;
        _gameStarted = true;
    }

    private void Update()
    {
        if (_gameStarted == false) 
            return;

        _gameTime += Time.deltaTime;

        ChangeDifficulty();
    }

    private void ChangeDifficulty() 
    {
        float difficulty = _gameTime * _difficultyMultiplier;
        difficulty = Mathf.Clamp(difficulty, _startGameDifficulty, _maxGameDifficulty);

        GameDifficulty = difficulty;

        Debug.Log(difficulty);
    }
}