using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDamagable : MonoBehaviour, ITakeDamage
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

    private void OnEnable()
    {
        currentHp = MaxHp;
        renderer = GetComponent<Renderer>();
        renderer.material.SetFloat("_Lerp", 0);
    }

    public void TakeDamage(float value)
    {
        currentHp -= value;
        StartCoroutine(ChangeRenderCoroutine());
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
            ObjectPoolManager.ReturnObjectToPool(this.gameObject);

            if(GameOverOnDied)
            {
                UIManager uiManager = FindObjectOfType<UIManager>();
                uiManager.GameClearCanvas.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    IEnumerator ChangeRenderCoroutine()
    {
        renderer.material.SetFloat("_Lerp", 0.7f);
        yield return new WaitForSeconds(time);
        renderer.material.SetFloat("_Lerp", 0);

    }

}
