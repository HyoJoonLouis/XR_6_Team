using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[Serializable]
public enum MarchhareEmotion
{
    smile,
    normal
}

[Serializable]
public enum HatterEmotion
{
    smile,
    normal,
    laugh
}

[Serializable]
public class Script1_2
{
    public List<bool> isPlayer;
    public LEmotion PlayerEmotion;
    public MarchhareEmotion MarchEmotion;
    public HatterEmotion HatterEmotion;
    public string script;
}

public class ChatScript1_2 : MonoBehaviour
{
    public List<Script1_2> scripts;
    public int CurrentScript;

    [SerializeField] public List<Sprite> PlayerSprite;
    [SerializeField] public List<Sprite> MarchSprite;
    [SerializeField] public List<Sprite> HatterSprite;


    public Manager1_2 Manager;

    public GameObject Player;
    public GameObject Enemy;

    private Transform PlayerChatBox;
    private Transform EnemyChatBox;

    private Image PlayerEmotion;
    private Image EnemyEmotion;

    private TextMeshProUGUI PlayerText;
    private TextMeshProUGUI EnemyText;


    void Start()
    {
        Manager = FindObjectOfType<Manager1_2>();

        CurrentScript = -1;

        PlayerEmotion = Player.transform.GetChild(0).GetComponent<Image>();
        EnemyEmotion = Enemy.transform.GetChild(0).GetComponent<Image>();

        PlayerChatBox = Player.transform.GetChild(1);
        EnemyChatBox = Enemy.transform.GetChild(1);

        PlayerText = PlayerChatBox.GetComponentInChildren<TextMeshProUGUI>();
        EnemyText = EnemyChatBox.GetComponentInChildren<TextMeshProUGUI>();

        PlayerChatBox.gameObject.SetActive(false);
        EnemyChatBox.gameObject.SetActive(false);

        ChangeToNextScript();
    }

    public void Update()
    {
        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            ChangeToNextScript();
        }
    }

    public void ChangeToNextScript()
    {
        if (CurrentScript == scripts.Count - 1)
        {
            StartCoroutine("PlayGameCoroutine");
            return;
        }

        CurrentScript = Mathf.Clamp(CurrentScript + 1, 0, scripts.Count);
        if (scripts[CurrentScript].isPlayer[0])
        {
            EnemyChatBox.gameObject.SetActive(false);
            EnemyEmotion.GetComponent<Image>().color = new Color(.7f, .7f, .7f);
            PlayerChatBox.gameObject.SetActive(true);
            PlayerEmotion.GetComponent<Image>().color = new Color(1, 1, 1);
            PlayerEmotion.sprite = PlayerSprite[(int)scripts[CurrentScript].PlayerEmotion];
            PlayerText.text = scripts[CurrentScript].script;
        }
        else if (scripts[CurrentScript].isPlayer[1])
        {
            PlayerChatBox.gameObject.SetActive(false);
            PlayerEmotion.GetComponent<Image>().color = new Color(.7f, .7f, .7f);
            EnemyChatBox.gameObject.SetActive(true);
            EnemyEmotion.GetComponent<Image>().color = new Color(1, 1, 1);
            EnemyEmotion.sprite = MarchSprite[(int)scripts[CurrentScript].MarchEmotion];
            EnemyText.text = scripts[CurrentScript].script;
        }
        else
        {
            PlayerChatBox.gameObject.SetActive(false);
            PlayerEmotion.GetComponent<Image>().color = new Color(.7f, .7f, .7f);
            EnemyChatBox.gameObject.SetActive(true);
            EnemyEmotion.GetComponent<Image>().color = new Color(1, 1, 1);
            EnemyEmotion.sprite = HatterSprite[(int)scripts[CurrentScript].HatterEmotion];
            EnemyText.text = scripts[CurrentScript].script;
        }
    }

    IEnumerator PlayGameCoroutine()
    {
        PlayerChatBox.gameObject.SetActive(false);
        EnemyChatBox.gameObject.SetActive(false);
        GetComponent<Animator>().Play("Start");
        yield return new WaitForSeconds(1);
        Manager.StartStage();
        this.gameObject.SetActive(false);
    }
}
