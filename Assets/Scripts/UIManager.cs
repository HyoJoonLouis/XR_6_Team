using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [HideInInspector] public static UIManager instance;

    [Header("Player")]
    [SerializeField] Image[] Health;
    [SerializeField] Sprite HpEmptySprite;
    [SerializeField] Sprite HpFullSprite;

    private void Awake()
    {
        if(instance == null)
            instance = this; 
    }

    public void SetHealth(int value)
    {
        int i;
        for(i = 0; i< value; i++)
        {
            Health[i].sprite = HpFullSprite;
        }
        for(;i< Health.Length; i++)
        {
            Health[i].sprite = HpEmptySprite;
        }
    }
}
