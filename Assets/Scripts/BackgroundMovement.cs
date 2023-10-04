
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] float Speed;

    float time;
    void Update()
    {
        this.transform.Translate(new Vector3(-Speed * Time.deltaTime, 0));
        if(this.transform.position.x <= -28.8f)
        {
            this.transform.position = new Vector3(this.transform.position.x + 57.6f, 0);
        }
    }
}
