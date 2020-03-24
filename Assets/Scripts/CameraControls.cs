using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
	// Other components
	private Camera cam;

	//--------------------------
	// MonoBehaviour methods
	//--------------------------
	void Awake()
    {
		cam = GetComponent<Camera>();
		//PointerType = CursorLockMode.Locked
    }

    void Update()
    {
		Vector3 rot = new Vector3(40, -90, 0);
		rot.y += (Input.mousePosition.x - Screen.width / 2) * 0.025f;
		rot.x -= (Input.mousePosition.y - Screen.height / 2) * 0.0125f;
		cam.transform.rotation = Quaternion.Euler(rot);
    }
}
