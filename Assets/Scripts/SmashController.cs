using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashController : MonoBehaviour {
    public FocusLevel FocusLevel;

    public List<Transform> Players;
    //here we need to add the AI to. as individuals I would guess.
    //if they die, they need to be removed.

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
        GameObject[] game = GameObject.FindGameObjectsWithTag("player");

        foreach (GameObject player in game)
            if ( (gameObject.name != ("Player (Ray")) || (gameObject.name != ("Player(Herman)")) || (gameObject.name != ("Player(Amar)")) )
            {
                AddTransform(player.transform);
            }

    }
    private void Update()
    {
        //if character a team dies. remove their stuff.

    }

    public void AddTransform(Transform player)
    {
        Players.Add(player);
    }
    private void LateUpdate()
    {
        CalculateCameraLocations();
        MoveCamera();
    }

    public void RemoveTransform(Transform player) // make sure that every AI script calls this when they die.
    {
        Players.Remove(player);
    }


    private void MoveCamera()
    {
        Vector3 position = gameObject.transform.position;
        if (position != CameraPosition)
        {
            Vector3 targetPosition = Vector3.zero;
            targetPosition.x = Mathf.MoveTowards(position.x, CameraPosition.x, PositionUpdateSpeed * Time.deltaTime);
            targetPosition.y = Mathf.MoveTowards(position.y, CameraPosition.y, PositionUpdateSpeed * Time.deltaTime);
            targetPosition.x = Mathf.MoveTowards(position.y, CameraPosition.y, PositionUpdateSpeed * Time.deltaTime);
            gameObject.transform.position = targetPosition;
        }

        Vector3 localEulerAngles = gameObject.transform.localEulerAngles;
        if (localEulerAngles.x != CameraEurlerX)
        {
            Vector3 targetEulerAngles = new Vector3(CameraEurlerX, localEulerAngles.y, localEulerAngles.z);
            gameObject.transform.localEulerAngles = Vector3.MoveTowards(localEulerAngles, targetEulerAngles, AngleUpdateSpeed * Time.deltaTime);
        }
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
                float playerX = Mathf.Clamp(playerPostion.x, FocusLevel.FocusBounds.min.x, FocusLevel.FocusBounds.max.x);
                float playerY = Mathf.Clamp(playerPostion.y, FocusLevel.FocusBounds.min.y, FocusLevel.FocusBounds.max.y);
                float playerZ = Mathf.Clamp(playerPostion.z, FocusLevel.FocusBounds.min.z, FocusLevel.FocusBounds.max.z);
                playerPostion = new Vector3(playerX, playerY, playerZ);
            }

            totalPositions += playerPostion;
            playerBounds.Encapsulate(playerPostion);
        }

        averageCenter = (totalPositions / Players.Count);

        float extents = (playerBounds.extents.x + playerBounds.extents.y);
        float lerpPercent = Mathf.InverseLerp(0, (FocusLevel.HalfXBounds + FocusLevel.HalfYBounds) / 2, extents);

        float depth = Mathf.Lerp(DepthMax, DepthMin, lerpPercent);
        float angle = Mathf.Lerp(AngleMax, AngleMind, lerpPercent);

        CameraEurlerX = angle;
        CameraPosition = new Vector3(averageCenter.x, averageCenter.y, depth);
    }
}