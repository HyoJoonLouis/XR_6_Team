using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToPlayer : MonoBehaviour
{
    [SerializeField] float Damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ITakeDamage player =  collision.GetComponent<ITakeDamage>();
        if(player != null)
        {
            player.TakeDamage(Damage);
        }
    }
}
