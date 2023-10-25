using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;

    public List<GameObject> Monsters;
    public List<GameObject> Ammos;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
