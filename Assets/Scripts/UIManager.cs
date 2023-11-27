using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum LaughterSprite
{
    WhiteRabbit = 0,
    Marchhare,
    Alice
}

public class UIManager : MonoBehaviour
{
    [SerializeField] List<Sprite> LaughterSprites;

    [Header("Player")]
    [SerializeField] Image[] Health;
    [SerializeField] Sprite HpEmptySprite;
    [SerializeField] Sprite HpFullSprite;
    [SerializeField] Image[] Item;

    [HideInInspector] public List<GameObject> items;

    
    [Header("Alice")]
    public UnityEvent OnGameOverEvent;
    GameObject GameOverCanvas;

    private void Awake()
    { 
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

    public void GetItem(GameObject item)
    {
        items.Add(item);
    }

    public void GameOver()
    {
        OnGameOverEvent?.Invoke();
        StartCoroutine(OnGameOverCoroutine());
    
    }

    public void OnRestartClicked(int i)
    {
        SceneManager.LoadScene(i);
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
