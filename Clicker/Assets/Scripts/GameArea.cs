using UnityEngine;

[System.Serializable]
public class GameArea
{
    [SerializeField] private float _minPosX, _maxPosX;
    [SerializeField] private float _minPosZ, _maxPosZ;

    public Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(_minPosX, _maxPosX);
        float randomZ = Random.Range(_minPosZ, _maxPosZ);

        return new Vector3(randomX, 0, randomZ);
    }
}