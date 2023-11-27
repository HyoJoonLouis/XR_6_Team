using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : BaseWeapon
{
    Transform player;
    bool isAttack = false;
    bool attackTime = true;
    public float time;
    Vector2 randVector;

    protected override void Awake()
    {
        base.Awake();

        if (GetComponentInParent<Player>() == null)
            player = GetComponentInParent<TutorialPlayer>().transform;
        else
            player = GetComponentInParent<Player>().transform;
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
        yield return new WaitForSecondsRealtime(AttackSpeed);
    
        if (!attackTime) attackTime = true;
    }

    void CreateProjectile()
    {
        if (isAttack && attackTime)
        {
            Fireball fire = ObjectPoolManager.SpawnObject(ProjectileObject, 
                new Vector2(player.transform.position.x + 0.5f, player.transform.position.y), 
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
