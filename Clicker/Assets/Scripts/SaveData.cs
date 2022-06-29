[System.Serializable]
public class SaveData
{
    public int LastRecord;
    public int[] Records;

    public SaveData(int lastRecord, int[] records) 
    {
        LastRecord = lastRecord;
        Records = records;
    }
}