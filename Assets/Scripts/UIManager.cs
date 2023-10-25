using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Player")]
    [SerializeField] Image[] Health;
    [SerializeField] Sprite HpEmptySprite;
    [SerializeField] Sprite HpFullSprite;

    [Header("Alice")]
    public UnityEvent OnGameOverEvent;
    GameObject GameOverCanvas;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        GameOverCanvas = GameObject.Find("GameOverCanvas");
        GameOverCanvas.SetActive(false);
    }

    public void SetHealth(int value)
    {
        int i;
        for(i = 0; i< value ; i++)
        {
            Health[i].sprite = HpFullSprite;
        }
        for(;i< Health.Length; i++)
        {
            Health[i].sprite = HpEmptySprite;
        }
    }

    public void GameOver()
    {
        OnGameOverEvent?.Invoke();
        StartCoroutine(OnGameOverCoroutine());
    
    }

    public void OnRestartClicked()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }

    IEnumerator OnGameOverCoroutine()
    {
        GameOverCanvas.SetActive(true);
        while(Time.timeScale >= 0.2f)
        {
            Time.timeScale -= 0.2f;
            yield return new WaitForSeconds(0.05f);
        }
        Time.timeScale = 0;
    }
}
