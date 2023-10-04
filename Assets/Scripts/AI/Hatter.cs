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
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 22.5f));
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 45));
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 67.5f));
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 90));
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 112.5f));
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 137));
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 159.5f));
            DelayTime = Random.Range(0.4f, 0.6f);
            yield return new WaitForSeconds(DelayTime);
            this.transform.Rotate(new Vector3(0, 0, 13));
        }
    }
}
