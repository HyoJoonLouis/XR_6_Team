using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserCat : AIMovement
{
    [Header("Chaser Cat")]
    [SerializeField] Camera cam;
    [SerializeField] float DelayTime;
    bool isUpsidedown = false;

    public override void OnEnable()
    {
        base.OnEnable();
        cam = Camera.main;
    }

    private void OnDisable()
    {
        isUpsidedown = false;
        cam.transform.position = new Vector3(0, 0, -10);
        cam.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if(time >= DelayTime && isUpsidedown == false)
        {
            isUpsidedown = true;
            cam.transform.position = new Vector3(0, 0, 10);
            cam.transform.rotation = Quaternion.Euler(180, 0, 0);
        }
    }
}
