using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondMovement : AIMovement
{
    [Header("Diamond")]
    [SerializeField] GameObject AmmoGameObject;
    [SerializeField] float AmmoStartTime;
    [SerializeField] float DelayTime;

    Coroutine coroutine;
    public override void Update()
    {
        base.Update();
        if (time >= AmmoStartTime && coroutine == null)
        {
            coroutine = StartCoroutine(SpawnAmmo());
        }
    }

    IEnumerator SpawnAmmo()
    {
        while (true)
        {
            yield return new WaitForSeconds(DelayTime);
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
        }
    }
}
