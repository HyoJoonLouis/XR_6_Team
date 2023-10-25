using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserCat : AIMovement
{
    [Header("Chaser Cat")]
    [SerializeField] float DelayTime;
    [SerializeField] GameObject SpawnEffect;

    public override void OnEnable()
    {
        base.OnEnable();
        ObjectPoolManager.SpawnObject(SpawnEffect, this.transform.position, this.transform.rotation);
    }

    private void OnDisable()
    {
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
