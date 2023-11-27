using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseMovement : AIMovement
{
    [Header("Boss Base")]
    public List<Transform> AmmoSpawnPosition;
    public Transform TargetTransform;
    public bool StartGame;

    public BaseState CurrentState;

    public override void OnEnable()
    {
        base.OnEnable();
        TargetTransform = ((Player)FindObjectOfType(typeof(Player))).transform;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (!StartGame)
            return;

        CurrentState.OnStateUpdate();
    }
    public void ChangeState(BaseState changeState)
    {
        CurrentState.OnStateExit();
        CurrentState = changeState;
        CurrentState.OnStateEnter();
    }
}
