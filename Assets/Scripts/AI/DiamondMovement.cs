using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondMovement : AIMovement
{
    [Header("Diamond")]
    [SerializeField] GameObject AmmoGameObject;
    [SerializeField] float DelayTime;

    Coroutine coroutine;
    public override void Update()
    {
        base.Update();
        if (time >= DelayTime && coroutine == null)
        {
            coroutine = StartCoroutine(SpawnAmmo());
        }
    }

    IEnumerator SpawnAmmo()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, transform.rotation);
        }
    }
}
