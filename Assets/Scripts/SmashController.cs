using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashController : MonoBehaviour {
    public FocusLevel FocusLevel;

    public List<GameObject> Players;

    public float DepthUpdateSpeed = 7f;
    public float AngleUpdateSpeed = 7f;
    public float PositionUpdateSpeed = 10f;

    public float DepthMax = -5f;
    public float DepthMin = -22f;

    public float AngleMax = 11f;
    public float AngleMind = 3f;

    private float CameraEurlerX;
    private Vector3 CameraPosition;



    private void Start()
    {
        
    }

    private void LateUpdate()
    {
        CalculateCameraLocations();
        MoveCamera();
    }


    private void MoveCamera()
    {

    }

    private void CalculateCameraLocations()
    {
        Vector3 averageCenter = Vector3.zero;
        Vector3 totalPositions = Vector3.zero;
        Bounds playerBounds = new Bounds();

        for (int i = 0; i < Players.Count; i++)
        {
            Vector3 playerPostion = Players[i].transform.position;

            if (!FocusLevel.FocusBounds.Contains(playerPostion))
            {

            }
        }
    }
}