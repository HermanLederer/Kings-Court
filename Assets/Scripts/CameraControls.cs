using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
	// Other components
	private Camera cam;

	// Private variables
	private Vector3 initialPosition;
	private Vector3 initialRotation;
	private float zoom;

	//--------------------------
	// MonoBehaviour methods
	//--------------------------
	void Awake()
    {
		cam = GetComponent<Camera>();

		initialPosition = transform.position;
		initialRotation = transform.rotation.eulerAngles;

		zoom = 0f;
	}

    void Update()
    {
		Vector3 pos = initialPosition;
		pos.z += (Input.mousePosition.x - Screen.width / 2) * 0.025f;
		pos.x -= (Input.mousePosition.y - Screen.height / 2) * 0.025f;
		cam.transform.position = pos;

		Vector3 rot = initialRotation;
		rot.y += (Input.mousePosition.x - Screen.width / 2) * 0.025f;
		rot.x -= (Input.mousePosition.y - Screen.height / 2) * 0.025f;
		cam.transform.rotation = Quaternion.Euler(rot);

		zoom += Input.GetAxis("Mouse ScrollWheel") * 25f;
		zoom = Mathf.Clamp(zoom, 0f, 25f);
		cam.transform.position += cam.transform.forward * zoom;
	}
}
