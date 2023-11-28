using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TutorialPlayer : Player, ITakeDamage
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
    TutorialOnceWeapon once;
    Weapon weapon;
    bool isUse;
    bool isMove;

    [Header("UI")]
    public GameObject cameraUI;
    UIManager uiManager;

    Tutorial tutorial;
    public int scriptCount = 0;
    public int count = 0;
    int scriptNum = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
        move = GetComponent<Movement>();
        weapon = GetComponentInChildren<Weapon>();
        once = GetComponentInChildren<TutorialOnceWeapon>();
        onceWeapons = new Queue<int>();
        uiManager = FindObjectOfType<UIManager>();
        tutorial = FindObjectOfType<Tutorial>();

        CurrentHp = MaxHp;

        uiManager.SetHealth((int)MaxHp);
        isMove = true;
        isUse = true;
        StartCoroutine(TutorialScript());
    }

    private void Update()
    {
        if (isMove)
            move.Move(Speed);

        if (count == 3 && scriptCount >= 1)
        {
            scriptCount = 0;
            tutorial.StartChat();
        }

        CameraWorldSpace();
    }



    public void TakeDamage(float value)
    {
    }

    public void SetHp(float hp)
    {
        if (CurrentHp + hp > 3)
            CurrentHp = 3;
        else
            CurrentHp += hp;
        uiManager.SetHealth((int)CurrentHp);
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

    IEnumerator TutorialScript()
    {
        while (true)
        {
            switch (count)
            {
                case 0:
                    if (animator.GetFloat("MoveDirection") > 0 || animator.GetFloat("MoveDirection") < 0)
                        scriptCount++;
                    if (scriptCount >= 4)
                    {
                        tutorial.StartChat();
                        scriptCount = 0;
                        isUse = false;
                        count++;
                    }
                    break;
                case 1:
                    if (scriptCount >= 1)
                    {
                        tutorial.StartChat();
                        count++;
                    }
                    break;
                case 2:
                    if (scriptCount >= 1)
                    {
                        tutorial.StartChat();
                        scriptCount = 0;
                        count++;
                    }
                    break;
            }

            yield return new WaitForSeconds(0.1f);
        }

    }

    public void SetCount()
    {
        scriptCount++;
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

        FindObjectOfType<TutorialHit>().hit();

        switch (itemType)
        {
            case (int)WeaponType.Flamingo:
                animator.SetBool("IsFlamingo", true);
                isMove = false;
                break;
        }

        uiManager.UseItem();

        if (count == 2)
            scriptCount++;
    }

    public void AddOnceWeapon(int itemName)
    {
        itemEffect.SetActive(true);
        StartCoroutine(OnItemEffect());

        if (onceWeapons.Count >= MaxOnceWeaponCount || once.GetLevel(itemName) == 3)
            return;

        if (itemName == (int)WeaponType.Key)
        {
            int randNum = (int)WeaponType.Flamingo;
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
        while (true)
        {
            if (itemEffect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("GetItemAnim") &&
                itemEffect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                itemEffect.SetActive(false);
            }

            yield return new WaitForSecondsRealtime(0.01f);
        }
    }

    #endregion Weapon
}