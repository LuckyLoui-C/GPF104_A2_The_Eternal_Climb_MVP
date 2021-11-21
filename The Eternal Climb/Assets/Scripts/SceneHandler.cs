using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
   // Now any scene that has a button for scene changing can just access this and call LoadScene and tell it the name of the scene it wants to got to
   public void LoadScene(string sceneName)
    {
        StartCoroutine(WaitBeforeLoad(sceneName));
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
    private IEnumerator WaitBeforeLoad(string sceneName)
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(sceneName);
    }
}
