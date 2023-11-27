using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public enum AliceEmotion
{
    angry,
    normal,
    panic
}

[Serializable]
public class Script1_3
{
    public List<bool> isPlayer;
    public LEmotion PlayerEmotion;
    public string PlayerName;
    public RabbitEmotion RabbitEmotion;
    public string RabbitName;
    public AliceEmotion AliceEmotion;
    public string AliceName;
    public string script;
}
public class ChatScript1_3 : MonoBehaviour
{
    public List<Script1_3> scripts;
    public int CurrentScript;

    [SerializeField] public List<Sprite> PlayerSprite;
    [SerializeField] public List<Sprite> RabbitSprite;
    [SerializeField] public List<Sprite> AliceSprite;


    public AliceMovement Manager;

    public GameObject Player;
    public GameObject Enemy;

    private Transform PlayerChatBox;
    private Transform EnemyChatBox;

    private Image PlayerEmotion;
    private Image EnemyEmotion;

    private TextMeshProUGUI PlayerText;
    private TextMeshProUGUI EnemyText;

    private TextMeshProUGUI PlayerName;
    private TextMeshProUGUI EnemyName;


    void Start()
    {
        Manager = FindObjectOfType<AliceMovement>();

        CurrentScript = -1;

        PlayerEmotion = Player.transform.GetChild(0).GetComponent<Image>();
        EnemyEmotion = Enemy.transform.GetChild(0).GetComponent<Image>();

        PlayerChatBox = Player.transform.GetChild(1);
        EnemyChatBox = Enemy.transform.GetChild(1);

        PlayerName = PlayerChatBox.GetChild(2).GetComponent<TextMeshProUGUI>();
        EnemyName = EnemyChatBox.GetChild(2).GetComponent <TextMeshProUGUI>();

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
            PlayerText.text = scripts[CurrentScript].script.Replace("\\n", "\n");
            PlayerName.text = scripts[CurrentScript].PlayerName;
        }
        else if (scripts[CurrentScript].isPlayer[1])
        {
            PlayerChatBox.gameObject.SetActive(false);
            PlayerEmotion.GetComponent<Image>().color = new Color(.7f, .7f, .7f);
            EnemyChatBox.gameObject.SetActive(true);
            EnemyEmotion.GetComponent<Image>().color = new Color(1, 1, 1);
            EnemyEmotion.sprite = RabbitSprite[(int)scripts[CurrentScript].RabbitEmotion];
            EnemyText.text = scripts[CurrentScript].script.Replace("\\n", "\n");
            EnemyName.text = scripts[CurrentScript].RabbitName;
        }
        else
        {
            PlayerChatBox.gameObject.SetActive(false);
            PlayerEmotion.GetComponent<Image>().color = new Color(.7f, .7f, .7f);
            EnemyChatBox.gameObject.SetActive(true);
            EnemyEmotion.GetComponent<Image>().color = new Color(1, 1, 1);
            EnemyEmotion.sprite = AliceSprite[(int)scripts[CurrentScript].AliceEmotion];
            EnemyText.text = scripts[CurrentScript].script.Replace("\\n", "\n");
            EnemyName.text = scripts[CurrentScript].AliceName;
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
