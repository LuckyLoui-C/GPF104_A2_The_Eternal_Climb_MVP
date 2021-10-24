using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [Header("Component References")]
    public PlayerHealth playerHealth;
    public Sprite fullHealth;
    public Sprite midHealth;
    public Sprite lowHealth;

    [Header("Display Settings")]
    public int healthRequired; // Amount of player health needed to display this heart
    public bool displayHeart = true;

    private void Update()
    {
        if (playerHealth.health >= healthRequired)
        {
            displayHeart = true;
            if (playerHealth.health == 3.0f && displayHeart)
            {
                GetComponent<UnityEngine.UI.Image>().sprite = fullHealth;
                GetComponent<UnityEngine.UI.Image>().color = Color.white;
            }
            else if (playerHealth.health == 2.0f && displayHeart)
            {
                GetComponent<UnityEngine.UI.Image>().sprite = midHealth;
                GetComponent<UnityEngine.UI.Image>().color = Color.white;
            }
            else if (playerHealth.health == 1.0f && displayHeart)
            {
                GetComponent<UnityEngine.UI.Image>().sprite = lowHealth;
                GetComponent<UnityEngine.UI.Image>().color = Color.white;
            }
        }
        else
        {
            GetComponent<UnityEngine.UI.Image>().color = Color.black;
        }
    }
}