
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] float Speed;
    [SerializeField] float x;

    float time;
    void Update()
    {
        this.transform.Translate(new Vector3(-Speed * Time.deltaTime, 0));
        if(this.transform.position.x <= -x)
        {
            this.transform.position = new Vector3(this.transform.position.x + x * 2, 0);
        }
    }
}
