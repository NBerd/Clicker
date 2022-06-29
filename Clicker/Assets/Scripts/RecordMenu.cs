using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RecordMenu : MonoBehaviour
{
    [SerializeField] private RecordLabel[] _labels;
    [SerializeField] private ScoreManager _scoreManager;

    private void OnEnable()
    {
        SetRecords();
    }

    private void SetRecords() 
    {
        List<int> records = new List<int>(_scoreManager.Data.Records);

        int count = records.Count > _labels.Length ? _labels.Length : records.Count;

        records = records.OrderBy(n => n).Reverse().Take(count).ToList();

        for (int i = 0; i < count; i++) 
        {
            _labels[i].SetRecord(records[i], i + 1);
        }
    }
}
