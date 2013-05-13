using UnityEngine;

public class Dustbox : MonoSingleton<Dustbox>
{
	public void StopFishes ()
	{
		foreach (FishController fish  in GetComponentsInChildren<FishController>()) {
			fish.Stop ();
		}
	}
	
	public void StopBullet()
	{
		foreach (BulletController bullet  in GetComponentsInChildren<BulletController>()) {
			bullet.Stop();
		}
	}
}
