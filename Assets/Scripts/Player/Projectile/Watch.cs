using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watch : MonoBehaviour
{
    Animator anim;
    float duration;
    public float time = 0;

    public void Init(float duration)
    {
        this.duration = duration;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(DurationCheck());
    }

    void Update()
    {
        if (time >= duration)
            anim.SetBool("isEnd", true);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Watch_Explosion") &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            time = 0;
            ObjectPoolManager.ReturnObjectToPool(this.gameObject);
        }
    }

    IEnumerator DurationCheck()
    {
        while (true)
        {
            time += Time.unscaledDeltaTime;

            if (time < duration)
                Time.timeScale = 0;
            else if (time >= duration)
                Time.timeScale = 1;

            yield return new WaitForSecondsRealtime(0.01f);
        }
    }
}
