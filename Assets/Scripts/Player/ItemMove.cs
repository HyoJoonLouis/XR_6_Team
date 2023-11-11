using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    public enum WeaponType
    {
        Jabberwocky,
        Hedgehog,
        Flamingo,
        Faketurtle,
        Heart,
        Watch,
        Key,
    }

    public WeaponType type;
    Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.gameObject.GetComponent<Player>();
        player.AddOnceWeapon((int)this.type);
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
}
