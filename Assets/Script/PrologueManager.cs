using UnityEngine;
using System.Collections;

public class PrologueManager : MonoSingleton<PrologueManager>
{
	IEnumerator Start ()
	{
		yield return new WaitForSeconds(4.5f);
		Application.LoadLevel ("Game");
	}
}
