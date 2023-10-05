using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpadeMovement : AIMovement
{
    [Header("Spade")]
    [SerializeField] Transform TargetTransform;
    [SerializeField] GameObject AmmoGameObject;
    [SerializeField] float AmmoStartTime;
    [SerializeField] float DelayTime;

    Coroutine coroutine;

    public override void OnEnable()
    {
        base.OnEnable();
        TargetTransform = ((Player)FindObjectOfType(typeof(Player))).transform;
        coroutine = null;
    }

    public override void Update()
    {
        base.Update();
        if(time > AmmoStartTime && coroutine == null)
        {
            coroutine = StartCoroutine(SpawnAmmo());
        }
    }

    IEnumerator SpawnAmmo()
    {
        while (true)
        {
            Vector2 direction = new Vector2(transform.position.x - TargetTransform.position.x, transform.position.y - TargetTransform.position.y);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0,0, 180 + angle));
            yield return new WaitForSeconds(DelayTime);
        }
    }
}
