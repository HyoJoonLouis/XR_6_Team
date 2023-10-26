using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneUI : MonoBehaviour
{
    public TextMeshProUGUI Text;

    int CurrentScene;
    public void Start()
    {
        CurrentScene = 0;
        Text.text = 0.ToString();
    }
    public void ChapterEnterClicked()
    {
        SceneManager.LoadScene(CurrentScene);
    }

    public void OnRightButtonClicked()
    {
        CurrentScene = Mathf.Clamp(CurrentScene + 1, 0, 2);
        Debug.Log(CurrentScene);
        Text.text = CurrentScene.ToString();
    }
    public void OnLeftButtonClicked()
    {
        CurrentScene = Mathf.Clamp(CurrentScene - 1, 0, 2);
        Text.text = CurrentScene.ToString();
    }
}
