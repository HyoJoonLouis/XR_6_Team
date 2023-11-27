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
    [SerializeField] Sprite[] itemSprites;


    [HideInInspector] public List<GameObject> items;

    
    [Header("Alice")]
    public UnityEvent OnGameOverEvent;
    GameObject GameOverCanvas;

    public GameObject GameClearCanvas;


    private void Awake()
    { 
        GameOverCanvas = GameObject.Find("GameOverCanvas");
        GameOverCanvas.SetActive(false);
        GameClearCanvas = GameObject.Find("GameClearCanvas");
        GameClearCanvas.SetActive(false);
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

    public void GetItem(WeaponType type, int level)
    {
        ShowItemImage(itemSprites[(int)type]);
    }

    public void ShowItemImage(Sprite sprite)
    {
        if (Item[0].sprite == null)
        {
            Item[0].sprite = sprite;
            Color tempColor = Item[0].color;
            tempColor.a = 255;
            Item[0].color = tempColor;
        }
        else if (Item[1].sprite == null)
        {
            Item[1].sprite = sprite;
            Color tempColor = Item[1].color;
            tempColor.a = 255;
            Item[1].color = tempColor;
        }
        else if (Item[2].sprite == null)
        {
            Item[2].sprite = sprite;
            Color tempColor = Item[2].color;
            tempColor.a = 255;
            Item[2].color = tempColor;
        }
    }

    public void UseItem()
    {
        if (Item[0].sprite != null)
        {
            Item[0].sprite = null;
            Color tempColor = Item[0].color;
            tempColor.a = 0;
            Item[0].color = tempColor;

            if (Item[1].sprite != null)
            {
                ShowItemImage(Item[1].sprite);
                Item[1].sprite = null;
                tempColor = Item[0].color;
                tempColor.a = 0;
                Item[1].color = tempColor;
            }
            if (Item[2].sprite != null)
            {
                ShowItemImage(Item[2].sprite);
                Item[2].sprite = null;
                tempColor = Item[2].color;
                tempColor.a = 0;
                Item[2].color = tempColor;
            }
        }
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
        SceneManager.LoadScene(0);
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
