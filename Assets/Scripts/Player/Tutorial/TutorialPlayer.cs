using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TutorialPlayer : MonoBehaviour
{
    [Header("Info")]
    public float Speed;
    public float MaxHp;
    public float CurrentHp;

    [Header("Movement")]
    Movement move;
    Animator animator;

    [Header("Weapon")]
    public int MaxOnceWeaponCount = 3;
    public GameObject itemEffect;
    Queue<int> onceWeapons;
    OnceWeapon once;
    Weapon weapon;
    bool isUse = false;
    bool isMove = true;
    float effectTime = 0;

    [Header("UI")]
    public GameObject cameraUI;
    UIManager uiManager;

    private void Start()
    {
        animator = GetComponent<Animator>();
        move = GetComponent<Movement>();
        weapon = GetComponentInChildren<Weapon>();
        once = GetComponentInChildren<OnceWeapon>();
        onceWeapons = new Queue<int>();
        uiManager = FindObjectOfType<UIManager>();

        CurrentHp = MaxHp;

        uiManager.SetHealth((int)MaxHp);
    }

    private void Update()
    {
        if (isMove)
            move.Move(Speed);
        CameraWorldSpace();
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

    public void OnPlayerDied()
    {
        this.gameObject.SetActive(false);
    }

    #region Weapon

    private void OnFire(InputValue value)
    {
        if (isUse)
            return;

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
        if (onceWeapons.Count <= 0 || isUse)
            return;

        int itemType;
        itemType = onceWeapons.Dequeue();
        once.OnUse(itemType);

        switch (itemType)
        {
            case (int)WeaponType.Jabberwocky:
                animator.SetFloat("JabberCount", 0.4f);
                animator.SetBool("IsJabber", true);
                break;
            case (int)WeaponType.Flamingo:
                animator.SetBool("IsFlamingo", true);
                isMove = false;
                break;
        }

        uiManager.UseItem();
    }

    public void AddOnceWeapon(int itemName)
    {
        itemEffect.SetActive(true);
        effectTime = 0;
        StartCoroutine(OnItemEffect());

        if (onceWeapons.Count >= MaxOnceWeaponCount || once.GetLevel(itemName) == 3)
            return;

        if (itemName == (int)WeaponType.Key)
        {
            int randNum = Random.Range(0, 3);
            itemName = randNum;
        }

        if (onceWeapons.Contains(itemName) == false)
        {
            onceWeapons.Enqueue(itemName);
            uiManager.GetItem((WeaponType)itemName, once.GetLevel(itemName));
        }
        else if (onceWeapons.Contains(itemName) == true)
        {
            once.SetLevel(itemName, 1, true);
        }
    }

    public int GetOnceWeaponCount()
    {
        return onceWeapons.Count;
    }

    public void SetIsUse(bool isUse)
    {
        this.isUse = isUse;
    }

    public void SetIsMove(bool isMove)
    {
        this.isMove = isMove;
    }

    IEnumerator OnItemEffect()
    {
        while (effectTime < 0.06f)
        {
            effectTime += Time.unscaledDeltaTime;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        itemEffect.SetActive(false);
        yield return 0;
    }

    #endregion Weapon
}