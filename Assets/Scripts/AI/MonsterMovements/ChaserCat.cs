using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserCat : AIMovement
{
    [Header("Chaser Cat")]
    [SerializeField] float DelayTime;
    [SerializeField] GameObject SpawnEffect;

    CameraShake camera;

    public override void OnEnable()
    {
        base.OnEnable();
        ObjectPoolManager.SpawnObject(SpawnEffect, this.transform.position, this.transform.rotation);
        camera = FindObjectOfType<CameraShake>();
        camera.CameraReverse();
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
}
