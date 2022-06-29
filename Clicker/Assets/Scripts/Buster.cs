using UnityEngine;

public abstract class Buster : MonoBehaviour, IHitable
{
    public static bool BusterAvailavle { get; private set; }

    private void Start()
    {
        BusterAvailavle = true;
    }

    public void Hit()
    {
        if (GameManager.IsGameStarted == true)
            UseBuster();

        DestroyBuster();
    }

    protected abstract void UseBuster();

    public void DestroyBuster()
    {
        BusterAvailavle = false;
        Destroy(gameObject);
    }
}