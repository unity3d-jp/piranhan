using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour
{
	
	public AudioClip clip;
	
	public void PlayOneShot ()
	{
		if (clip != null)
			AudioSource.PlayClipAtPoint (clip, Vector3.zero);
	}
}
