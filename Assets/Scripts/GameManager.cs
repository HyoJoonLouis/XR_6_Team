using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;
    [SerializeField] float time;
    [SerializeField] float Wave1Time;

    [HideInInspector] public UnityEvent TimeEvent;

    public List<GameObject> Monsters;
    public List<GameObject> Ammos;

    public void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public void Update()
    {
        time += Time.deltaTime;
        Wave1Time += Time.deltaTime;
        if(time >= 10)
        {
            TimeEvent.Invoke();
        }

        if(Wave1Time >= 10)
        {
            Wave1Time = 0;
            StartCoroutine(Wave1());
        }
    }

    IEnumerator Wave1()
    {
        WaitForSeconds threeSeconds =  new WaitForSeconds(3.0f);
        ObjectPoolManager.SpawnObject(Monsters[0], new Vector3(10, 3, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(Monsters[0], new Vector3(10, 0, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(Monsters[0], new Vector3(10, -3, 0), this.transform.rotation);

        yield return threeSeconds;

        ObjectPoolManager.SpawnObject(Monsters[0], new Vector3(10, 4, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(Monsters[0], new Vector3(10, 2, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(Monsters[0], new Vector3(10, -2, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(Monsters[0], new Vector3(10, -4, 0), this.transform.rotation);

        yield return threeSeconds;

        ObjectPoolManager.SpawnObject(Monsters[1], new Vector3(10, 3, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(Monsters[1], new Vector3(10, -3, 0), this.transform.rotation);

        yield return null;
    }

    IEnumerator Wave2()
    {
        WaitForSeconds threeSeconds = new WaitForSeconds(3.0f);
        ObjectPoolManager.SpawnObject(Monsters[2], new Vector3(10, 3, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(Monsters[2], new Vector3(10, -3, 0), this.transform.rotation);

        yield return threeSeconds;

        ObjectPoolManager.SpawnObject(Monsters[1], new Vector3(10, 3, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(Monsters[1], new Vector3(10, 0, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(Monsters[1], new Vector3(10, -3, 0), this.transform.rotation);

        yield return threeSeconds;

        ObjectPoolManager.SpawnObject(Monsters[1], new Vector3(10, 3, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(Monsters[1], new Vector3(10, -3, 0), this.transform.rotation);

        yield return null;
    }

}
