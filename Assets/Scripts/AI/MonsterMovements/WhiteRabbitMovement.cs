using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteRabbitMovement : AIMovement
{
    [Header("White Rabbit")]
    [SerializeField] GameObject AmmoGameObject;
    [SerializeField] float AmmoStartTime;
    [SerializeField] float DelayTime;
    [SerializeField] Transform AmmoStartPosition;

    Coroutine coroutine;
    Animator animator;

    public override void OnEnable()
    {
        base.OnEnable();
        animator = GetComponent<Animator>();
        coroutine = null;
        AmmoStartPosition.eulerAngles= Vector3.zero;
    }

    public override void Update()
    {
        base.Update();
        if (time >= AmmoStartTime && coroutine == null)
        {
            coroutine = StartCoroutine(WhiteRabbitCoroutine());
        }
    }


    IEnumerator WhiteRabbitCoroutine()
    {
        animator.Play("AttackReady");
        yield return new WaitForSeconds(0.9f);
        while (true)
        {
            animator.Play("Attack");
            yield return new WaitForSeconds(DelayTime);
        }
    }

    public void SpawnAmmo()
    {
        ObjectPoolManager.SpawnObject(GameManager.instance.Ammos[2], AmmoStartPosition.position, Quaternion.Euler(0, 0, AmmoStartPosition.eulerAngles.z + 0));
        ObjectPoolManager.SpawnObject(GameManager.instance.Ammos[2], AmmoStartPosition.position, Quaternion.Euler(0, 0, AmmoStartPosition.eulerAngles.z + 60));
        ObjectPoolManager.SpawnObject(GameManager.instance.Ammos[2], AmmoStartPosition.position, Quaternion.Euler(0, 0, AmmoStartPosition.eulerAngles.z + 120));
        ObjectPoolManager.SpawnObject(GameManager.instance.Ammos[2], AmmoStartPosition.position, Quaternion.Euler(0, 0, AmmoStartPosition.eulerAngles.z + 180));
        ObjectPoolManager.SpawnObject(GameManager.instance.Ammos[2], AmmoStartPosition.position, Quaternion.Euler(0, 0, AmmoStartPosition.eulerAngles.z + 240));
        ObjectPoolManager.SpawnObject(GameManager.instance.Ammos[2], AmmoStartPosition.position, Quaternion.Euler(0, 0, AmmoStartPosition.eulerAngles.z + 300));
        AmmoStartPosition.Rotate(new Vector3(0, 0, 20));
    }
}
