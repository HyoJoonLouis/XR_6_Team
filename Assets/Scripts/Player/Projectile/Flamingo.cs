using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamingo : MonoBehaviour
{
    public AnimationCurve SwishCurve;
    public Vector3 initPosition;
    Player player;
    float time = 0;
    float damage = 0;

    public void Init(float dmg, Player p)
    {
        player = p;
        damage = dmg;
    }

    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, 45);
        transform.position = new Vector3(player.transform.position.x + 3f, player.transform.position.y + 2f);
        initPosition = new Vector3(player.transform.position.x + 0.5f, player.transform.position.y);
    }

    void Update()
    {
        SwishFlamingo();
    }

    void SwishFlamingo()
    {
        time += Time.deltaTime;
        transform.RotateAround(initPosition, Vector3.back, SwishCurve.Evaluate(time));
        if (SwishCurve.Evaluate(time) == 0)
            ObjectPoolManager.ReturnObjectToPool(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<ITakeDamage>().TakeDamage(damage);
    }
}
