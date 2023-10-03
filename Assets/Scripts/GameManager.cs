using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;
    [SerializeField] float time;

    [HideInInspector] public UnityEvent TimeEvent;

    public void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public void Update()
    {
        time += Time.deltaTime;
        if(time >= 0)
        {
            TimeEvent.Invoke();
        }
    }
}
