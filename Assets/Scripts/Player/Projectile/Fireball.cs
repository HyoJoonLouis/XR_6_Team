using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    float Speed;
    float Damage;
    int CurrentAttack;
    int MaxAttack;
    [SerializeField] AnimationCurve curveUp;
    float time;
    [SerializeField] GameObject OnHitParticle;

    public void Init(float speed, float damage, int maxAtk)
    {
        Speed = speed;
        Damage = damage;
        MaxAttack = maxAtk;
        CurrentAttack = MaxAttack;
    }

    private void OnEnable()
    {
        this.transform.localScale = new Vector3(0.4f, 0.4f, 1);
        time = 0;
    }

    private void Update()
    {
        transform.Translate(Vector3.right * Speed * Time.deltaTime);

        if (this.transform.position.x < -13 || this.transform.position.x > 13 || this.transform.position.y > 7 || this.transform.position.y < -7)
            ObjectPoolManager.ReturnObjectToPool(this.gameObject);

        if (time < 1)
        {
            time += Time.deltaTime;
            this.transform.localScale = new Vector3(curveUp.Evaluate(time), curveUp.Evaluate(time), 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<ITakeDamage>().TakeDamage(Damage);
        
        CurrentAttack -= 1;
        if (CurrentAttack <= 0)
        {
            ObjectPoolManager.SpawnObject(OnHitParticle, this.transform.position, this.transform.rotation);
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }
}
