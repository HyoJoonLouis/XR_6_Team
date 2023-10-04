using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpadeMovement : AIMovement
{
    [Header("Spade")]
    [SerializeField] Transform TargetTransform;
    [SerializeField] GameObject AmmoGameObject;
    [SerializeField] float DelayTime;

    Coroutine coroutine;
    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if(time > DelayTime && coroutine == null)
        {
            coroutine = StartCoroutine(SpawnAmmo());
        }
    }

    IEnumerator SpawnAmmo()
    {
        while (true)
        {
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, transform.rotation);
            yield return new WaitForSeconds(1.0f);
        }
    }
}
