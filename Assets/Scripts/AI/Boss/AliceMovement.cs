using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceMovement : BossBaseMovement
{
    private enum State
    {
        Idle,
        Pattern1,
        Pattern2,
        Pattern6,
    }

    Dictionary<State, BaseState> AliceState = new Dictionary<State, BaseState>();

    void Start()
    {
        AliceState.Add(State.Idle, new AliceIdleState(this));
        AliceState.Add(State.Pattern1, new Pattern1(this));
        AliceState.Add(State.Pattern2, new Pattern2(this));
        AliceState.Add(State.Pattern6, new Pattern6(this));

        CurrentState = AliceState[State.Pattern6];
        CurrentState.OnStateEnter();

    }
}

public class AliceIdleState : BaseState
{
    public AliceIdleState(BossBaseMovement alice) : base(alice) { }

    public override void OnStateEnter()
    {
    }

    public override void OnStateUpdate()
    {
    }
    public override void OnStateExit()
    {
    }
}

public class Pattern1 : BaseState
{
    public Pattern1(BossBaseMovement alice) : base(alice) { }

    Coroutine coroutine;
    public override void OnStateEnter()
    {
        coroutine = Monster.StartCoroutine(Pattern1Coroutine());
    }

    public override void OnStateExit()
    {
        Monster.StopCoroutine(coroutine);
        Monster.AmmoSpawnPosition[0].eulerAngles = new Vector3(0, 0, 0);
    }

    public override void OnStateUpdate()
    {
    }

    IEnumerator Pattern1Coroutine()
    {
        while(true)
        {
            for(int i = 0; i< 15; i++)
            {
                ObjectPoolManager.SpawnObject(GameManager.instance.Ammos[0], Monster.AmmoSpawnPosition[0].position, Quaternion.Euler(0, 0, Monster.AmmoSpawnPosition[0].eulerAngles.z + (i * 24)));
            }
            yield return new WaitForSeconds(0.7f);
            Monster.AmmoSpawnPosition[0].Rotate(0, 0, 12);
        }
    }
}

public class Pattern2 : BaseState
{
    public Pattern2(BossBaseMovement alice) : base(alice){}

    Coroutine coroutine;

    public override void OnStateEnter()
    {
        coroutine = Monster.StartCoroutine(Pattern2Coroutine());
    }

    public override void OnStateExit()
    {
        Monster.StopCoroutine(coroutine);
    }

    public override void OnStateUpdate()
    {
    }

    IEnumerator Pattern2Coroutine()
    {
        while(true)
        {
            for(int i = 0;i < 12; i++)
            {
                ObjectPoolManager.SpawnObject(GameManager.instance.Ammos[0], Monster.AmmoSpawnPosition[0].position, Quaternion.Euler(0, 0, Monster.AmmoSpawnPosition[0].eulerAngles.z + (i * 30)));
            }
            yield return new WaitForSeconds(0.7f);
            Vector2 direction = new Vector2(Monster.transform.position.x - Monster.TargetTransform.position.x, Monster.transform.position.y - Monster.TargetTransform.transform.position.y);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            ObjectPoolManager.SpawnObject(GameManager.instance.Ammos[0], Monster.AmmoSpawnPosition[1].position, Quaternion.Euler(0, 0, 180 + angle));
            yield return new WaitForSeconds(0.7f);
            ObjectPoolManager.SpawnObject(GameManager.instance.Ammos[0], Monster.AmmoSpawnPosition[1].position, Quaternion.Euler(0, 0, 175 + angle));
            Debug.Log(angle);
            yield return new WaitForSeconds(0.7f);
        }
    }
}

public class Pattern6 : BaseState
{
    Vector3 initPosition;

    Vector3 TargetPosition;

    Coroutine coroutine;

    public Pattern6(BossBaseMovement alice) : base(alice) { }

    public override void OnStateEnter()
    {
        Monster.isMoveable = false;
        initPosition = Monster.transform.position;

        coroutine = Monster.StartCoroutine(Pattern6Coroutine());
    }

    public override void OnStateExit()
    {
        Monster.StopCoroutine(coroutine);
        Monster.isMoveable = true;
    }

    public override void OnStateUpdate()
    {
    }

    IEnumerator Pattern6Coroutine()
    {
        while(Monster.transform.position.y < 8)
        {
            Monster.transform.position = Vector2.Lerp(Monster.transform.position, new Vector2(Monster.transform.position.x, 9), 2 * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        GameObject GiantHand = ObjectPoolManager.SpawnObject(GameManager.instance.Ammos[3], new Vector3(0, 10, 0), Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(3);
        GiantHand.transform.position = new Vector2(Monster.TargetTransform.position.x, 10);
            
        while(GiantHand.transform.position.y > 2.5f )
        {
            GiantHand.transform.position = Vector2.Lerp(GiantHand.transform.position, new Vector2(GiantHand.transform.position.x, 2.4f), Time.deltaTime * 3.5f);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        while(GiantHand.transform.position.y < 10)
        {
            GiantHand.transform.Translate(Vector2.up * Time.deltaTime * 7); 
            /*GiantHand.transform.position = Vector2.Lerp(GiantHand.transform.position, new Vector2(GiantHand.transform.position.x, 11), Time.deltaTime * 2);*/
            yield return null;
        }
        ObjectPoolManager.ReturnObjectToPool(GiantHand);

        Monster.transform.position = initPosition;
    }
}

