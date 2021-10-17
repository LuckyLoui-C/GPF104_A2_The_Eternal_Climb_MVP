using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject credits;
    public void ToggleCredits()
    {
        if(credits.activeSelf == true)
        {
            credits.SetActive(false);
        }
        else
        {
            credits.SetActive(true);
        }
    }
}