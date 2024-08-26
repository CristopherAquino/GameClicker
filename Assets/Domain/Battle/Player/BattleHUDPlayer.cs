using BreakInfinity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BattleHUDPlayer : MonoBehaviour
{
    public TMP_Text levelText;
    public Slider hpSlider;
    public TMP_Text HPDisplay;
    public TMP_Text ATKDisplay;
    public TMP_Text DEFDisplay;
    public TMP_Text CRDisplay;
    public TMP_Text CDDisplay;
    public TMP_Text LMDisplay;

    public TMP_Text AtkPotion;
    public TMP_Text DefPotion;
    public TMP_Text CDPotion;
    public TMP_Text CRPotion;
    public TMP_Text HealPotion;
    public TMP_Text TimeText;

    float timeSinceLastUpdate = 0f;
    float updateTime = 1f; // Update time interval in seconds
    public string timeDisplay;

    public bool flagAtk = false;
    public bool flagDef = false;
    public bool flagCR = false;
    public bool flagCD = false;

    public Button healbtn;
    public Button atkbtn;
    public Button defbtn;
    public Button crbtn;
    public Button cdbtn;

    BigDouble maxhp;
    BigDouble currenthp;

    public PlayerStats stat;
    public PotionsController potion;
    public XPManager lv;

    public void SetHUD(PlayerStats player)
    {
        levelText.text = "" + NotationScript.Notate(lv.currentLevel);
        hpSlider.maxValue = 1;

        currenthp = player.currentHP;
        maxhp = player.maxHP;
        SetHP(currenthp);
    }

    public void SetHP(BigDouble current)
    {
        BigDouble fillvalueEnemy = current / maxhp;

        hpSlider.value = (float)fillvalueEnemy.ToDouble();
        if (current <= 0)
        {
            HPDisplay.text = NotationScript.Notate(0)
                + "/" + NotationScript.Notate(maxhp);
        }
        else
        {
            HPDisplay.text = NotationScript.Notate(current)
                + "/" + NotationScript.Notate(maxhp);
        }
    }

    void Update()
    {
        timeSinceLastUpdate += Time.deltaTime; // Track time since last update

        // Only update the display once per second
        if (timeSinceLastUpdate >= updateTime)
        {
            timeSinceLastUpdate -= updateTime; // Reset the time tracker

            ATKDisplay.text = NotationScript.Notate(stat.CurrentAttack);
            DEFDisplay.text = NotationScript.Notate(stat.CurrentDefense);
            CRDisplay.text = NotationScript.Notate(stat.CurrentCR);
            CDDisplay.text = NotationScript.Notate(stat.CurrentCD);
            LMDisplay.text = NotationScript.Notate(stat.LootMultiplier);

            timeDisplay = "";

            if (flagAtk == true && stat.durationA > 0)
            {
                timeDisplay += "ATK Potion (" + stat.durationA-- + "s)\n";
            }
            else
            {
                flagAtk = false;
            }

            if (flagDef == true && stat.durationD > 0)
            {
                timeDisplay += "DEF Potion (" + stat.durationD-- + "s)\n";
            }
            else
            {
                flagDef = false;
            }

            if (flagCR == true && stat.durationCR > 0)
            {
                timeDisplay += "CR Potion (" + stat.durationCR-- + "s)\n";
            }
            else
            {
                flagCR = false;
            }

            if (flagCD == true && stat.durationCD > 0)
            {
                timeDisplay += "CD Potion (" + stat.durationCD-- + "s)\n";
            }
            else
            {
                flagCD = false;
            }

            TimeText.text = timeDisplay.Trim(); // Trim to remove any trailing newline
        }
        ShowPotions();
        interactablePotion();
    }

    public void AdditionalHP()
    {

    }

    public void setNewStart()
    {
        stat.setCurrentStats();
        flags(false);
        TimeText.text = "";
        stat.durationA = 0;
        stat.durationD = 0;
        stat.durationCR = 0;
        stat.durationCD = 0;
    }

    void flags(bool action)
    {
        flagAtk = action;
        flagDef = action;
        flagCR = action;
        flagCD = action;
    }

    void ShowPotions()
    {
        HealPotion.text = NotationScript.Notate(potion.HealPotionQuantity);
        AtkPotion.text = NotationScript.Notate(potion.ATKPotionQuantity);
        DefPotion.text = NotationScript.Notate(potion.DEFPotionQuantity);
        CRPotion.text = NotationScript.Notate(potion.CRPotionQuantity);
        CDPotion.text = NotationScript.Notate(potion.CDPotionQuantity);
    }

    void interactablePotion()
    {
        if(potion.HealPotionQuantity > 0)
        {
            healbtn.interactable = true;
        }
        else
        {
            healbtn.interactable = false;
        }

        if (potion.ATKPotionQuantity > 0 && flagAtk == false)
        {
            atkbtn.interactable = true;
        }
        else
        {
            atkbtn.interactable = false;
        }

        if(potion.DEFPotionQuantity > 0 && flagDef == false)
        {
            defbtn.interactable = true;
        }
        else
        {
            defbtn.interactable = false;
        }

        if (potion.CRPotionQuantity > 0 && flagCR == false)
        {
            crbtn.interactable = true;
        }
        else
        {
            crbtn.interactable = false;
        }

        if (potion.CDPotionQuantity > 0 && flagCD == false)
        {
            cdbtn.interactable = true;
        }
        else
        {
            cdbtn.interactable = false;
        }
    }

}
