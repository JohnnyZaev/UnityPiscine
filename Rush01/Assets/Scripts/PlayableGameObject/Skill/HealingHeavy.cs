﻿using UnityEngine;

public class HealingHeavy : Skill
{
    public float coolDownTime;
    public float powerRegen;
    private float _oldTimeActivate;
    [SerializeField] private ParticleSystem _ps;
    
    public void levelUpSkill()
    {
        if (levelSkill < maxLvlSkill)
        {
            levelSkill += 1;
            powerRegen += (powerRegen * 0.50f);
            coolDownTime -= (coolDownTime * 0.1f);
            GamaManager.gm.upgradeSkillDone();
        }
    }

    public void Update()
    {
        if (!isActive && Time.time - _oldTimeActivate > coolDownTime)
        {
            isActive = true;
        }
    }
    
    public override void action(AliveObject aliveObject)
    {
        if (isActive) {
            float hp = aliveObject.hp;
            float maxHp = aliveObject.maxHp;
            if (hp > 0)
            {
                hp += (maxHp * powerRegen);
                if (hp > maxHp)
                {
                    hp = maxHp;
                }
                aliveObject.hp = System.Convert.ToInt32(hp);
            }
            isActive = false;
            _oldTimeActivate = Time.time;
            _ps.Play();
        }
    }
    
    public override string getInfo()
    {
        return $"Regen hp {(GamaManager.gm.pc.maxHp * powerRegen)} CD {coolDownTime} seconds ";
    }
    
    public override string getInfoLevelNext()
    {
        float tmpPowerRegen = powerRegen + (powerRegen * 0.50f);
        float tmpCoolDownTime = coolDownTime - (coolDownTime * 0.1f);
            
        return $"Regen hp {(GamaManager.gm.pc.maxHp * tmpPowerRegen)} CD {tmpCoolDownTime} seconds ";
    }
}
