using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamingo : MonoBehaviour
{
    Player player;
    public Vector3 initPosition;

    void Start()
    {
        player = GetComponentInParent<Player>();
        transform.position = new Vector3(player.transform.position.x + 3f, player.transform.position.y);
        initPosition = new Vector3(player.transform.position.x + 0.5f, player.transform.position.y);
    }

    void Update()
    {
        transform.RotateAround(initPosition, Vector3.back, 2);
    }
}
