using UnityEngine;

public static class SaveSystem
{
    private const string SAVE_KEY = nameof(SAVE_KEY);

    public static void SaveData(SaveData data) 
    {
        string json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString(SAVE_KEY, json);
    }

    public static SaveData LoadData() 
    {
        string json = PlayerPrefs.GetString(SAVE_KEY, null);

        return JsonUtility.FromJson<SaveData>(json);
    }
}