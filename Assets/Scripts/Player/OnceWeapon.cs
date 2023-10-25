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
        Watch,
    }

    public WeaponType type;
    public Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.gameObject.GetComponent<Player>();
        if (player.GetOnceWeaponCount() >= player.MaxOnceWeaponCount - 1)
            return;

        // Destory
    }
}
