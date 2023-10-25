using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnceWeapon : BaseWeapon
{
    public enum WeaponType
    {
        None,
        Penetrate,
        Damege,
        Charging,
        Destroy,
        Heart,
    }

    public WeaponType type;
    public Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.gameObject.GetComponent<Player>();

        this.gameObject.transform.SetParent(collision.gameObject.transform);
    }
}
