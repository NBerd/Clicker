using System.Collections.Generic;

public class DestroyBuster : Buster
{
    protected override void UseBuster()
    {
        List<Enemy> enemys = new List<Enemy>(Spawner.enemys);

        foreach(Enemy enemy in enemys) 
        {
            enemy.Disable();
            enemy.Die();
        }
    }
}