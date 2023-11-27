using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class StartSceneClass
{
    public string Chapter;
    public string KoreanChapter;
    public bool isCompleted;
    public string ExaplainText;
    public Sprite RightSprite;
    public Sprite CompleteSprite;
}


public class StartSceneUI : MonoBehaviour
{

    [SerializeField] public List<StartSceneClass> Scene;

    public TextMeshProUGUI ChapterText;
    public TextMeshProUGUI KoreanText;
    public TextMeshProUGUI ExplainText;
    public Image RightImage;
    public Button RightButton;

    public GameObject BookCover;
    public GameObject Book;
    public GameObject Stamp;

    public Animator Open;
    public Animator Flip;

    int CurrentScene;
    public void Start()
    {
        CurrentScene = -1;

    }
    public void ChapterEnterClicked()
    {
        SceneManager.LoadScene(CurrentScene + 1);
    }

    public void OnRightButtonClicked()
    {
        Flip.gameObject.SetActive(true);
        Flip.Play("Flip");
        Invoke("CloseFlip", 0.3f);
        CurrentScene = Mathf.Clamp(CurrentScene + 1, 0, Scene.Count);
        ChapterText.text = Scene[CurrentScene].Chapter;
        KoreanText.text = Scene[CurrentScene].KoreanChapter;
        ExplainText.text = Scene[CurrentScene].ExaplainText;
        if (!Scene[CurrentScene].isCompleted)
        {
            RightImage.sprite = Scene[CurrentScene].RightSprite;
            Stamp.SetActive(false);
        }
        else
        {
            RightImage.sprite = Scene[CurrentScene].CompleteSprite;
            Stamp.SetActive(true);
        }
    }
    public void OnLeftButtonClicked()
    {
        CurrentScene = Mathf.Clamp(CurrentScene - 1, 0, Scene.Count);
        ChapterText.text = Scene[CurrentScene].Chapter;
        KoreanText.text = Scene[CurrentScene].KoreanChapter;
        ExplainText.text = Scene[CurrentScene].ExaplainText;
        if (!Scene[CurrentScene].isCompleted)
        {
            RightImage.sprite = Scene[CurrentScene].RightSprite;
            Stamp.SetActive(false);
            
        }
        else
        {
            RightImage.sprite = Scene[CurrentScene].CompleteSprite;
            Stamp.SetActive(true);
        }
    }

    public void OnChapterButtonClicked()
    {
        StartCoroutine(OnChapterButtonClickedCoroutine());
    }

    IEnumerator OnChapterButtonClickedCoroutine()
    {
        BookCover.SetActive(false);
        Open.gameObject.SetActive(true);
        Open.Play("Open");
        yield return new WaitForSeconds(0.5f);
        Book.SetActive(true);
        OnRightButtonClicked();
        yield return new WaitForSeconds(1);
        Open.gameObject.SetActive(false);
    }

    public void CloseFlip()
    {
        Flip.gameObject.SetActive(false);
    }
}
