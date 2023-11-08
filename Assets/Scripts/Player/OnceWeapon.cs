using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnceWeapon : BaseWeapon
{
    public enum WeaponType
    {
        Jabberwocky,
        Hedgehog,
        Flamingo,
        Heart,
        Watch,
    }

    public WeaponType type;
    Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.gameObject.GetComponent<Player>();
        ObjectPoolManager.ReturnObjectToPool(gameObject);
        if (player.GetOnceWeaponCount() >= player.MaxOnceWeaponCount - 1)
            return;

        player.AddOnceWeapon(gameObject);

    }

    // Jabberwocky
    public void DamageBeam()
    {

    }

    // Hedgehog
    public void ThrowBullet()
    {

    }

    // Flamingo
    public void AreaBullet()
    {

    }

    // Heart
    public void HeartUp()
    {
        player.SetHp(level);
    }

    // Watch
    public void TimeStop()
    {

    }

    public void OnUse()
    {
        switch (type)
        {
            case WeaponType.Jabberwocky:
                DamageBeam();
                break;
            case WeaponType.Hedgehog:
                ThrowBullet();
                break;
            case WeaponType.Flamingo:
                AreaBullet();
                break;
            case WeaponType.Heart:
                HeartUp();
                break;
            case WeaponType.Watch:
                TimeStop();
                break;
        }
    }
}
