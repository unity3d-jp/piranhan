using UnityEngine;

public class Dustbox : MonoBehaviour
{

	private static Dustbox instance;
    
	public static Dustbox Instance {
		get {
			if (instance == null) {
				instance = new GameObject ("dustbox", typeof(Dustbox)).GetComponent<Dustbox> ();
			}
			return instance;
		}
	}
}
