using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    float Speed;
    float Damage;
    int CurrentAttack;
    int MaxAttack;

    public void Init(float speed, float damage, int maxAtk)
    {
        Speed = speed;
        Damage = damage;
        MaxAttack = maxAtk;
        CurrentAttack = MaxAttack;
    }

    private void Update()
    {
        transform.Translate(Vector3.right * Speed * Time.deltaTime);

        if (this.transform.position.x < -13 || this.transform.position.x > 13 || this.transform.position.y > 7 || this.transform.position.y < -7)
            ObjectPoolManager.ReturnObjectToPool(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<ITakeDamage>().TakeDamage(Damage);
        
        CurrentAttack -= 1;
        if (CurrentAttack <= 0)
        {
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }
}
