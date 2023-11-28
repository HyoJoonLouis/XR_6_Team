using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager1_2 : MonoBehaviour
{
    [HideInInspector] public Player player;

    [SerializeField] float time;
    float SpawnTime;

    [SerializeField] float TimeRate;

    Dictionary<int, string> Phase1_2 = new Dictionary<int, string>();

    private bool StartGame;

    public UIManager uimanager;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        StartGame = false;

        Phase1_2.Add(0, "SpawnSpade");
        Phase1_2.Add(1, "SpawnDiamond");
        Phase1_2.Add(2, "SpawnFourClover");
        Phase1_2.Add(3, "SpawnEightClover");
        Phase1_2.Add(4, "SpawnTwoHeartTwoSpade");
        Phase1_2.Add(5, "SpawnOneDiamondTwoSpade");
        Phase1_2.Add(6, "SpawnFourHeartTwoSpade");
        Phase1_2.Add(7, "SpawnSixClover");
        

        uimanager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!StartGame)
            return;

        time += Time.deltaTime;
        SpawnTime += Time.deltaTime;

        if (SpawnTime >= TimeRate)
        {
            SpawnTime -= TimeRate;
            StartCoroutine(Phase1_2[UnityEngine.Random.Range(0, Phase1_2.Count)]);
        }
    }
    public void StartStage()
    {
        StartGame = true;
        StartCoroutine(Phase1_1SpawnCoroutine());
    }

    IEnumerator Phase1_1SpawnCoroutine()
    {
        StartCoroutine(Phase1_2[1]);
        yield return new WaitForSeconds(10);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[5], new Vector3(7.2f, -4.8f, 0), this.transform.rotation);
        yield return new WaitForSeconds(20);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[8], Vector3.zero, this.transform.rotation);
        yield return new WaitForSeconds(20);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[5], new Vector3(7.2f, -4.8f, 0), this.transform.rotation);
        yield return new WaitForSeconds(10);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[6], new Vector3(15, 0, 0), this.transform.rotation);
        yield return new WaitForSeconds(20);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[8], Vector3.zero, this.transform.rotation);
        yield return new WaitForSeconds(10);
        SpawnTime *= 0.7f;
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

    IEnumerator SpawnFourClover()
    {
        bool y = player.transform.position.y >= 0;
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], y ? new Vector3(11.25f, 4.1f, 0) : new Vector3(11.25f, -4.1f, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], y ? new Vector3(11.25f, 2.9f, 0) : new Vector3(11.25f, -2.9f, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], y ? new Vector3(11.25f, 1.7f, 0) : new Vector3(11.25f, -1.7f, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], y ? new Vector3(11.25f, 0.5f, 0) : new Vector3(11.25f, -0.5f, 0), this.transform.rotation);
        yield return null;
    }

    IEnumerator SpawnEightClover()
    {
        bool y = player.transform.position.y >= 0;
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], y ? new Vector3(11.25f, 4.1f, 0) : new Vector3(11.25f, -4.1f, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], y ? new Vector3(11.25f, 2.9f, 0) : new Vector3(11.25f, -2.9f, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], y ? new Vector3(11.25f, 1.7f, 0) : new Vector3(11.25f, -1.7f, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], y ? new Vector3(11.25f, 0.5f, 0) : new Vector3(11.25f, -0.5f, 0), this.transform.rotation);

        yield return new WaitForSeconds(1.3f);

        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], y ? new Vector3(11.25f, 4.1f, 0) : new Vector3(11.25f, -4.1f, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], y ? new Vector3(11.25f, 2.9f, 0) : new Vector3(11.25f, -2.9f, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], y ? new Vector3(11.25f, 1.7f, 0) : new Vector3(11.25f, -1.7f, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], y ? new Vector3(11.25f, 0.5f, 0) : new Vector3(11.25f, -0.5f, 0), this.transform.rotation);
        yield return null;
    }

    IEnumerator SpawnTwoHeartTwoSpade()
    {
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[3], new Vector3(12, 3.5f, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[3], new Vector3(12, -3.5f, 0), this.transform.rotation);

        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[1], new Vector3(11, 1.2f, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[1], new Vector3(11, 0, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[1], new Vector3(11, -1.2f, 0), this.transform.rotation);
        yield return null;
    }

    IEnumerator SpawnOneDiamondTwoSpade()
    {
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[3], new Vector3(12, 3, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[3], new Vector3(12, -3, 0), this.transform.rotation);

        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[2], new Vector3(12, 0, 0), this.transform.rotation);
        yield return null;
    }

    IEnumerator SpawnFourHeartTwoSpade()
    {
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[1], new Vector3(19, 4, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[1], new Vector3(19, -4, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[1], new Vector3(11, 1, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[1], new Vector3(11, -1, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[3], new Vector3(12, 0, 0), this.transform.rotation);        yield return null;
        yield return null;
    }

    IEnumerator SpawnSixClover()
    {
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], new Vector3(11.25f, 0.8f, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], new Vector3(11.25f, -0.8f, 0), this.transform.rotation);
        yield return new WaitForSeconds(1);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], new Vector3(11.25f, 4, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], new Vector3(11.25f, 2.4f, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], new Vector3(11.25f, -2.4f, 0), this.transform.rotation);
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], new Vector3(11.25f, -4, 0), this.transform.rotation);
        yield return null;
    }
}
