using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHit : MonoBehaviour, ITakeDamage
{
    [SerializeField] float MaxHp;
    [SerializeField] float currentHp;

    [Header("Renderer")]
    [SerializeField] Renderer renderer;
    [SerializeField] float time;

    [Header("Effect")]
    [SerializeField] GameObject DieEffect;

    [Header("Items")]
    [SerializeField] float DropPercent;
    [SerializeField] List<GameObject> items;

    [Header("Boss")]
    [SerializeField] bool GameOverOnDied;
    bool first = false;
    int i = 0;

    [Header("Sound")]
    [SerializeField] AudioClip[] audioClips;
    private AudioSource audioSource;

    private void OnEnable()
    {
        currentHp = MaxHp;
        renderer = GetComponent<Renderer>();
        renderer.material.SetFloat("_Lerp", 0);
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(float value)
    {
        currentHp -= value;
        StartCoroutine(ChangeRenderCoroutine());
        audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);

        if (!first)
            i++;

        if (i > 5 && !first)
        {
            first = true;
            GameObject.FindObjectOfType<Tutorial>().StartChat();
        }
    }

    public void hit()
    {
        GameObject.FindObjectOfType<Tutorial>().StartChat();
    }

    IEnumerator ChangeRenderCoroutine()
    {
        renderer.material.SetFloat("_Lerp", 0.7f);
        yield return new WaitForSeconds(time);
        renderer.material.SetFloat("_Lerp", 0);

    }

}
