using BreakInfinity;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AoSharpBtnController : MonoBehaviour
{
    //Numeric Variable
    public BigDouble AoSharpnessLvl;
    public BigDouble CostMonsterEye;
    public BigDouble CostMonsterEssence;

    //UI Variables
    public TMP_Text CostMonsterEyeDisplay;
    public TMP_Text CostMonsterEssenceDisplay;
    public TMP_Text AoSharpnessLvlDisplay;
    public TMP_Text CDDisplay;
    public TMP_Text BuyAoSharpBtnDisplay;

    //Variable on other class
    public MonsterResourcesController monster;
    public BtnMAXController Buy;
    public PlayerStats Player;

    // Start is called before the first frame update
    void Start()
    {
        CostMonsterEssence = 10;
        CostMonsterEye = 10;
        CostMonsterEssenceDisplay.text = "" + NotationScript.Notate(CostMonsterEssence);
        CostMonsterEyeDisplay.text = "" + NotationScript.Notate(CostMonsterEye);
    }

    // Update is called once per frame
    void Update()
    {
        BigDouble temp;
        CostMonsterEssenceDisplay.text = "" + NotationScript.Notate(CostMonsterEssence);
        CostMonsterEyeDisplay.text = "" + NotationScript.Notate(CostMonsterEye);
        AoSharpnessLvlDisplay.text = "" + NotationScript.Notate(AoSharpnessLvl);
        CDDisplay.text = NotationScript.Notate(Player.CriticalDamage) + "";

        if (Buy.f3 == true)
        {
            Buy.MAXxMul(monster.MonsterEssence, monster.MonsterEye, CostMonsterEssence, CostMonsterEye);
            temp = Buy.NumxBuy;
            BuyAoSharpBtnDisplay.text = "BUY" + Environment.NewLine + "x" + NotationScript.Notate(temp);
        }
        else
        {
            temp = Buy.NumxBuy;
            BuyAoSharpBtnDisplay.text = "BUY" + Environment.NewLine + "x" + NotationScript.Notate(temp);
        }
    }

    public void UpgradeAoSharp()
    {
        if (Buy.f3 == true)
        {
            Buy.MAXxMul(monster.MonsterEssence, monster.MonsterEye, CostMonsterEssence, CostMonsterEye);
            BuyComparator();
        }
        else
        {
            BuyComparator();
        }
    }

    void BuyComparator()
    {
        if (monster.MonsterEssence >= (CostMonsterEssence * Buy.NumxBuy) &&
            monster.MonsterEye >= (CostMonsterEye * Buy.NumxBuy))
        {
            AoSharpnessLvl += (1 * Buy.NumxBuy);
            monster.MonsterEssence -= (CostMonsterEssence * Buy.NumxBuy);
            monster.MonsterEye -= (CostMonsterEye * Buy.NumxBuy);

            Player.CriticalDamage = (Player.CriticalDamage + (AoSharpnessLvl * 50));

        }
    }
}
