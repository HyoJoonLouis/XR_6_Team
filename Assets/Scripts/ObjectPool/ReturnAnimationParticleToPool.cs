using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnAnimationParticleToPool : MonoBehaviour
{
    Animator animator; 
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        animator.Play("Start");
    }

    public void ReturnToPool()
    {
        ObjectPoolManager.ReturnObjectToPool(this.gameObject);
    }
}
