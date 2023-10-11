using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] AnimationCurve yCurveUp;
    public GameObject player;
    float time = 0;
    bool isShaking = false;

    [Header("Shaking")]
    [SerializeField] float shakeAmount;
    float shakeTime;
    Vector3 initialPosition;

    void Update()
    {
        if (!isShaking)
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
            if (time >= -1)
                time -= Time.deltaTime;
            this.transform.position = new Vector3(0, yCurveUp.Evaluate(time), -10);
        }
    }

    public void VibrateForTime(float times)
    {
        isShaking = true;
        shakeTime = times;
        initialPosition = new Vector3(transform.position.x, transform.position.y, -10);
        StartCoroutine(CameraShaking());
    }

    IEnumerator CameraShaking()
    {
        while (isShaking)
        {
            if (shakeTime > 0)
            {
                transform.position = Random.insideUnitSphere * shakeAmount + initialPosition;
                shakeTime -= Time.deltaTime;
            }
            else
            {
                shakeTime = 0;
                transform.position = initialPosition;
                isShaking = false;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
