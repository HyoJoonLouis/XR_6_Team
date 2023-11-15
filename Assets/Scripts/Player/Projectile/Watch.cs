using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watch : MonoBehaviour
{
    Animator anim;
    float duration;
    float time = 0;

    public void Init(float duration)
    {
        this.duration = duration;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        time += Time.deltaTime;

        if (time >= duration)
            anim.SetBool("isEnd", true);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Watch_Explosion") &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            ObjectPoolManager.ReturnObjectToPool(this.gameObject);

    }
}
