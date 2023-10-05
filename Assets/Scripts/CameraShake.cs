using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] AnimationCurve yCurveUp;
    [SerializeField] AnimationCurve yCurveDown;
    public GameObject player;
    float time = 0;

    void Update()
    {
        if (player.transform.position.y > 1.5f)
        {
            CameraUp();
        }
        else if (player.transform.position.y < -1.5f)
        {
            CameraDown();
        }
    }

    void CameraUp()
    {
        if (this.transform.position.y <= 0.4f)
        {
            if (time <= 1)
                time += Time.deltaTime;
            this.transform.position = new Vector3(0, yCurveUp.Evaluate(time), -10);
        }
    }

    void CameraDown()
    {
        if (this.transform.position.y >= -0.4f)
        {
            if (time >= 0)
                time -= Time.deltaTime;
            this.transform.position = new Vector3(0, yCurveUp.Evaluate(time), -10);
        }
    }
}
