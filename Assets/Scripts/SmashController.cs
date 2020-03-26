using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashController : MonoBehaviour
{
	// Editor variables
	public FocusLevel FocusLevel;

	// Private variables
	private List<Transform> Players;
	public float PositionUpdateSpeed = 10f;

	private void Start()
	{
		GameObject[] game = GameObject.FindGameObjectsWithTag("Player");

		AICore.AIBrainInterface aiInterface;

		foreach (GameObject player in game)
			if (gameObject.TryGetComponent<AICore.AIBrainInterface>(out aiInterface))
				Players.Add(player.transform);
	}

	private void Update()
	{
		//CalculateCameraLocations();
		//MoveCamera();
	}

	private void MoveCamera()
	{
		//Vector3 position = gameObject.transform.position;
		//if (position != CameraPosition)
		//{
		//	Vector3 targetPosition = Vector3.zero;
		//	targetPosition.x = Mathf.MoveTowards(position.x, CameraPosition.x, PositionUpdateSpeed * Time.deltaTime);
		//	targetPosition.y = Mathf.MoveTowards(position.y, CameraPosition.y, PositionUpdateSpeed * Time.deltaTime);
		//	targetPosition.x = Mathf.MoveTowards(position.y, CameraPosition.y, PositionUpdateSpeed * Time.deltaTime);
		//	gameObject.transform.position = targetPosition;
		//}
	}

	private void CalculateCameraLocations()
	{
		//Vector3 averageCenter = Vector3.zero;
		//Vector3 totalPositions = Vector3.zero;
		//Bounds playerBounds = new Bounds();

		//for (int i = 0; i < Players.Count; i++)
		//{
		//	Vector3 playerPostion = Players[i].transform.position;

		//	if (!FocusLevel.FocusBounds.Contains(playerPostion))
		//	{
		//		float playerX = Mathf.Clamp(playerPostion.x, FocusLevel.FocusBounds.min.x, FocusLevel.FocusBounds.max.x);
		//		float playerY = Mathf.Clamp(playerPostion.y, FocusLevel.FocusBounds.min.y, FocusLevel.FocusBounds.max.y);
		//		float playerZ = Mathf.Clamp(playerPostion.z, FocusLevel.FocusBounds.min.z, FocusLevel.FocusBounds.max.z);
		//		playerPostion = new Vector3(playerX, playerY, playerZ);
		//	}

		//	totalPositions += playerPostion;
		//	playerBounds.Encapsulate(playerPostion);
		//}

		//averageCenter = (totalPositions / Players.Count);

		//float extents = (playerBounds.extents.x + playerBounds.extents.y);
		//float lerpPercent = Mathf.InverseLerp(0, (FocusLevel.HalfXBounds + FocusLevel.HalfYBounds) / 2, extents);

		//float depth = Mathf.Lerp(DepthMax, DepthMin, lerpPercent);
		////float angle = Mathf.Lerp(AngleMax, AngleMin, lerpPercent);

		////CameraEurlerX = angle;
		//CameraPosition = new Vector3(averageCenter.x, averageCenter.y, depth);
	}


}