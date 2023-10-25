using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatter : AIMovement
{
    [Header("Hatter")]
    [SerializeField] GameObject AmmoGameObject;
    [SerializeField] float AmmoStartTime;
    [SerializeField] float DelayTime;

    Coroutine coroutine;
    // Update is called once per frame
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
        while (true)
        {
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 0));
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 45));
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 90));
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 135));
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 180));
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 225));
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 270));
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 315));
            DelayTime = Random.Range(0.4f, 0.6f);
            yield return new WaitForSeconds(DelayTime);
            this.transform.Rotate(new Vector3(0, 0, 13));
        }
    }
}
