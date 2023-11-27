using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    public WeaponType type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<OnceWeapon>().ItemToPlayer(type);
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
}
