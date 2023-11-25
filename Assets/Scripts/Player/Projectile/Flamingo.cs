using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamingo : MonoBehaviour
{
    public AnimationCurve SwishCurve;
    public Vector3 initPosition;
    Transform playerPos;
    float time = 0;
    float damage = 0;
    Vector3 fb;

    public void Init(float dmg, Player p)
    {
        playerPos = p.transform;
        damage = dmg;
    }

    void Start()
    {
        transform.position = new Vector3(playerPos.position.x + 1.74f, playerPos.position.y + 2f);
        initPosition = new Vector3(playerPos.position.x + 0.5f, playerPos.position.y);
    }

    void Update()
    {
        SwishFlamingo();
    }

    void SwishFlamingo()
    {
        time += Time.deltaTime;

        if (time <= 0.12f)
            fb = Vector3.forward;
        else if (time > 0.12f)
            fb = Vector3.back;

        transform.RotateAround(initPosition, fb, SwishCurve.Evaluate(time) * 1.7f);
        if (SwishCurve.Evaluate(time) == 0)
        {
            playerPos.GetComponent<Animator>().SetBool("IsFlamingo", false);
            playerPos.GetComponent<Player>().SetIsMove(true);
            ObjectPoolManager.ReturnObjectToPool(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<ITakeDamage>().TakeDamage(damage);
    }
}
