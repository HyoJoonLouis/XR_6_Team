using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnceWeapon : BaseWeapon
{
    [Header("Projectile Option")]
    public GameObject flamingoPrefab;
    public GameObject turtlePrefab;
    public GameObject hedgehogPrefab;

    Player player;

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

    Dictionary<WeaponType, int> itemInfo;

    private void Start()
    {
        player = GetComponentInParent<Player>();
        itemInfo = new Dictionary<WeaponType, int>();

        for (int i = 0; i < MaxOnceItem; ++i)
        {
            itemInfo.Add((WeaponType)i, 1);
        }
    }

    // Jabberwocky
    public void DamageBeam()
    {

    }

    // Hedgehog
    public void ThrowBullet()
    {
        player.SetIsUse(true);

        AttackPoint = 3;
        ProjectileSpeed = 20f;

        ObjectPoolManager.SpawnObject(hedgehogPrefab, new Vector3(), new Quaternion()).GetComponent<Hedgehog>().Init(player, AttackPoint, ProjectileSpeed);
    }

    // Flamingo
    public void AreaBullet()
    {
        AttackPoint = 10;

        ObjectPoolManager.SpawnObject(flamingoPrefab, new Vector3(), new Quaternion()).GetComponent<Flamingo>().Init(AttackPoint, player);
    }

    // Faketurtle
    public void GuardBullet()
    {
        player.SetIsUse(true);

        ObjectPoolManager.SpawnObject(turtlePrefab, new Vector3(), new Quaternion()).GetComponent<FakeTurtle>().Init(player, 0);
        ObjectPoolManager.SpawnObject(turtlePrefab, new Vector3(), new Quaternion()).GetComponent<FakeTurtle>().Init(player, 1);
        ObjectPoolManager.SpawnObject(turtlePrefab, new Vector3(), new Quaternion()).GetComponent<FakeTurtle>().Init(player, 2);
    }

    // Heart
    public void HeartUp()
    {
        player.SetHp(itemInfo[WeaponType.Heart]);
    }

    // Watch
    public void TimeStop()
    {

    }

    public void OnUse(int type)
    {
        WeaponType w = (WeaponType)type;
        switch (w)
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
            case WeaponType.Faketurtle:
                GuardBullet();
                break;
            case WeaponType.Heart:
                HeartUp();
                break;
            case WeaponType.Watch:
                TimeStop();
                break;
        }
        itemInfo[w] = 1;
    }

    public int GetLevel(int itemname)
    {
        int value;
        itemInfo.TryGetValue((WeaponType)itemname, out value);

        return value;
    }
    
    public void SetLevel(int itemname, int level, bool isPlus)
    {
        if (isPlus)
            itemInfo[(WeaponType)itemname] += level;
        else
            itemInfo[(WeaponType)itemname] = level;
    }
}
