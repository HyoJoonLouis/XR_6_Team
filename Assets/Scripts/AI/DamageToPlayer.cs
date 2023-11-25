using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToPlayer : MonoBehaviour
{
    [SerializeField] bool DestroyOnHit;
    [SerializeField] GameObject OnHitSpawnedEffect;
    [SerializeField] float Damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ITakeDamage player =  collision.GetComponent<ITakeDamage>();
        Debug.Log("hi");
        if(player != null)
            player.TakeDamage(Damage);

        if (OnHitSpawnedEffect)
            ObjectPoolManager.SpawnObject(OnHitSpawnedEffect, transform.position, transform.rotation);

        if(DestroyOnHit)
            ObjectPoolManager.ReturnObjectToPool(this.gameObject);
    }
}
