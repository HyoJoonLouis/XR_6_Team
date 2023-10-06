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

    private void Update()
    {
        CreateProjectile();
    }

    public IEnumerator OnAttack()
    {
        yield return new WaitForSeconds(AttackSpeed);
    
        if (!attackTime) attackTime = true;
    }

    void CreateProjectile()
    {
        if (isAttack && attackTime)
        {
            Fireball fire = ObjectPoolManager.SpawnObject(ProjectileObject, 
                new Vector2(player.transform.position.x + 0.9f, player.transform.position.y - 0.3f), 
                player.transform.rotation).GetComponent<Fireball>();
            fire.Init(ProjectileSpeed, AttackPoint, MaxAttack);
            StartCoroutine(OnAttack());
            attackTime = false;
        }
    }

    public void OnShot(bool isAtk)
    {
        isAttack = isAtk;
    }
}
