using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondMovement : AIMovement
{
    [Header("Diamond")]
    [SerializeField] GameObject AmmoGameObject;
    [SerializeField] float AmmoStartTime;
    [SerializeField] float DelayTime;

    [SerializeField] Transform AmmoStartPosition;

    Animator animator;

    Coroutine coroutine;

    public override void OnEnable()
    {
        base.OnEnable();
        animator = GetComponent<Animator>();
        animator.Play("Walk");
        coroutine = null;
    }

    public override void Update()
    {
        base.Update();
        if (time >= AmmoStartTime && coroutine == null)
        {
            coroutine = StartCoroutine(StartHitAnimation());
        }
    }

    IEnumerator StartHitAnimation()
    {
        while(true)
        {
            animator.Play("Hit");
            yield return new WaitForSeconds(DelayTime);
        }
    }

    public void Hit()
    {
        ObjectPoolManager.SpawnObject(GameManager.instance.Ammos[0], AmmoStartPosition.position, Quaternion.Euler(new Vector3(0,0,180)));
    }
}
