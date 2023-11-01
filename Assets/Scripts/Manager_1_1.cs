using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Manager_1_1 : MonoBehaviour
{
    [HideInInspector] public Player player;


    [SerializeField] float time;
    float SpawnTime;

    [SerializeField] float TimeRate;

    Dictionary<int, string> Phase1_1 = new Dictionary<int, string>();


    private void Start()
    {
        player = FindObjectOfType<Player>();

        Phase1_1.Add(0, "SpawnSpade");
        Phase1_1.Add(1, "SpawnDiamond");
        Phase1_1.Add(2, "SpawnThreeClover");
        Phase1_1.Add(3, "SpawnSixClover");
        Phase1_1.Add(4, "SpawnTwoHeartTwoSpade");
        Phase1_1.Add(5, "SpawnOneDiamondTwoSpade");

        UIManager.instance.ChangeLaughter(LaughterSprite.WhiteRabbit);
        StartCoroutine(Phase1_1SpawnCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        SpawnTime += Time.deltaTime;

        if (SpawnTime >= TimeRate)
        {
            SpawnTime -= TimeRate;
            StartCoroutine(Phase1_1[UnityEngine.Random.Range(0, Phase1_1.Count)]);
        }
    }

    IEnumerator Phase1_1SpawnCoroutine()
    {
        StartCoroutine(Phase1_1[1]);
        yield return new WaitForSeconds(20);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[8], Vector3.zero, this.transform.rotation);
        yield return new WaitForSeconds(10);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[4], new Vector3(15, 0, 0), this.transform.rotation);
        yield return new WaitForSeconds(30);
        SpawnTime *= 0.8f;
        yield return new WaitForSeconds(10);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[8], Vector3.zero, this.transform.rotation);
        yield return new WaitForSeconds(10);
        SpawnTime *= 0.5f;
        yield return new WaitForSeconds(10);
        //게임 종료
    }

    IEnumerator SpawnSpade()
    {
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[3], player.transform.position.y >= 0 ? new Vector3(11, 3, 0) : new Vector3(11, -3, 0), this.transform.rotation);
        yield return null;
    }

    IEnumerator SpawnDiamond()
    {
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[2], new Vector3(11, 1.5f, 0), this.transform.rotation);
        yield return null;
    }

    IEnumerator SpawnThreeClover()
    {
        bool y = player.transform.position.y >= 0;
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], y ? new Vector3(11.25f, 3.6f, 0) : new Vector3(11.25f, -3.6f, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], y ? new Vector3(11.25f, 2, 0) : new Vector3(11.25f, -2, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], y ? new Vector3(11.25f, 0.4f, 0) : new Vector3(11.25f, -0.4f, 0), this.transform.rotation);
        yield return null;
    }

    IEnumerator SpawnSixClover()
    {
        bool y = player.transform.position.y >= 0;
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], y ? new Vector3(11.25f, 4, 0) : new Vector3(11.25f, -0.8f, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], y ? new Vector3(11.25f, 2.4f, 0) : new Vector3(11.25f, -2.4f, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], y ? new Vector3(11.25f, 0.8f, 0) : new Vector3(11.25f, -4f, 0), this.transform.rotation);

        yield return new WaitForSeconds(1.3f);

        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], y ? new Vector3(11.25f, -0.8f, 0) : new Vector3(11.25f, 4, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], y ? new Vector3(11.25f, -2.4f, 0) : new Vector3(11.25f, 2.4f, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], y ? new Vector3(11.25f, -4f, 0) : new Vector3(11.25f, 0.8f, 0), this.transform.rotation);
        yield return null;
    }

    IEnumerator SpawnTwoHeartTwoSpade()
    {
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[3], new Vector3(12, 4, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[3], new Vector3(12, -4, 0), this.transform.rotation);

        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[1], new Vector3(11, 1, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[1], new Vector3(11, -1, 0), this.transform.rotation);
        yield return null;
    }

    IEnumerator SpawnOneDiamondTwoSpade()
    {
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[3], new Vector3(12, 3, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[3], new Vector3(12, -3, 0), this.transform.rotation);

        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[2], new Vector3(12, 0, 0), this.transform.rotation);
        yield return null;
    }
}
