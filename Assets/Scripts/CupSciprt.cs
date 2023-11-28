using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CupSciprt : MonoBehaviour
{
    Vector2 TargetTransform;
    Collider2D collider;
    float Delay;

    IEnumerator OnSpawn()
    {
        while (true)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, TargetTransform, 8 * Time.deltaTime);
            yield return null;
            if (Vector2.Distance(this.transform.position, TargetTransform) < 0.1f)
                break;
        }
        yield return new WaitForSeconds(Delay);
        Animator animator = GetComponent<Animator>();
        animator.Play("Attack");
        collider.enabled = true;
        yield return new WaitForSeconds(3.0f);
        animator.Play("AttackEnd");
        collider.enabled = false;
        yield return new WaitForSeconds(2.0f);
        while(true)
        {
            transform.Translate(Vector2.down * Time.deltaTime);
            yield return null;
            if (Vector2.Distance(this.transform.position, new Vector2(transform.position.x, -20)) < 0.01f)
                break;
        }
        ObjectPoolManager.ReturnObjectToPool(this.gameObject);
    }

    public void SetTargetTransform(Vector2 transform, float delay)
    {
        Delay = delay;
        TargetTransform = transform;
        collider = GetComponent<Collider2D>();
        collider.enabled = false;
        StartCoroutine(OnSpawn());
    }
}
