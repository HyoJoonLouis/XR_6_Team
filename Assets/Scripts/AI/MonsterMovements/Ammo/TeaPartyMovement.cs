using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaPartyMovement : MonoBehaviour
{
    [SerializeField] float Speed;

    public virtual void Update()
    {
        this.transform.Translate(new Vector3(0,Speed,0) * Time.deltaTime);

        if (this.transform.position.x < -13 || this.transform.position.x > 13 || this.transform.position.y > 7 || this.transform.position.y < -7)
            ObjectPoolManager.ReturnObjectToPool(this.gameObject);
    }
}
