using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : BaseWeapon
{
    protected Player player;
    bool isAttack = false;
    bool attackTime = true;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponentInParent<Player>();
    }

    private void Start()
    {
        StartCoroutine(OnAttack());
    }

    private void Update()
    {
        CreateProjectile();
    }

    public IEnumerator OnAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(AttackSpeed);
    
            if (!attackTime) attackTime = true;
        }
    }

    void CreateProjectile()
    {
        if (isAttack && attackTime)
        {
            Fireball fire = ObjectPoolManager.SpawnObject(ProjectileObject, player.transform.position, player.transform.rotation).GetComponent<Fireball>();
            fire.Init(ProjectileSpeed, AttackPoint, MaxAttack);
            attackTime = false;
        }
    }

    public void OnShot(bool isAtk)
    {
        isAttack = isAtk;
    }
}
