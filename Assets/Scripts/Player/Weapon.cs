using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : BaseWeapon
{
    protected Player player;
    bool isAttack = true;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponentInParent<Player>();
    }

    private void Start()
    {
        StartCoroutine(OnAttack());
    }

    public IEnumerator OnAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(AttackSpeed);

            if (!isAttack) isAttack = true;
        }
    }

    void CreateProjectile()
    {
        Fireball fire = ObjectPoolManager.SpawnObject(ProjectileObject, player.transform.position, player.transform.rotation).GetComponent<Fireball>();
        fire.Init(ProjectileSpeed, AttackPoint, MaxAttack);
        isAttack = false;
    }

    public void OnShot()
    {
        if (isAttack)
        {
            CreateProjectile();
        }
    }
}
