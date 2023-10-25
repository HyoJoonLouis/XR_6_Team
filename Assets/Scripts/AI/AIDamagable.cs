using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDamagable : MonoBehaviour, ITakeDamage
{
    [SerializeField] float MaxHp;
    float currentHp;

    [Header("Renderer")]
    [SerializeField] Renderer renderer;
    [SerializeField] float time;

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
            ObjectPoolManager.ReturnObjectToPool(this.gameObject);
        }
    }

    IEnumerator ChangeRenderCoroutine()
    {
        renderer.material.SetFloat("_Lerp", 0.7f);
        yield return new WaitForSeconds(time);
        renderer.material.SetFloat("_Lerp", 0);

    }

}
