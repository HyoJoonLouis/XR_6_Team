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
    Animator animator;

    [Header("Weapon")]
    public int MaxOnceWeaponCount = 3;
    Stack<GameObject> onceWeapons;
    Weapon weapon;

    [Header("UI")]
    public GameObject cameraUI;

    private void Start()
    {
        animator = GetComponent<Animator>();
        move = GetComponent<Movement>();
        weapon = GetComponentInChildren<Weapon>();
        render = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        onceWeapons = new Stack<GameObject>();

        CurrentHp = MaxHp;
        UIManager.instance.SetHealth((int)CurrentHp);
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
        UIManager.instance.SetHealth((int)CurrentHp);

        if (CurrentHp <= 0)
        {
            // Gameover
            UIManager.instance.GameOver();
            return;
        }

        isUnbeatTime = true;
        StartCoroutine(UnBeatTime());

        cameraUI.transform.GetComponent<CameraShake>().VibrateForTime(0.3f);
    }

    public void SetHp(float hp)
    {
        if (CurrentHp + hp > 3)
            CurrentHp = 3;
        else
            CurrentHp += hp;
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
        if (worldpos.y > 0.90f) worldpos.y = 0.90f;
        this.transform.position = Camera.main.ViewportToWorldPoint(worldpos);
    }

    public void OnPlayerDied() {
        this.gameObject.SetActive(false);
    }

    #region Weapon

    private void OnFire(InputValue value)
    {
        animator.SetBool("IsAttack", value.isPressed);

        if (value.isPressed)
        {
            weapon.OnShot(value.isPressed);
        }
        else
        {
            weapon.OnShot(value.isPressed);
        }
    }

    private void OnUse(InputValue value)
    {
        if (onceWeapons.Count <= 0)
            return;
        Debug.Log("ÀÛµ¿");
        Debug.Log(onceWeapons.Count);
        onceWeapons.Pop().GetComponent<OnceWeapon>().OnUse();
    }

    public void AddOnceWeapon(GameObject onceweapon)
    {
        OnceWeapon once = onceweapon.GetComponent<OnceWeapon>();
        if (onceWeapons.Count >= MaxOnceWeaponCount - 1 || once.GetLevel() == 3)
            return;

        int itemName = (int)once.type;

        for (int i = 0; i < onceWeapons.Count; ++i)
        {
            if (!onceWeapons.Contains(onceweapon))
            {
                onceWeapons.Push(onceweapon);
                break;
            }
            else
            {
                if ((int)onceWeapons.ToArray()[i].GetComponent<OnceWeapon>().type == itemName)
                    onceWeapons.ToArray()[i].GetComponent<OnceWeapon>().PlusLevel(1);
            }
        }
    }

    public int GetOnceWeaponCount()
    {
        return onceWeapons.Count;
    }

    #endregion Weapon
}
