using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[Serializable]
public enum HeartQueenEmotion
{
    normal,
    notbad,
    smile,
    thinking
}

[Serializable]
public class TutorialScript
{
    public HeartQueenEmotion QueenEmotion;
    public string script;
}

public class Tutorial : MonoBehaviour
{
    public List<TutorialScript> scripts;
    public int CurrentScript;

    [SerializeField] public List<Sprite> QueenSprite;

    public Manager1_2 Manager;

    public GameObject Player;

    private Transform PlayerChatBox;

    private Image PlayerEmotion;

    private TextMeshProUGUI PlayerText;

    private bool canChat;


    void Start()
    {
        Manager = FindObjectOfType<Manager1_2>();

        CurrentScript = -1;

        PlayerEmotion = Player.transform.GetChild(0).GetComponent<Image>();

        PlayerChatBox = Player.transform.GetChild(1);

        PlayerText = PlayerChatBox.GetComponentInChildren<TextMeshProUGUI>();

        PlayerChatBox.gameObject.SetActive(false);

        ChangeToNextScript();
    }
    public void Update()
    {
        if (Keyboard.current.zKey.wasPressedThisFrame && canChat)
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
        if (CurrentScript == 1 || CurrentScript == 3 || CurrentScript == 7 || CurrentScript == 8)
        {
            StopChat();
            return;
        }
        canChat = true;
        CurrentScript = Mathf.Clamp(CurrentScript + 1, 0, scripts.Count);
        PlayerChatBox.gameObject.SetActive(true);
        PlayerEmotion.GetComponent<Image>().color = new Color(1, 1, 1);
        PlayerEmotion.sprite = QueenSprite[(int)scripts[CurrentScript].QueenEmotion];
        PlayerText.text = scripts[CurrentScript].script.Replace("\\n", "\n");
    }

    public void StartChat()
    {
        canChat = true;
        CurrentScript = Mathf.Clamp(CurrentScript + 1, 0, scripts.Count);
        PlayerChatBox.gameObject.SetActive(true);
        PlayerEmotion.GetComponent<Image>().color = new Color(1, 1, 1);
        PlayerEmotion.sprite = QueenSprite[(int)scripts[CurrentScript].QueenEmotion];
        PlayerText.text = scripts[CurrentScript].script.Replace("\\n", "\n");
    }


    IEnumerator PlayGameCoroutine()
    {
        PlayerChatBox.gameObject.SetActive(false);
        GetComponent<Animator>().Play("Start");
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
        UIManager uiManager = GameObject.FindObjectOfType<UIManager>();
        uiManager.GameClearCanvas.SetActive(true);
    }

    void StopChat()
    {
        canChat = false;
        PlayerChatBox.gameObject.SetActive(false);
        PlayerChatBox.gameObject.SetActive(false);
        GetComponent<Animator>().Play("Start");
    }
}
