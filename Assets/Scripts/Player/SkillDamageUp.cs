using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillDamageUp : MonoBehaviour
{
    float damage = 10;

    private void Start()
    {
        if (this.GetComponent<BaseSkill>() == null)
            return;
        this.GetComponent<BaseSkill>().SetDamage(damage);
    }
}
