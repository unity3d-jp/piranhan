using UnityEngine;
using System.Collections;

public class VirtualPad : MonoBehaviour {
	[HideInInspector]
	public bool left;
	[HideInInspector]
	public bool right;
	[HideInInspector]
	public bool up;
	[HideInInspector]
	public bool trigger;

	public Vector2 dpadCenter;
	public float dpadRadius;

	public Vector2 triggerCenter;
	public float triggerRadius;
	
	void ClearInput() {
		left = right = up = trigger = false;
	}

	void Update() {
#if (UNITY_IPHONE || UNITY_IPHONE) && !UNITY_EDITOR
		foreach (var touch in Input.touches) {
			if (touch.phase != TouchPhase.Ended) {
				TestTouch(touch);
			}
		}
#else
		if (Input.GetMouseButton(0)) TestTouch(Input.mousePosition);
#endif
	}

	void TestTouch(Vector2 position) {
		if ((position - triggerCenter).magnitude < triggerRadius) {
			trigger = true;
			return;
		}

		if ((position - dpadCenter).magnitude < dpadRadius) {
			var r = position - dpadCenter;
			if (r.x > r.y || -r.x > r.y || r.y < 0) {
				if (r.x > 0) {
					right = true;
				} else {
					left = true;
				}
			} else {
				up = true;
			}
		}
	}

	void OnDrawGizmos() {
		Vector2 ux = new Vector2(1, 0);
		Vector2 uy = new Vector2(0, 1);

		DrawGizmoLine(dpadCenter, dpadCenter + (uy + ux) * dpadRadius);
		DrawGizmoLine(dpadCenter, dpadCenter + (uy - ux) * dpadRadius);
		DrawGizmoLine(dpadCenter, dpadCenter - uy * dpadRadius);

		DrawGizmoLine(triggerCenter - ux * triggerRadius, triggerCenter + ux * triggerRadius);
		DrawGizmoLine(triggerCenter - uy * triggerRadius, triggerCenter + uy * triggerRadius);
	}

	void DrawGizmoLine(Vector2 p1, Vector2 p2) {
		Gizmos.DrawLine(Camera.main.ScreenToWorldPoint(new Vector3(p1.x, p1.y, 1)),
		                Camera.main.ScreenToWorldPoint(new Vector3(p2.x, p2.y, 1)));
	}
}
