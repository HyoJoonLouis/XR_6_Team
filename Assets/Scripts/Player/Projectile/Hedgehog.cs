using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hedgehog : MonoBehaviour
{
    float damage;
    float speed;

    public void Init(Player p, float damage, float projectileSpeed)
    {
        this.damage = damage;
        speed = projectileSpeed;

        transform.position = new Vector3(p.transform.position.x + 2, p.transform.position.y);
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (this.transform.position.x < -13 || this.transform.position.x > 13 || this.transform.position.y > 7 || this.transform.position.y < -7)
            ObjectPoolManager.ReturnObjectToPool(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<ITakeDamage>().TakeDamage(damage);

        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
}
