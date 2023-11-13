using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamingo : MonoBehaviour
{
    public AnimationCurve SwishCurve;
    public Vector3 initPosition;
    Transform startPosition;
    Player player;
    float time = 0;

    void Start()
    {
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
        {
            transform.position = startPosition.position;
            transform.rotation = startPosition.rotation;
            this.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        player = GetComponentInParent<Player>();
        transform.position = new Vector3(player.transform.position.x + 3f, player.transform.position.y + 2.2f);
        startPosition = transform;
        initPosition = new Vector3(player.transform.position.x + 0.5f, player.transform.position.y);
        time = 0;
    }
}
