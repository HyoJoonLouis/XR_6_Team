using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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

    public GameObject Setting;

    int CurrentScene;

    public AudioClip audioClip;
    private AudioSource audioSource;
    public AudioClip FlipClip;
    public GameObject Fin;
    public GameObject Fffff;
    public Animator RealFin;


    public GameObject CImage;
    public GameObject SImage;
    public GameObject QImage;

    public void OnSettingClicked()
    {
        CImage.SetActive(false);
        SImage.SetActive(false);
        QImage.SetActive(false);
        Open.gameObject.SetActive(true);
        Open.Play("Open");
        Invoke("OpenSetting", 0.5f);
        Invoke("OpenFalse", 0.5f);
    }
    public void OpenFalse()
    {
        Open.gameObject.SetActive(false);
    }

    public void OnSettingClicked2()
    {
        CImage.SetActive(false);
        SImage.SetActive(false);
        QImage.SetActive(false);
        Flip.gameObject.SetActive(true);
        Flip.Play("Flip");
        Invoke("CloseFlip", 0.3f);
        Book.SetActive(false);
        Invoke("OpenSetting", 0.1f);
    }

    public void Return()
    {
        Book.SetActive(true);
        Flip.gameObject.SetActive(true);
        Flip.Play("Flip");
        Invoke("CloseFlip", 0.3f);
        Invoke("CloseSetting", 0.1f);
    }

    public void OpenSetting()
    {
        Setting.SetActive(true);
    }

    public void CloseSetting()
    {
        Setting.SetActive(false);
    }

    public void Start()
    {
        CurrentScene = -1;
        for(int i = 0; i< Scene.Count; i++)
        {
            if (GameManager.instance.SceneCompleted[i])
                Scene[i].isCompleted = true;
        }

        if(GameManager.instance.CurrentScene != -1)
        {
            BookCover.SetActive(false);
            Background.SetActive(true);
            Book.SetActive(true);
            CurrentScene = GameManager.instance.CurrentScene - 1;
            OnRightButtonClicked();
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void ChapterEnterClicked()
    {
        if (!Scene[CurrentScene - 1].isCompleted)
            return;
        SceneManager.LoadScene(CurrentScene);
    }

    public void OnRightButtonClicked()
    {
        if (CurrentScene == 5 && !GameManager.instance.SceneCompleted[5])
            return;

        else if (CurrentScene == 5 && GameManager.instance.SceneCompleted[5])
        {
            Flip.gameObject.SetActive(true);
            Flip.Play("Flip");
            Invoke("CloseFlip", 0.3f);
            Fin.SetActive(true);
            CurrentScene++;
            return;
        }
        if (CurrentScene == 6)
        {
            Fffff.SetActive(true);
            RealFin.Play("End");
        }

        Flip.gameObject.SetActive(true);
        Flip.Play("Flip");
        Invoke("CloseFlip", 0.3f);

        CurrentScene = Mathf.Clamp(CurrentScene + 1, 0, Scene.Count);

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

        audioSource.PlayOneShot(FlipClip);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnLeftButtonClicked()
    {
        Fin.SetActive(false);
        if (CurrentScene == 0)
            return;
        Flip.gameObject.SetActive(true);
        Flip.Play("Back");
        Invoke("CloseFlip", 0.3f);
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


        audioSource.PlayOneShot(FlipClip);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnChapterButtonClicked()
    {

        audioSource.PlayOneShot(audioClip);
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
        CurrentScene = 0;
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

        audioSource.PlayOneShot(FlipClip);
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForSeconds(1);
        Open.gameObject.SetActive(false);
    }

    public void CloseFlip()
    {
        Flip.gameObject.SetActive(false);
    }
}
