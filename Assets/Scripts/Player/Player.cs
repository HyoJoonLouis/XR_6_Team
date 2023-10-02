using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Info")]
    public float Speed;
    public float MaxHp;
    float CurrentHp;

    [Header("PlayerManager")]
    Movement move;
    Weapon weapon;

    bool isFire = false;

    private void Awake()
    {
        move = GetComponent<Movement>();
        weapon = GetComponentInChildren<Weapon>();

        CurrentHp = MaxHp;
    }

    private void Update()
    {
        move.Move(Speed);
    }

    public void TakeDamage(float value)
    {
        CurrentHp -= value;

        if (CurrentHp <= 0)
        {
            // Gameover
            Destroy(this);
        }
    }

    private void OnFire(InputValue value)
    {
        if (value.isPressed)
            weapon.OnShot(true);
        else
            weapon.OnShot(false);
    }
}
