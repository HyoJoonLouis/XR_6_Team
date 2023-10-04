using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoMovement : MonoBehaviour{
    void Update()
    {
        this.transform.Translate(transform.right * Time.deltaTime);
    }
}
