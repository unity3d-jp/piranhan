using UnityEngine;
using System.Collections;

public class AppInitializer : MonoBehaviour {

	IEnumerator Start () {
		yield return null;

#if UNITY_IPHONE && !UNITY_EDITOR
		Application.targetFrameRate = 60;
		AudioSettings.outputSampleRate = 44100;
#endif

		yield return null;

		Application.LoadLevel (1);
	}
}
