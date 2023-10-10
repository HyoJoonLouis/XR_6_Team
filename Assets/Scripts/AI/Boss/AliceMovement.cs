using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceMovement : BossBaseMovement
{
    public enum State
    {
        Idle,
        Pattern1,
        Pattern2,
        Pattern5,
        Pattern6,
        Pattern7,
    }

    public Dictionary<State, BaseState> AliceState = new Dictionary<State, BaseState>();

    void Start()
    {
        AliceState.Add(State.Idle, new AliceIdleState(this));
        AliceState.Add(State.Pattern1, new Pattern1(this));
        AliceState.Add(State.Pattern2, new Pattern2(this));
        AliceState.Add(State.Pattern5, new Pattern5(this));
        AliceState.Add(State.Pattern6, new Pattern6(this));
        AliceState.Add(State.Pattern7, new Pattern7(this));

        CurrentState = AliceState[State.Idle];
        CurrentState.OnStateEnter();

    }
}

public class AliceIdleState : BaseState
{
    public AliceIdleState(BossBaseMovement alice) : base(alice) { }

    public override void OnStateEnter()
    {
        Monster.StartCoroutine(ChangeRandomState());
    }

    public override void OnStateUpdate()
    {
    }
    public override void OnStateExit()
    {
    }

    IEnumerator ChangeRandomState()
    {
        yield return new WaitForSeconds(2);
        Monster.ChangeState(((AliceMovement)Monster).AliceState[(AliceMovement.State)Random.Range(0,((AliceMovement)Monster).AliceState.Count)]);
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
        for(int repeat = 0; repeat < Random.Range(10,20); repeat++)
        {
            for(int i = 0; i< 15; i++)
            {
                ObjectPoolManager.SpawnObject(GameManager.instance.Ammos[6], Monster.AmmoSpawnPosition[0].position, Quaternion.Euler(0, 0, Monster.AmmoSpawnPosition[0].eulerAngles.z + (i * 24)));
            }
            yield return new WaitForSeconds(0.7f);
            Monster.AmmoSpawnPosition[0].Rotate(0, 0, 12);
            for(int i = 0; i< 15; i++)
            {
                ObjectPoolManager.SpawnObject(GameManager.instance.Ammos[5], Monster.AmmoSpawnPosition[0].position, Quaternion.Euler(0, 0, Monster.AmmoSpawnPosition[0].eulerAngles.z + (i * 24)));
            }
            yield return new WaitForSeconds(0.7f);
        }
        Monster.ChangeState(((AliceMovement)Monster).AliceState[AliceMovement.State.Idle]);
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
        for(int repeat = 0; repeat < Random.Range(0,4); repeat++)
        {
            for(int i = 0;i < 12; i++)
            {
                ObjectPoolManager.SpawnObject(GameManager.instance.Ammos[2], Monster.AmmoSpawnPosition[0].position, Quaternion.Euler(0, 0, Monster.AmmoSpawnPosition[0].eulerAngles.z + (i * 30)));
            }
            yield return new WaitForSeconds(0.7f);
            Vector2 direction = new Vector2(Monster.transform.position.x - Monster.TargetTransform.position.x, Monster.transform.position.y - Monster.TargetTransform.transform.position.y);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            ObjectPoolManager.SpawnObject(GameManager.instance.Ammos[2], Monster.AmmoSpawnPosition[1].position, Quaternion.Euler(0, 0, 180 + angle));
            yield return new WaitForSeconds(0.7f);
            ObjectPoolManager.SpawnObject(GameManager.instance.Ammos[2], Monster.AmmoSpawnPosition[1].position, Quaternion.Euler(0, 0, 175 + angle));
            Debug.Log(angle);
            yield return new WaitForSeconds(0.7f);
        }
        Monster.ChangeState(((AliceMovement)Monster).AliceState[AliceMovement.State.Idle]);
    }
}

