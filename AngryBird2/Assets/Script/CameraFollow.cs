using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;

    private void Awake()
    {
        instance = this;
    }
    public bool IsFollowing;
    public GameObject BirdToFollow;

    public Vector3 StartingPosition;

    private const float minCameraX = 0;
    private const float maxCameraX = 13;

    private void Start()
    {
        StartingPosition = transform.position;
    }

    void Update()
    {
        if (IsFollowing == true)
        {
            if (BirdToFollow != null) //bird will be destroyed if it goes out of the scene
            {
                var birdPosition = BirdToFollow.transform.position;
                float x = Mathf.Clamp(birdPosition.x, minCameraX, maxCameraX);
                //camera follows bird's x position
                transform.position = new Vector3(x, StartingPosition.y, StartingPosition.z);
            }
            else
            {
                IsFollowing = false;
            }
        }
        if (IsFollowing == false)
        {
            transform.position = StartingPosition;
        }
    }

}
