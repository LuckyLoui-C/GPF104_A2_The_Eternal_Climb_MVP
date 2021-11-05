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
        Instantiate(background, new Vector3(0, backgroundPosY, 0), Quaternion.identity);
        backgroundPosY += distanceBetweenBackgrounds;
        Instantiate(background, new Vector3(0, backgroundPosY, 0), Quaternion.identity);
        movingCamera = FindObjectOfType<Camera>().gameObject;
        StartCoroutine(BackgroundCheck());
        StartCoroutine(SpawnPlatform());
    }
    private void FixedUpdate()
    {
        movingCamera.transform.position += new Vector3(0, cameraSpeed * Time.deltaTime, 0);
    }
    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("PlatSpawn") != null)
        {
            canSpawn = true;
            platSpawns = GameObject.FindGameObjectsWithTag("PlatSpawn");
        }
        else
        {
            canSpawn = false;
        }
    }
    IEnumerator BackgroundCheck()
    {
        yield return new WaitForSeconds(timeToNewBackground);
        backgroundPosY += distanceBetweenBackgrounds;
        GameObject go = Instantiate(background, new Vector3(0, backgroundPosY, 0), Quaternion.identity);
        if (Vector2.Distance(go.transform.position, movingCamera.transform.position) > distanceToDestroyBackground)
        {
            Destroy(go);
            backgroundPosY -= distanceBetweenBackgrounds;
        }
        GameObject[] backgrounds = GameObject.FindGameObjectsWithTag("Background");
        foreach(GameObject background in backgrounds)
        {
            if (background.transform.position.y < movingCamera.transform.position.y && Vector2.Distance(background.transform.position, movingCamera.transform.position) > distanceToDestroyBackground)
            {
                Destroy(background);
            }
        }
        StartCoroutine(BackgroundCheck());
    }
    IEnumerator SpawnPlatform()
    {

        if (canSpawn)
        {
            int selectedPlatNum = Random.Range(0, platSpawns.Length);
            int randomPosX = Random.Range(0, 10);
            while (platSpawns[selectedPlatNum] == null)
            {
                selectedPlatNum = Random.Range(0, platSpawns.Length);
            }
            Vector2 newTrans = new Vector2(platSpawns[selectedPlatNum].transform.position.x + randomPosX, platSpawns[selectedPlatNum].transform.position.y);
            DestroyPlatform(selectedPlatNum);
            if (newTrans.y > movingCamera.transform.position.y + 5)
            {
                 yield return new WaitForSeconds(timeToNewPlat);
                 Instantiate(platform, newTrans, Quaternion.identity);
                 StartCoroutine(SpawnPlatform());
            }
            else
            {
                 yield return new WaitForSeconds(timeToNewPlat / 3);
                  StartCoroutine(SpawnPlatform());
            }
        }
        else
        {
             yield return new WaitForSeconds(timeToNewPlat / 3);
             StartCoroutine(SpawnPlatform());
        }
    }
    void DestroyPlatform(int selectedPlatNum)
    {
        if (platSpawns[selectedPlatNum].GetComponent<NeigbouringSpawns>().nullAbove == false)
        {
            GameObject topSpawn = platSpawns[selectedPlatNum].GetComponent<NeigbouringSpawns>().aboveSpawn.gameObject;
            if (topSpawn.GetComponent<NeigbouringSpawns>().nullAbove == false)
            {
                GameObject topTopSpawn = topSpawn.GetComponent<NeigbouringSpawns>().aboveSpawn.gameObject;
                topTopSpawn.GetComponent<NeigbouringSpawns>().nullBelow = true;
            }
            Destroy(topSpawn);
        }

        if(platSpawns[selectedPlatNum].GetComponent<NeigbouringSpawns>().nullBelow == false)
        {
            GameObject bottomSpawn = platSpawns[selectedPlatNum].GetComponent<NeigbouringSpawns>().belowSpawn.gameObject;
            if (bottomSpawn.GetComponent<NeigbouringSpawns>().nullBelow == false)
            {
                GameObject bottomBottomSpawn = bottomSpawn.GetComponent<NeigbouringSpawns>().belowSpawn.gameObject;
                bottomBottomSpawn.GetComponent<NeigbouringSpawns>().nullAbove = true;
            }
            Destroy(bottomSpawn);
        }
        Destroy(platSpawns[selectedPlatNum]);
    }
}