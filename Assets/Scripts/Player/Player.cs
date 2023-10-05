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

    [Header("Hit")]
    bool isUnbeatTime = false;
    Renderer render;
    BoxCollider2D boxCollider;

    [Header("Movement")]
    Movement move;

    [Header("Weapon")]
    Weapon weapon;
    public GameObject[] subWeapons;
    int MaxSubWeaponCount = 3;
    int CurSubWeaponCount = 0;

    private void Awake()
    {
        move = GetComponent<Movement>();
        weapon = GetComponentInChildren<Weapon>();
        render = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        CurrentHp = MaxHp;
    }

    private void Update()
    {
        if (isUnbeatTime)
            boxCollider.enabled = false;
        else
            boxCollider.enabled = true;

        move.Move(Speed);
        CameraWorldSpace();
    }

    public void TakeDamage(float value)
    {
        CurrentHp -= value;
        isUnbeatTime = true;
        StartCoroutine(UnBeatTime());

        if (CurrentHp <= 0)
        {
            // Gameover
        }
    }

    IEnumerator UnBeatTime()
    {
        int countTime = 0;

        while (countTime < 10)
        {
            if (countTime % 2 == 0)
                render.material.color = new Color32(255, 255, 255, 90);
            else
                render.material.color = new Color32(255, 255, 255, 180);

            yield return new WaitForSeconds(0.2f);

            countTime++;
        }

        render.material.color = new Color32(255, 255, 255, 255);
        isUnbeatTime = false;

        yield return null;
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
