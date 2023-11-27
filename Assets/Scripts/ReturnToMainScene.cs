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
        SceneManager.LoadScene(0);
    }
}
