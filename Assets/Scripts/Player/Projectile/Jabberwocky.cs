using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jabberwocky : MonoBehaviour
{
    Transform player;
    Animator anim;
    float time = 0;
    float blendTime = 0;
    float laserDuration;
    float laserTime = 0;
    float damage;
    int level = 1;
    bool isOff = false;
    float posX, posY;

    // level
    // 1 - x+9.6 y-0.37
    // 2 - x stay (y+0.33) , (y-0.79)
    // 3 - x stay (y+1.07) , (y-0.37) , (y-1.57)
    public void Init(Player p, float duration, float damage, int level, int posType)
    {
        player = p.transform;
        laserDuration = duration;
        this.damage = damage;
        this.level = level;

        posX = 9.6f;
        switch (level)
        {
            case 1:
                posY = -0.37f;
                break;
            case 2:
                if (posType == 1)
                    posY = 0.53f;
                else if (posType == 2)
                    posY = -0.99f;
                break;
            case 3:
                if (posType == 1)
                    posY = 1.77f;
                else if (posType == 2)
                    posY = -0.37f;
                else if (posType == 3)
                    posY = -2.37f;
                break;
        }
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(DurationChecking());
    }

    void Update()
    {
        transform.position = new Vector3(player.position.x + posX, player.position.y + posY);

        if (blendTime < 0.3f)
            blendTime += Time.deltaTime;
        else if (blendTime <= 0.5f && blendTime >= 0.3f)
        {
            blendTime += Time.deltaTime;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        else if (blendTime >= 0.5f && blendTime < 1f && isOff)
            blendTime += Time.deltaTime;
        else if (blendTime >= 1)
        {
            player.GetComponent<Player>().SetIsUse(false);
            ObjectPoolManager.ReturnObjectToPool(this.gameObject);
            time = 0f;
            blendTime = 0f;
            isOff = false;
        }

        anim.SetFloat("LaserTime", blendTime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        lock (collision)
        {
            if (collision == null)
                return;

            collision.GetComponent<ITakeDamage>().TakeDamage(damage);
        }
    }

    IEnumerator DurationChecking()
    {
        while (!isOff)
        {
            if (time >= laserDuration)
            {
                isOff = true;
                yield return 0;
            }

            time += Time.deltaTime;

            yield return new WaitForSeconds(0.01f);
        }
    }
}
