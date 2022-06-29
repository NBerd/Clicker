using System.Collections.Generic;

public class DestroyBuster : Buster
{
    protected override void UseBuster()
    {
        Spawner.Instance.DestroyAllEnemys();
    }
}