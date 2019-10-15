using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorMovement : MonoBehaviour
{


    public List<GameObject> Players;

    public float DepthUpdateSpeed = 5;
    public float AngleUpdateSpeed = 7f;
    public float PositionUpdateSpeed = 5f;

    //zoom out and in at most
    public float DepthMax = -10f;
    public float DepthMin = -22f;

    public float AngleMax = 11f;
    public float AngleMin = 3f;

    private float CameraEulerX;
    private Vector3 CameraPosition;

    // Start is called before the first frame update

    // Update is called once per frame
    private void LateUpdate()
    {
        CalculateCameraLocations();
        MoveCamera();
    }

    private void MoveCamera()
    {
        Vector3 position = gameObject.transform.position;
        if (position != CameraPosition)
        {
            Vector3 targetPosition = Vector3.zero;
            targetPosition.x = Mathf.MoveTowards(position.x, CameraPosition.x, PositionUpdateSpeed * Time.deltaTime);
            targetPosition.y = Mathf.MoveTowards(position.y, CameraPosition.y, PositionUpdateSpeed * Time.deltaTime);
            targetPosition.z = -20f;
            gameObject.transform.position = targetPosition;
        }

        Vector3 LocalEulerAngles = gameObject.transform.localEulerAngles;
        if (LocalEulerAngles.x != CameraEulerX)
        {
            Vector2 targetEulerAngles = new Vector2(CameraEulerX, LocalEulerAngles.y);
            gameObject.transform.localEulerAngles = Vector3.MoveTowards(LocalEulerAngles, targetEulerAngles, AngleUpdateSpeed * Time.deltaTime);
        }
    }
    private void CalculateCameraLocations()
    {
        Vector2 averageCenter = Vector2.zero;
        Vector2 totalPositions = Vector2.zero;
        Bounds playerBounds = new Bounds();
        for (int i = Players.Count - 1; i > -1; i--)
        {
            if (Players[i] == null)
                Players.RemoveAt(i);
        }

        for (int i = 0; i < Players.Count; i++)
        {
            Vector2 playerPosition = Players[i].transform.position;

                float playerX = playerPosition.x;
                float playerY =playerPosition.y;
                playerPosition = new Vector2(playerX, playerY);


            totalPositions += playerPosition;
            playerBounds.Encapsulate(playerPosition);
        }

        averageCenter = (totalPositions / Players.Count);
        float extents = (playerBounds.extents.x + playerBounds.extents.y);
    //    float lerpPercent = Mathf.InverseLerp(0, (FocusLevel.HalfXBounds + FocusLevel.HalfYBounds) / 2, extents);

     //   float depth = Mathf.Lerp(DepthMax, DepthMin, lerpPercent);
      //  float angle = Mathf.Lerp(AngleMax, AngleMin, lerpPercent);

       // CameraEulerX = angle;
        CameraPosition = new Vector3(averageCenter.x, averageCenter.y);

    }
}
