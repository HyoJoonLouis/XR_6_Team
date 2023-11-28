using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDamagable : MonoBehaviour, ITakeDamage
{
    private Player player;
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

        
        if(currentHp <= 0)
        {
            if(DieEffect != null) 
                ObjectPoolManager.SpawnObject(DieEffect, this.transform.position, this.transform.rotation);
            if(items.Count > 0)
            {
                float percent = Random.Range(0.0f, 1.0f);

                if(percent < DropPercent)
                {
                    ObjectPoolManager.SpawnObject(items[Random.Range(0, items.Count)],this.transform.position, this.transform.rotation);
                }
            }

            if(GameOverOnDied)
            {
                StartCoroutine(Died());
                return;
            }
            ObjectPoolManager.ReturnObjectToPool(this.gameObject);
        }
    }

    IEnumerator Died()
    {
        GetComponent<AIMovement>().isMoveable = false;
        GetComponent<Collider2D>().enabled = false;
        player = FindObjectOfType<Player>();
        player.CurrentHp = 100000;
        int i = 0;
        while(true)
        {
            i++;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, -15), 3 * Time.deltaTime);
            yield return null;
            if (i > 30)
            {
                i = 0;
                ObjectPoolManager.SpawnObject(DieEffect, transform.position + new Vector3(Random.RandomRange(0, 3), Random.RandomRange(0, 3),0), this.transform.rotation);
            }
            if(Vector2.Distance(transform.position, new Vector2(transform.position.x, -15)) < 0.1f)
                 break;
        }
        GameOver();
    }

    public void GameOver()
    {
        UIManager uiManager = FindObjectOfType<UIManager>();
        uiManager.GameClearCanvas.SetActive(true);
        Time.timeScale = 0;
    }
    IEnumerator ChangeRenderCoroutine()
    {
        renderer.material.SetFloat("_Lerp", 0.7f);
        yield return new WaitForSeconds(time);
        renderer.material.SetFloat("_Lerp", 0);

    }

}
