using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatter : AIMovement
{
    [Header("Hatter")]
    [SerializeField] GameObject AmmoGameObject;
    [SerializeField] float AmmoStartTime;
    [SerializeField] float DelayTime;
    Transform AmmoStartPosition;

    Coroutine coroutine;
    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if(time >= AmmoStartTime && coroutine == null)
        {
            coroutine = StartCoroutine(SpawnAmmo());
        }
    }

    IEnumerator SpawnAmmo()
    {
        Animator animator = GetComponent<Animator>();
        animator.Play("OutsideHat");
        yield return new WaitForSeconds(1.5f);
        while (true)
        {
            isMoveable = false;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(-4.19f, -4.84f), 11 * Time.deltaTime);
            yield return null;
            //ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 0));
            //ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 45));
            //ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 90));
            //ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 135));
            //ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 180));
            //ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 225));
            //ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 270));
            //ObjectPoolManager.SpawnObject(AmmoGameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 315));
            //DelayTime = Random.Range(0.4f, 0.6f);
            //yield return new WaitForSeconds(DelayTime);
            if(Vector2.Distance(transform.position, new Vector2(-4.19f, -4.84f)) < 0.1f){
                break;
            }
            //this.transform.Rotate(new Vector3(0, 0, 13));
        }
        while(true)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(-7.32f, -0.87f), 11 * Time.deltaTime);
            yield return null;
            if (Vector2.Distance(transform.position, new Vector2(-7.32f, -0.87f)) < 0.1f)
            {
                break;
            }
        }
        while(true)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(-0.98f, 3.17f), 12 * Time.deltaTime);
            yield return null;
            if (Vector2.Distance(transform.position, new Vector2(-0.98f, 3.17f)) < 0.1f)
            {
                break;
            }
        }
        while(true)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(8.74f, -1f), 12 * Time.deltaTime);
            yield return null;
            if (Vector2.Distance(transform.position, new Vector2(8.74f, -1f)) < 0.1f)
            {
                break;
            }
        }
        while(true)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(0.5f, 0f), 5 * Time.deltaTime);
            yield return null;
            if (Vector2.Distance(transform.position, new Vector2(0.5f, 0f)) < 0.1f)
            {
                break;
            }
        }
        int random = Random.Range(0, 2);
        AmmoStartPosition = transform.GetChild(0);

        if(random == 0)
        {
            for(int i = 0; i< 8; i++)
            {
                ObjectPoolManager.SpawnObject(GameManager.instance.Ammos[10], AmmoStartPosition.position, Quaternion.Euler(0, 0, AmmoStartPosition.transform.eulerAngles.z + 0));
                ObjectPoolManager.SpawnObject(GameManager.instance.Ammos[11], AmmoStartPosition.position, Quaternion.Euler(0, 0, AmmoStartPosition.transform.eulerAngles.z + 45));
                ObjectPoolManager.SpawnObject(GameManager.instance.Ammos[12], AmmoStartPosition.position, Quaternion.Euler(0, 0, AmmoStartPosition.transform.eulerAngles.z + 90));
                ObjectPoolManager.SpawnObject(GameManager.instance.Ammos[10], AmmoStartPosition.position, Quaternion.Euler(0, 0, AmmoStartPosition.transform.eulerAngles.z + 135));
                ObjectPoolManager.SpawnObject(GameManager.instance.Ammos[11], AmmoStartPosition.position, Quaternion.Euler(0, 0, AmmoStartPosition.transform.eulerAngles.z + 180));
                ObjectPoolManager.SpawnObject(GameManager.instance.Ammos[12], AmmoStartPosition.position, Quaternion.Euler(0, 0, AmmoStartPosition.transform.eulerAngles.z + 225));
                ObjectPoolManager.SpawnObject(GameManager.instance.Ammos[10], AmmoStartPosition.position, Quaternion.Euler(0, 0, AmmoStartPosition.transform.eulerAngles.z + 270));
                ObjectPoolManager.SpawnObject(GameManager.instance.Ammos[11], AmmoStartPosition.position, Quaternion.Euler(0, 0, AmmoStartPosition.transform.eulerAngles.z + 315));
                DelayTime = Random.Range(0.4f,0.8f);
                yield return new WaitForSeconds(DelayTime);
                AmmoStartPosition.transform.Rotate(new Vector3(0, 0, 13));
            }
            animator.Play("InsideHat");
            yield return new WaitForSeconds(AmmoStartTime);
        }
        else
        {
            animator.Play("Fake");
            yield return new WaitForSeconds(2.0f + AmmoStartTime);
        }
        StartCoroutine(SpawnAmmo());
    }
}
