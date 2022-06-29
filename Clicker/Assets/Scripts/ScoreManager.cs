using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;

    private int _score;
    private List<int> _records;

    public SaveData Data { get; private set; }

    private void Start()
    {
        Enemy.OnEnemyDied += () => 
        {
            _score += 1; 
            SetScore(_score); 
        };

        LoadData();
    }

    private void LoadData() 
    {
        Data = SaveSystem.LoadData();

        if (Data == null) 
            Data = new SaveData(0, new int[0]);

        _records = new List<int>(Data.Records);
    }

    public void SetScore(int value) 
    {
        if (GameManager.IsGameStarted == false) 
            return;

        _score = value;
        _scoreText.text = _score.ToString();
    }

    public void EnableScoreText(bool enable) 
    {
        _scoreText.enabled = enable;
    }

    public void SaveData() 
    {
        if (_score > Data.LastRecord)
        {
            _records.Add(_score);
            Data.LastRecord = _score;
            Data.Records = _records.ToArray();
        }

        SaveSystem.SaveData(Data);
    }
}