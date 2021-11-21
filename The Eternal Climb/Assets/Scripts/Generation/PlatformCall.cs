using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCall : MonoBehaviour
{
    [Header("Spawning Platforms")]
    public GameObject platform;
    public Transform spawn1;
    public Transform spawn2;
    private Transform player;
    public float xDiff;
    public float yDiff;

    [Header("Spawning of Enemies/Items")]
    [SerializeField] private bool canSpawnSomething = true;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();
    public List<GameObject> powersToSpawn = new List<GameObject>();
    public Transform spawnTrans;
    private void Start()
    {
        if(canSpawnSomething)
        {
            canSpawnSomething = (Random.Range(1, 4) == 1) ? true : false;
            if (canSpawnSomething)
            {
                bool willSpawnPowerUp = (Random.Range(1, 6) == 1) ? true : false;
                if(willSpawnPowerUp)
                {
                    int entitySpawnNum = Random.Range(0, enemiesToSpawn.Count);
                    Instantiate(powersToSpawn[entitySpawnNum], spawnTrans.position, Quaternion.identity);
                }
                else
                {
                    int entitySpawnNum = Random.Range(0, enemiesToSpawn.Count);
                    if (enemiesToSpawn[entitySpawnNum].name == "Dragon")
                    {
                        Instantiate(enemiesToSpawn[entitySpawnNum], new Vector2(transform.position.x, transform.position.y + 2), Quaternion.identity);
                    }
                    else
                        Instantiate(enemiesToSpawn[entitySpawnNum], spawnTrans.position, Quaternion.identity);
                }
               
            }
        }
        
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
        GameObject go = null;
        switch (Random.Range(1, 3))
        {
            case 1:
                Vector3 newVec1 = new Vector3(Random.Range(-xDiff, xDiff), Random.Range(-yDiff, yDiff), 0);
                spawn1.position += newVec1;
                if(CheckCanSpawn(spawn1))
                    go = Instantiate(platform, spawn1.position, Quaternion.identity);
                else
                    go = Instantiate(platform, spawn2.position, Quaternion.identity);
                break;
            case 2:
                Vector3 newVec2 = new Vector3(Random.Range(-xDiff, xDiff), Random.Range(-yDiff, yDiff), 0);
                spawn2.position += newVec2;
                if (CheckCanSpawn(spawn2))
                    go = Instantiate(platform, spawn2.position + newVec2, Quaternion.identity);
                else
                    go = Instantiate(platform, spawn1.position, Quaternion.identity);
                break;
        }
        go.GetComponent<PlatformCall>().canSpawnSomething = true;
        Destroy(spawn1.gameObject);
        Destroy(spawn2.gameObject);
    }
    bool CheckCanSpawn(Transform spawnPos)
    {
        bool canSpawn = spawnPos.position.x <= 6 && spawnPos.position.x >= -6;
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
