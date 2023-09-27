using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] int level;
    float Speed;

    [Header("PlayerManager")]
    Movement move;
    Weapon weapon;

    UnityEvent damageUp;    //임시 

    private void Awake()
    {
        move = GetComponent<Movement>();
        weapon = GetComponentInChildren<Weapon>();

        // 임시
        damageUp = new UnityEvent();
    }

    private void Update()
    {
        move.Move(Speed);
    }

    /*public override void TakeDamage(float value)
    {
        base.TakeDamage(value);

        if (CurrentHp <= 0)
        {
            // Gameover
        }
    }*/

    private void OnShot(InputValue value)
    {
        if (value.isPressed)
        {
            weapon.OnShot();
        }

    }

    #region DamageUpTrigger
    void OnDamageUp()
    {
        damageUp.AddListener(DamageUpTrigger);
        damageUp.Invoke();
        damageUp.RemoveListener(DamageUpTrigger);
    }

    public void OnTest1(InputValue value)
    {
        float keyCode = value.Get<float>();

        OnDamageUp();
    }

    public void DamageUpTrigger()
    {
        for (int i = 0; i < this.transform.childCount; ++i)
        {
            if (this.transform.GetChild(i).gameObject.GetComponent<BaseSkill>() == null)
                return;
            this.transform.GetChild(i).gameObject.AddComponent<SkillDamageUp>();
        }
    }

    #endregion DamageUpTrigger
}
