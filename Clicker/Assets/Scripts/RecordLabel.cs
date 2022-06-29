using UnityEngine;
using UnityEngine.UI;

public class RecordLabel : MonoBehaviour
{
    [SerializeField] private Text _recordText;
    [SerializeField] private Text _numberText;

    public void SetRecord(int record, int number) 
    {
        _recordText.text = record.ToString();
        _numberText.text = number.ToString();
    }
}
