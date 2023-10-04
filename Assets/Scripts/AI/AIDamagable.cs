using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDamagable : MonoBehaviour, ITakeDamage
{
    [SerializeField] float MaxHp;
    float currentHp;

    private void OnEnable()
    {
        currentHp = MaxHp;
    }

    public void TakeDamage(float value)
    {
        currentHp -= value;
        if(currentHp <= 0)
        {
            ObjectPoolManager.ReturnObjectToPool(this.gameObject);
        }
    }

}
