using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCall : MonoBehaviour
{
    public GameObject platform;
    public Transform spawn1;
    public Transform spawn2;
    private Transform player;
    public LayerMask antiSpawn;
    public float xDiff;
    public float yDiff;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        if(Vector2.Distance(this.transform.position,player.position) <= 10)
        {
            GeneratePlatform();
        }
        else
        {
            StartCoroutine(WaitForDistance());
        }
    }
    void GeneratePlatform()
    {
        switch (Random.Range(1, 3))
        {
            case 1:
                Vector3 newVec1 = new Vector3(Random.Range(-xDiff, xDiff), Random.Range(-yDiff, yDiff), 0);
                spawn1.position += newVec1;
                if(CheckCanSpawn(spawn1))
                    Instantiate(platform, spawn1.position, Quaternion.identity);
                else
                    Instantiate(platform, spawn2.position, Quaternion.identity);
                break;
            case 2:
                Vector3 newVec2 = new Vector3(Random.Range(-xDiff, xDiff), Random.Range(-yDiff, yDiff), 0);
                spawn2.position += newVec2;
                if (CheckCanSpawn(spawn2))
                    Instantiate(platform, spawn2.position + newVec2, Quaternion.identity);
                else
                    Instantiate(platform, spawn1.position, Quaternion.identity);
                break;
        }
        Destroy(spawn1.gameObject);
        Destroy(spawn2.gameObject);
    }
    bool CheckCanSpawn(Transform spawnPos)
    {
        bool canSpawn = spawnPos.position.x <= 6 && spawnPos.position.x >= -6;
        Debug.Log(canSpawn);
        return canSpawn;
    }
    IEnumerator WaitForDistance()
    {
        yield return new WaitForSeconds(1);
        player = FindObjectOfType<PlayerMovement>().transform;
        if (Vector2.Distance(this.transform.position, player.position) <= 10)
        {
            GeneratePlatform();
        }
        else
        {
            StartCoroutine(WaitForDistance());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("MainCamera"))
        {
            Destroy(this.gameObject);
        }
    }
}
