using UnityEngine;

public class Dustbox : MonoSingleton<Dustbox>
{
	public void StopFishes ()
	{
		foreach (FishController fish  in GetComponentsInChildren<FishController>()) {
			fish.Stop ();
		}
	}
}
