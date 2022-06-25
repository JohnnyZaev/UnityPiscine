﻿using System.Collections.Generic;
using UnityEngine;

public class AliveObject : MonoBehaviour
{
    public float strengh;//STR
    public float agility;//AGI
    public float constitution;//CON
    public float armor;
    public int hp;
    public int maxHp;
    public float minDamage;
    public float maxDamage;
    public float level;
    public float exp;
    public float credits;

    public bool isSpawn;
    public bool isDead;
    public bool isExp;
    public List<PassiveSkill> _passiveSkills;

    protected void updateState()
    {
        updateHp();
        updateDamage();
    }

    protected void updateHp()
    {
        hp = System.Convert.ToInt16(constitution * 5.0f);
        maxHp = hp;
    }

    public void UpdateMaxHp()
    {
        maxHp = System.Convert.ToInt16(constitution * 5.0f);
    }

    public void updateDamage()
    {
        minDamage = strengh / 2;
        maxDamage = minDamage + 4;
    }

    protected void passiveSkills()
    {
        foreach (var sk in _passiveSkills)
        {
            sk.action(gameObject);
        }
    }

    public virtual string GetTypeObject()
    {
        return "null";
    }

    public virtual void hit(float damage){}
}
