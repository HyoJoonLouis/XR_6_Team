using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnceWeapon : BaseWeapon
{
    public GameObject flamingoObj;

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

    }

    // Flamingo
    public void AreaBullet()
    {
        ObjectPoolManager.SpawnObject(flamingoObj, new Vector3(), new Quaternion()).GetComponent<Flamingo>().Init(AttackPoint, player);
    }

    // Faketurtle
    public void GuardBullet()
    {

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
