using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[Serializable]
public enum LEmotion
{
    angry = 0,
    annoying,
    happy,
    hasitate,
    nervous,
    normal,
    normal2,
    smile
}

[Serializable]
public enum RabbitEmotion
{
    angry = 0,
    doubt,
    nervous,
    normal
}

[Serializable]
public class Script
{
    public bool isPlayer;
    public LEmotion PlayerEmotion;
    public RabbitEmotion RabbitEmotion;
    public string script;
}

public class ChatScript : MonoBehaviour
{
    public List<Script> scripts;
    public int CurrentScript;

    [SerializeField] public List<Sprite> PlayerSprite;
    [SerializeField] public List<Sprite> EnemySprite;


    public Manager_1_1 Manager;

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
        Manager = FindObjectOfType<Manager_1_1>();

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
        if(Keyboard.current.zKey.wasPressedThisFrame)
        {
            ChangeToNextScript();
        }
    }

    public void ChangeToNextScript()
    {
        if(CurrentScript == scripts.Count - 1)
        {
            StartCoroutine("PlayGameCoroutine");
            return;
        }

        CurrentScript = Mathf.Clamp(CurrentScript + 1, 0, scripts.Count);
        if (scripts[CurrentScript].isPlayer)
        {
            EnemyChatBox.gameObject.SetActive(false);
            EnemyEmotion.GetComponent<Image>().color = new Color(.7f, .7f, .7f);
            PlayerChatBox.gameObject.SetActive(true);
            PlayerEmotion.GetComponent<Image>().color = new Color(1, 1, 1);
            PlayerEmotion.sprite = PlayerSprite[(int)scripts[CurrentScript].PlayerEmotion];
            PlayerText.text = scripts[CurrentScript].script;
        }
        else
        {
            PlayerChatBox.gameObject.SetActive(false);
            PlayerEmotion.GetComponent<Image>().color = new Color(.7f, .7f, .7f);
            EnemyChatBox.gameObject.SetActive(true);
            EnemyEmotion.GetComponent<Image>().color = new Color(1, 1, 1);
            EnemyEmotion.sprite = EnemySprite[(int)scripts[CurrentScript].RabbitEmotion];
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
