using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [Header("Weapon Info")]
    protected int WeaponId;                                     // ���� ID
    protected float AttackRange;                                // ���� ����
    [SerializeField] protected float AttackPoint;               // ���ݷ�
    [SerializeField] protected float AttackSpeed;               // ���ݼӵ�
    [SerializeField] protected float ProjectileSpeed;           // ����ü �ӵ�
    [SerializeField] protected GameObject ProjectileObject;     // ����ü ������
    [SerializeField] protected int MaxAttack;                   // �ִ� ���� Ƚ��
    protected int CurrentAttack = 0;                            // ���� ���� Ƚ��

    protected virtual void Awake()
    {
    }

    public float GetAtkSpd()
    {
        return AttackSpeed;
    }
}