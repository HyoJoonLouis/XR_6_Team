using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpadeMovement : DiamondMovement
{
    [Header("Spade")]
    [SerializeField] Transform TargetTransform;

    public override void OnEnable()
    {
        base.OnEnable();
        TargetTransform = ((Player)FindObjectOfType(typeof(Player))).transform;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Hit()
    {
        ObjectPoolManager.SpawnObject(GameManager.instance.Ammos[1], AmmoStartPosition.position, Quaternion.Euler(0,0,180)).GetComponent<FollowAmmoMovement>().Init(TargetTransform);
    }
}
