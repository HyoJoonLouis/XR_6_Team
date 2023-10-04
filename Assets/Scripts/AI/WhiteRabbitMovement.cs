using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteRabbitMovement : AIMovement
{
    [Header("White Rabbit")]
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
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 0));
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 30));
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 60));
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 90));
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 120));
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 150));
            yield return new WaitForSeconds(5f);
            this.transform.Rotate(new Vector3(0, 0, 20));
        }
    }
}
