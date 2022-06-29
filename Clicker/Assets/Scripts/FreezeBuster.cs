using System.Collections;
using UnityEngine;

public class FreezeBuster : Buster
{
    private const float FREEZE_TIME = 3f;

    private Spawner _spawner;

    protected override void UseBuster()
    {
        _spawner = Spawner.Instance;
        _spawner.StartCoroutine(Freeze());
    }

    IEnumerator Freeze() 
    {
        _spawner.IsFreeze = true;

        yield return new WaitForSeconds(FREEZE_TIME);

        _spawner.IsFreeze = false;
    }
}
