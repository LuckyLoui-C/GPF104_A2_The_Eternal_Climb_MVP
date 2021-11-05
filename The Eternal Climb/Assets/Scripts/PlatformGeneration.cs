using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneration : MonoBehaviour
{
    public float timeToNewBackground;
    public float distanceToDestroyBackground;
    public float cameraSpeed;
    public float distanceBetweenBackgrounds;
    private float backgroundPosY;

    public float timeToNewPlat;
    private bool canSpawn;

    public GameObject platform;
    public GameObject background;
    private GameObject movingCamera;
    private GameObject[] platSpawns;
    private void Start()
    {
        backgroundPosY = 0;
        GenerateNewBackground();
        movingCamera = FindObjectOfType<Camera>().gameObject;
    }
    private void FixedUpdate()
    {
        movingCamera.transform.position += new Vector3(0, cameraSpeed * Time.deltaTime, 0);
    }

    public void GenerateNewBackground()
    {
        Instantiate(background, new Vector3(0, backgroundPosY, 0), Quaternion.identity);
        backgroundPosY += distanceBetweenBackgrounds;
    }
}
