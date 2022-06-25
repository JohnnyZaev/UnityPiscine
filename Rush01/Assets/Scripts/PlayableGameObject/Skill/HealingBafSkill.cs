﻿using UnityEngine;

public class HealingBafSkill : Skill
{
    public float coolDownTime;
    public float timing;
    public float powerRegen;
    private float oldTimeActivate;
    private float oneSecond;
    private float timeToEnd;
    [SerializeField]private bool isHealing;
    [SerializeField] private ParticleSystem _ps;
    private AliveObject _alive;

    public void Start()
    {
        isActive = true;
    }

    public void levelUpSkill()
    {
        if (levelSkill < maxLvlSkill)
        {
            levelSkill += 1;
            coolDownTime = coolDownTime - (coolDownTime * 0.10f);
            powerRegen = powerRegen + (powerRegen * 0.1f);
            GamaManager.gm.upgradeSkillDone();
        }
    }


    public void Update()
    {
        if (!isActive && Time.time - oldTimeActivate > coolDownTime)
        {
            isActive = true;
        }
        if (isHealing && Time.time - timeToEnd > timing)
        {
            isHealing = false;
        }

        if (isHealing && Time.time - oneSecond > 1)
        {
            float hp = _alive.hp;
            float maxHp = _alive.maxHp;
            if (hp > 0)
            {
                hp = hp + (maxHp * powerRegen);
                if (hp > maxHp)
                {
                    hp = maxHp;
                }
                _alive.hp = System.Convert.ToInt32(hp);
                
            }
            _ps.Play();
            oneSecond = Time.time;
        }
    }
    
    public override void action(AliveObject alive)
    {
        if (isActive)
        {
            _alive = alive;
            isActive = false;
            isHealing = true;
            oldTimeActivate = Time.time;
            timeToEnd = Time.time;
        }
    }
    
    public override string getInfo()
    {
        return "Regen hp " + (GamaManager.gm.pc.maxHp * powerRegen) + " CD " + coolDownTime + " seconds ";
    }
    
    public override string getInfoLevelNext()
    {
        float tmpCoolDownTime = coolDownTime - (coolDownTime * 0.10f);
        float tmpPowerRegen = powerRegen + (powerRegen * 0.1f);
            
        return "Regen hp " + (GamaManager.gm.pc.maxHp * tmpPowerRegen) + " CD " + tmpCoolDownTime + " seconds ";
    }
}
