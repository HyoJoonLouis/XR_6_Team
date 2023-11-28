using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hedgehog : MonoBehaviour
{
    float damage;
    float speed;
    float time = 0;

    public void Init(Player p, float damage, float projectileSpeed)
    {
        this.damage = damage;
        speed = projectileSpeed;
        time = 0;
        this.GetComponent<Animator>().SetBool("isDead", false);
        this.GetComponent<CircleCollider2D>().enabled = true;

        transform.position = new Vector3(p.transform.position.x + 0.5f, p.transform.position.y);
        p.GetComponent<Player>().SetIsUse(false);
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("HedgehogDead") &&
            this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            ObjectPoolManager.ReturnObjectToPool(this.gameObject);
        }

        if (this.transform.position.x < -13 || this.transform.position.x > 13 || this.transform.position.y > 7 || this.transform.position.y < -7)
            ObjectPoolManager.ReturnObjectToPool(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.GetComponent<Animator>().SetBool("isDead", true);
        collision.gameObject.GetComponent<ITakeDamage>().TakeDamage(damage);

        this.GetComponent<CircleCollider2D>().enabled = false;
    }
}
