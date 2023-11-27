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
    public string CompleteText;
    public Sprite RightSprite;
    public Sprite CompleteSprite;
}


public class StartSceneUI : MonoBehaviour
{
    [SerializeField] public List<StartSceneClass> Scene;

    public TextMeshProUGUI ChapterText;
    public TextMeshProUGUI KoreanText;
    public TextMeshProUGUI ExplainText;
    public TextMeshProUGUI CompleteText;
    public Image RightImage;
    public Button RightButton;
    public GameObject Background;

    public GameObject BookCover;
    public GameObject Book;
    public GameObject Stamp;
    public GameObject CompleteLine;

    public Animator Open;
    public Animator Flip;

    public GameObject StartButton;

    int CurrentScene;
    public void Start()
    {
        CurrentScene = -1;
        for(int i = 0; i< Scene.Count; i++)
        {
            if (GameManager.instance.SceneCompleted[i])
                Scene[i].isCompleted = true;
        }
    }

    public void ChapterEnterClicked()
    {
        if (!Scene[CurrentScene - 1].isCompleted)
            return;
        SceneManager.LoadScene(CurrentScene);
    }

    public void OnRightButtonClicked()
    {
        Flip.gameObject.SetActive(true);
        Flip.Play("Flip");
        Invoke("CloseFlip", 0.3f);

        CurrentScene = Mathf.Clamp(CurrentScene + 1, 0, Scene.Count - 1);

        ChapterText.text = Scene[CurrentScene].Chapter;
        KoreanText.text = Scene[CurrentScene].KoreanChapter;
        ExplainText.text = Scene[CurrentScene].ExaplainText.Replace("\\n", "\n");
        if (!Scene[CurrentScene].isCompleted)
        {
            CompleteLine.SetActive(false);
            CompleteText.gameObject.SetActive(false);
            RightImage.sprite = Scene[CurrentScene].RightSprite;
            Stamp.SetActive(false);
        }
        else
        {
            CompleteLine.SetActive(true);
            CompleteText.gameObject.SetActive(true);
            CompleteText.text = Scene[CurrentScene].CompleteText.Replace("\\n", "\n");
            RightImage.sprite = Scene[CurrentScene].CompleteSprite;
            Stamp.SetActive(true);
        }

        if (CurrentScene == 0 || CurrentScene == 5)
        {
            StartButton.SetActive(false);
            CompleteLine.SetActive(false);
            CompleteText.gameObject.SetActive(false);
        }
        else
            StartButton.SetActive(true);

    }
    public void OnLeftButtonClicked()
    {
        CurrentScene = Mathf.Clamp(CurrentScene - 1, 0, Scene.Count - 1);
        

        ChapterText.text = Scene[CurrentScene].Chapter;
        KoreanText.text = Scene[CurrentScene].KoreanChapter;
        ExplainText.text = Scene[CurrentScene].ExaplainText.Replace("\\n", "\n");
        if (!Scene[CurrentScene].isCompleted)
        {
            CompleteLine.SetActive(false);
            CompleteText.gameObject.SetActive(false);
            RightImage.sprite = Scene[CurrentScene].RightSprite;
            Stamp.SetActive(false);
        }
        else
        {
            CompleteLine.SetActive(true);
            CompleteText.gameObject.SetActive(true);
            CompleteText.text = Scene[CurrentScene].CompleteText.Replace("\\n", "\n");
            RightImage.sprite = Scene[CurrentScene].CompleteSprite;
            Stamp.SetActive(true);
        }

        if (CurrentScene == 0 || CurrentScene == 5)
        {
            StartButton.SetActive(false);
            CompleteLine.SetActive(false);
            CompleteText.gameObject.SetActive(false);
        }
        else
            StartButton.SetActive(true);
    }

    public void OnChapterButtonClicked()
    {
        StartCoroutine(OnChapterButtonClickedCoroutine());
    }

    IEnumerator OnChapterButtonClickedCoroutine()
    {
        Background.SetActive(true);
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
