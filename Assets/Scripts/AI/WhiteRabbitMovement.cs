using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteRabbitMovement : AIMovement
{
    [Header("White Rabbit")]
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
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 0));
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 60));
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 120));
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 180));
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 240));
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 300));
            yield return new WaitForSeconds(DelayTime);
            this.transform.Rotate(new Vector3(0, 0, 20));
        }
    }
}
