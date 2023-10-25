using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAmmoMovement : AmmoMovement
{
    [HideInInspector] public Transform TargetTransform;
    float time;

    [SerializeField] float AmmoFollowStartTime;
    [SerializeField] float AmmoFollowEndTime;

    [SerializeField] float RotateSpeed;

    private void OnEnable()
    {
        time = 0;
    }

    public override void Update()
    {
        base.Update();
        time += Time.deltaTime;

        if(time >= AmmoFollowStartTime && time <= AmmoFollowEndTime)
        {
            Vector2 direction = new Vector2(transform.position.x - TargetTransform.transform.position.x, transform.position.y - TargetTransform.transform.position.y);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle + 180, Vector3.forward);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, RotateSpeed * Time.deltaTime);

            transform.rotation = rotation;
        }
    }

    public void Init(Transform target)
    {
        TargetTransform = target;
    }
}