public class Pattern5 : BaseState
{
    public Pattern5(BossBaseMovement alice) : base(alice) { }
    public override void OnStateEnter()
    {
        Monster.StartCoroutine(Pattern5Coroutine());
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
    }

    IEnumerator Pattern5Coroutine()
    {
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], new Vector3(10, 4, 0), Quaternion.Euler(0,0,0));
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], new Vector3(10, 2, 0), Quaternion.Euler(0, 0, 0));
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], new Vector3(10, -2, 0), Quaternion.Euler(0, 0, 0));
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[0], new Vector3(10, -4, 0), Quaternion.Euler(0, 0, 0));

        yield return new WaitForSeconds(2.5f);

        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[2], new Vector3(10, 3, 0), Quaternion.Euler(0, 0, 0));
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[2], new Vector3(10, 0, 0), Quaternion.Euler(0, 0, 0));
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[2], new Vector3(10, -3, 0), Quaternion.Euler(0, 0, 0));

        Monster.ChangeState(((AliceMovement)Monster).AliceState[AliceMovement.State.Idle]);
    }
}

public class Pattern6 : BaseState
{
    public Pattern6(BossBaseMovement alice) : base(alice) { }
    public override void OnStateEnter()
    {
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[3], new Vector3(10, 3.3f, 0), Quaternion.Euler(0, 0, 0));
        ObjectPoolManager.SpawnObject(GameManager.instance.Monsters[3], new Vector3(10, -3.3f, 0), Quaternion.Euler(0, 0, 0));

        Monster.ChangeState(((AliceMovement)Monster).AliceState[AliceMovement.State.Idle]);
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
    }
}

public class Pattern7 : BaseState
{
    Vector3 initPosition;

    Coroutine coroutine;

    public Pattern7(BossBaseMovement alice) : base(alice) { }

    public override void OnStateEnter()
    {
        Monster.isMoveable = false;
        initPosition = Monster.transform.position;

        coroutine = Monster.StartCoroutine(Pattern6Coroutine());
    }

    public override void OnStateExit()
    {
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
        GameObject GiantHand = ObjectPoolManager.SpawnObject(GameManager.instance.Ammos[7], new Vector3(0, 10, 0), Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(0.5f);

        GameObject GiantHandRange = ObjectPoolManager.SpawnObject(GameManager.instance.Ammos[8], new Vector3(0, 10, 0), Quaternion.Euler(0, 0, 0));
        float time = 0;
        while(time <= 3)
        {
            GiantHandRange.transform.position = new Vector2(Monster.TargetTransform.transform.position.x, 0.5f);
            time += Time.deltaTime;
            yield return null;
        }

        ObjectPoolManager.ReturnObjectToPool(GiantHandRange);

        GiantHand.transform.position = new Vector2(Monster.TargetTransform.position.x, 10);
            
        while(GiantHand.transform.position.y > 0.5f )
        {
            GiantHand.transform.position = Vector2.Lerp(GiantHand.transform.position, new Vector2(GiantHand.transform.position.x, 0.45f), Time.deltaTime * 4.5f);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        while(GiantHand.transform.position.y < 10)
        {
            GiantHand.transform.Translate(Vector2.up * Time.deltaTime * 10); 
            /*GiantHand.transform.position = Vector2.Lerp(GiantHand.transform.position, new Vector2(GiantHand.transform.position.x, 11), Time.deltaTime * 2);*/
            yield return null;
        }
        ObjectPoolManager.ReturnObjectToPool(GiantHand);

        while(Monster.transform.position.y > initPosition.y + 0.1f)
        {
            Monster.transform.position = Vector2.Lerp(Monster.transform.position, initPosition, 2 * Time.deltaTime);
            yield return null;
        }

        Monster.transform.position = initPosition;

        Monster.ChangeState(((AliceMovement)Monster).AliceState[AliceMovement.State.Idle]);
    }
}

