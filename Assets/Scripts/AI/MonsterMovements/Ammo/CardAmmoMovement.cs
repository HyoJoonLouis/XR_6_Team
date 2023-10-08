using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAmmoMovement : AmmoMovement
{
    public bool isClockWise;

    [SerializeField] float RotateRate;

    [SerializeField] Sprite[] sprites;

    public void OnEnable()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
    }

    public override void Update()
    {
        base.Update();

        if (!isClockWise)
        {
            transform.Rotate(Vector3.forward * RotateRate * Time.deltaTime);
        }
        else
        {
            transform.Rotate(-Vector3.forward * RotateRate  * Time.deltaTime);
        }
    }
}
