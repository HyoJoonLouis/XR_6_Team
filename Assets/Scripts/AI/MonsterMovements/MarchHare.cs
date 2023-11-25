using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class MarchHare : AIMovement
{
    [Header("March Hare")]
    [SerializeField] Transform TargetTransform;
    [SerializeField] GameObject AmmoGameObject;
    [SerializeField] float AmmoStartTime;
    [SerializeField] float DelayTime;

    Animator animator;
    Coroutine coroutine;

    public override void Update()
    {
        base.Update();
        if(time >= AmmoStartTime && coroutine == null)
        {
            coroutine = StartCoroutine(SpawnAmmo());
        }
    }

    IEnumerator SpawnAmmo()
    {
        animator = GetComponent<Animator>();
        isMoveable = false;
        yield return new WaitForSeconds(DelayTime);
        animator.Play("Attack");
        yield return new WaitForSeconds(2.0f);
        while (true)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(this.transform.position.x, 10), 7 * Time.deltaTime);
            if (Vector2.Distance(this.transform.position, new Vector2(this.transform.position.x, 10)) < 0.1f)
                break;
            yield return null;
            //ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.(TargetTransform.position - this.transform.position))
        }
        yield return new WaitForSeconds(2.0f);
        ObjectPoolManager.SpawnObject(GameManager.instance.Ammos[9], new Vector3(5.38f, -3.63f, 0), transform.rotation);
    }
}
