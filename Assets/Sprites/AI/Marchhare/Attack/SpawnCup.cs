using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCup : MonoBehaviour
{
    public GameObject Cup;
    List<GameObject> Cups = new List<GameObject>();
    public void SpawnFirst()
    {
        ObjectPoolManager.SpawnObject(Cup, new Vector3(6, -6.1f,0), transform.rotation).GetComponent<CupSciprt>().SetTargetTransform(new Vector2(-5.8f, -6.11f));
    }

    public void SpawnSecond()
    {
        ObjectPoolManager.SpawnObject(Cup, new Vector3(6, -6.1f, 0), transform.rotation).GetComponent<CupSciprt>().SetTargetTransform(new Vector2(-1.3f, -6.11f));
    }
    public void SpawnThird()
    {

        ObjectPoolManager.SpawnObject(Cup, new Vector3(6, -6.1f, 0), transform.rotation).GetComponent<CupSciprt>().SetTargetTransform(new Vector2(3.5f, -6.1f));
    }
}
