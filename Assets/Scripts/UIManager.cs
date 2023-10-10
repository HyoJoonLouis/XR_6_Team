using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [HideInInspector] public static UIManager instance;

    [Header("Player")]
    [SerializeField] Image[] Health;
    [SerializeField] Sprite HpEmptySprite;
    [SerializeField] Sprite HpFullSprite;

    [Header("Alice")]
    public UnityEvent OnGameOverEvent;
    [SerializeField] GameObject GameOverPanel;

    private void Awake()
    {
        if(instance == null)
            instance = this;
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
        GameOverPanel.SetActive(true);
    }

    public void OnRestartClicked()
    {
        SceneManager.LoadScene(0);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
