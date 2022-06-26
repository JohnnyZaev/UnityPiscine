﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamaManager : MonoBehaviour
{
    // View Player
    public Text textXp;
    public Text textHp;
    public Text textLevel;
    public Slider hpSlider;
    public Slider xpSlider;
    public PlayerController pc;
    public InventoryManager im;

    // Panel
    public GameObject panelPlayer;
    public GameObject panelGameOver;
    public GameObject panelCharacter;
    public GameObject panelTalent;
    
    // Target Enemy
    public Slider hpEnemy;
    public Text textHpEnemy;
    public Text textTargetEnemyName;
    public Text textLvlEnemy;
    public List<GameObject> _enemyView;

    // Character View
    public bool isCharacterPanel;
    public Text textName;
    public Text textStrengh;
    public Text textAgility;
    public Text textConstitution;
    public Text textArmor;
    public Text textUpgradePoints;
    public Text textAmountPointsTalents;
    public Text textMinMaxDamage;
    public Text textMaxHp;
    public Text textEXpCharacter;
    public Text textXpToNextLevel;
    public Text textCredits;
    
    // Talent View
    public bool isTalentPanel;
    public bool isStaticPlayer;
    public Text textAmountTalentPoint;
    public bool isSelectedSkill;
    public DragSkill dragSkill;
    public List<GameObject> _level;
    
    // buttons
    public GameObject upgradeButton;
    public GameObject upgradeButtonStrange;
    public GameObject upgradeButtonAgility;
    public GameObject upgradeButtonCON;

    public List<GameObject> imagesIconSkill;

    
    public static GamaManager gm;
    public GameObject image1;
    public GameObject image2;

    private void Awake () {
        if (gm == null)
            gm = this;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            pc.cheatLevelUp();
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            onSwapCharacterPanel();
        }
        if (Input.GetKeyUp(KeyCode.N))
        {
            onSwapTalentPanel();
        }
        if (Input.GetKeyUp(KeyCode.I))
        {
            im.ChangeInventoryStatus();
        }
        visibleViewGame();
        visibleCharacterPanel();
        visibleTalentPanel();
        upgradePointEvent();
        
        if (SkillManager.sk.isDrop)
        {
            imagesIconSkill[SkillManager.sk.number].GetComponent<Image>().sprite = dragSkill.sprite;
            SkillManager.sk.isDrop = false;
            isSelectedSkill = false;
        }
    }
    
    public void targetViewEnemy(bool status)
    {
        foreach (var view in _enemyView)
        {
            view.SetActive(status);
        }
    }
    
    public void target(AliveObject enemy)
    {
        targetViewEnemy(true);
        hpEnemy.maxValue = enemy.maxHp;
        hpEnemy.value = enemy.hp;
        textHpEnemy.text = enemy.hp.ToString();
        textLvlEnemy.text = $"Lv.{enemy.level}";
        textTargetEnemyName.text = enemy.GetTypeObject();
        
    }

    public void upgradeStreght()
    {
        pc.strengh += 1;
        pc.updateDamage();
        pc.upgradePoint--;
    }

    public void upgradeAgility()
    {
        pc.agility += 1;
        pc.upgradePoint--;
    }

    public void upgradeCON()
    {
        pc.constitution += 1;
        pc.UpdateMaxHp();
        pc.upgradePoint--;
    }

    public void upgradeSkillDone()
    {
        pc.increasePointTalent();
    }

    private void visibleViewGame()
    {
        if (pc.hp > 0)
        {
            textHp.text = pc.hp.ToString();
            textLevel.text = $"Lv.{pc.level}";
            textXp.text = $"{pc.exp}/{(float)pc.nextLevelXp}";
            hpSlider.maxValue = pc.maxHp;
            hpSlider.value = pc.hp;
            xpSlider.maxValue = (float)pc.nextLevelXp;
            xpSlider.value = pc.exp;
        }
        else
        {
            panelPlayer.SetActive(false);
            panelGameOver.SetActive(true);
        }
    }

    private void visibleCharacterPanel()
    {
        if (isCharacterPanel)
        {
            textName.text = $"Maya Lv.{pc.level}";
            textStrengh.text = $"Strengh: {pc.strengh}";
            textAgility.text = $"Agility: {pc.agility}";
            textConstitution.text = $"Constitution: {pc.constitution}";
            textArmor.text = $"Armor: {pc.armor}";
            textUpgradePoints.text = $"Upgrade Points: {pc.upgradePoint}";
            textMinMaxDamage.text = $"Min-Max Damage: {pc.minDamage}-{pc.maxDamage}";
            textMaxHp.text = $"Max Hp: {pc.maxHp}";
            textEXpCharacter.text = $"EXP: {pc.exp}";
            textXpToNextLevel.text = $"Exp to next level: {pc.nextLevelXp}";
            textCredits.text = $"Credits: {pc.credits}";
            textAmountPointsTalents.text = $"Amount Points Talents: {pc.getAmountPointTalent()}";
        }
    }

    private void visibleTalentPanel()
    {
        textAmountTalentPoint.text = $"Amount Points Talents: {pc.getAmountPointTalent()}";
        int count = 0;
        foreach (var t in _level)
        {
	        t.SetActive(true);
	        if (count + 6 > pc.level)
	        {
		        break;
	        }
	        count += 6;
        }
    }

    private void upgradePointEvent()
    {
        if (pc.upgradePoint > 0)
        {
            upgradeButton.SetActive(true);
            upgradeButtonStrange.SetActive(true);
            upgradeButtonAgility.SetActive(true);
            upgradeButtonCON.SetActive(true);
        }
        else
        {
            upgradeButton.SetActive(false);
            upgradeButtonStrange.SetActive(false);
            upgradeButtonAgility.SetActive(false);
            upgradeButtonCON.SetActive(false);
        }
    }
    
    /*
     * Need for button
     */
    public void onSwapCharacterPanel()
    {
        if (!isCharacterPanel)
        {
            panelCharacter.SetActive(true);
            isCharacterPanel = true;
            isStaticPlayer = true;
        }
        else
        {
            panelCharacter.SetActive(false);
            isCharacterPanel = false;
            isStaticPlayer = false;
        }
    }
    
    /*
    * Need for button
    */
    public void onSwapTalentPanel()
    {
        if (!isTalentPanel)
        {
            panelTalent.SetActive(true);
            isTalentPanel = true;
            isStaticPlayer = true;
        }
        else
        {
            panelTalent.SetActive(false);
            isTalentPanel = false;
            isStaticPlayer = false;
            image1.SetActive(false);
            image2.SetActive(false);
        }
    }
}
