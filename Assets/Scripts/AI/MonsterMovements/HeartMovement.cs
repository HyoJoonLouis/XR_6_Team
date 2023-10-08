using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartMovement : AIMovement
{
    [Header("Heart")]
    [SerializeField] float RushStartTime;
    Transform TargetTransform;

    public override void OnEnable()
    {
        base.OnEnable();
        TargetTransform = ((Player)FindObjectOfType(typeof(Player))).transform;
    }
}
