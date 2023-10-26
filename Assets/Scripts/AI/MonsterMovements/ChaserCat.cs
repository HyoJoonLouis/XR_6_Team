using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserCat : AIMovement
{
    [Header("Chaser Cat")]
    [SerializeField] float DelayTime;
    [SerializeField] GameObject SpawnEffect;
    [SerializeField] GameObject AttackEffect;

    CameraShake camera;

    public override void OnEnable()
    {
        base.OnEnable();
        ObjectPoolManager.SpawnObject(SpawnEffect, this.transform.position, this.transform.rotation);
        camera = FindObjectOfType<CameraShake>();
        StartCoroutine(ChaserCatAttckCoroutine());
    }

    private void OnDisable()
    {
        camera.CameraRestore();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if(time >= DelayTime)
        {
        }
    }

    IEnumerator ChaserCatAttckCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        ObjectPoolManager.SpawnObject(AttackEffect, Vector3.zero, Quaternion.identity);
        yield return new WaitForSeconds(1.02f);
        camera.CameraReverse();
    }
}
