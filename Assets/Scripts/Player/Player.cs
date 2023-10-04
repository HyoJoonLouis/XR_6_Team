using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, ITakeDamage
{
    [Header("Info")]
    public float Speed;
    public float MaxHp;
    float CurrentHp;

    Movement move;
    Weapon weapon;
    public GameObject[] subWeapons;
    int MaxSubWeaponCount = 3;
    int CurSubWeaponCount = 0;
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
        CameraWorldSpace();
    }

    public void TakeDamage(float value)
    {
        CurrentHp -= value;

        if (CurrentHp <= 0)
        {
            // Gameover
        }
    }

    void CameraWorldSpace()
    {
        Vector3 worldpos = Camera.main.WorldToViewportPoint(this.transform.position);
        if (worldpos.x < 0.02f) worldpos.x = 0.02f;
        if (worldpos.y < 0.1f) worldpos.y = 0.1f;
        if (worldpos.x > 0.98f) worldpos.x = 0.98f;
        if (worldpos.y > 0.95f) worldpos.y = 0.95f;
        this.transform.position = Camera.main.ViewportToWorldPoint(worldpos);
    }

    #region Weapon

    private void OnFire(InputValue value)
    {
        if (value.isPressed)
            weapon.OnShot(true);
        else
            weapon.OnShot(false);
    }

    private void OnUse(InputValue value)
    {
        if (CurSubWeaponCount <= 0)
            return;

        for (int i = 0; i < 3; ++i)
        {
            if (subWeapons[i] == null)
                continue;
        }

        subWeapons[CurSubWeaponCount-- - 1].SetActive(false);
    }

    private void OnTest(InputValue value)
    {
        if (CurSubWeaponCount >= MaxSubWeaponCount)
            return;
        CurSubWeaponCount += 1;

        ActiveSubWapon(CurSubWeaponCount);
    }

    void ActiveSubWapon(int cursub)
    {
        for (int i = 0; i < cursub; ++i)
        {
            subWeapons[i].SetActive(true);
        }
    }

    #endregion Weapon
}
