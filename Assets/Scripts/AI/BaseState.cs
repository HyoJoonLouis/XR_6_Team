using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    protected BossBaseMovement Monster;

    protected BaseState(BossBaseMovement monster)
    {
        Monster = monster;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();
}
