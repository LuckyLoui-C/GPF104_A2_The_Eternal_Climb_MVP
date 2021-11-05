using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MainCamera"))
        {
            Debug.Log("Wow");
            FindObjectOfType<PlatformGeneration>().GenerateNewBackground();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MainCamera"))
        {
            Destroy(this.gameObject);
        }
    }
}
