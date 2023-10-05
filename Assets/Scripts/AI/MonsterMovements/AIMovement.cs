using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    [Header("Base Movement")]
    [SerializeField] AnimationCurve XMovement;
    [SerializeField] AnimationCurve YMovement;

    public float time = 0;

    public Vector2 initPosition;

    virtual public void OnEnable()
    {
        initPosition = transform.position;
        time = 0;
    }

    virtual public void Update()
    {
        time += Time.deltaTime;
        
        this.transform.position = new Vector2(XMovement.Evaluate(time) + initPosition.x, YMovement.Evaluate(time) + initPosition.y);
        if (this.transform.position.x <= -13)
            ObjectPoolManager.ReturnObjectToPool(this.gameObject);
    }

}
