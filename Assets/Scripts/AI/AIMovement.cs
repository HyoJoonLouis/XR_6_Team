using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    [Header("Base Movement")]
    [SerializeField] AnimationCurve XMovement;
    [SerializeField] AnimationCurve YMovement;

    protected float time = 0;

    protected Vector2 initPosition;

    virtual public void OnEnable()
    {
        initPosition = transform.position;
    }

    virtual public void Update()
    {
        time += Time.deltaTime;
        
        this.transform.position = new Vector2(XMovement.Evaluate(time) + initPosition.x, YMovement.Evaluate(time) + initPosition.y);

    }

}
