using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainScene : MonoBehaviour
{
    public int CurrentScene;

    public void Return()
    {
        GameManager.instance.SceneCompleted[CurrentScene] = true;
        GameManager.instance.CurrentScene = CurrentScene;
        if (CurrentScene == 4)
            GameManager.instance.SceneCompleted[CurrentScene + 1] = true;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
