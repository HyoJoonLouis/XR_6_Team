using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : BaseWeapon
{
    protected Player player;
    bool isAttack = false;
    bool attackTime = true;
    public float time;
    Vector2 randVector;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponentInParent<Player>();
    }

    private void Update()
    {
        time++;
        if (time > 100f)
            time = 0;
        Random.InitState((int)time);

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
                new Vector2(player.transform.position.x + 1.5f, player.transform.position.y + Random.Range(-0.65f, 0.3f)), 
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
