using BreakInfinity;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AoSBtnController : MonoBehaviour
{
    //Numeric Variable
    public BigDouble AoStrenghtLvl;
    public BigDouble CostMonsterMeat;
    public BigDouble CostMonsterEssence;

    //UI Variables
    public TMP_Text CostMonsterMeatDisplay;
    public TMP_Text CostMonsterEssenceDisplay;
    public TMP_Text AoStrenghtLvlDisplay;
    public TMP_Text ATKDisplay;
    public TMP_Text BuyAoSBtnDisplay;

    //Variable on other class
    public MonsterResourcesController Monster;
    public BtnMAXController Buy;
    public PlayerStats Player;

    // Start is called before the first frame update
    void Start()
    {
        CostMonsterEssence = 10;
        CostMonsterMeat = 10;
        CostMonsterEssenceDisplay.text = "" + NotationScript.Notate(CostMonsterEssence);
        CostMonsterMeatDisplay.text = "" + NotationScript.Notate(CostMonsterMeat);
    }

    // Update is called once per frame
    void Update()
    {
        BigDouble temp;
        CostMonsterEssenceDisplay.text = "" + NotationScript.Notate(CostMonsterEssence);
        CostMonsterMeatDisplay.text = "" + NotationScript.Notate(CostMonsterMeat);
        AoStrenghtLvlDisplay.text = "" + NotationScript.Notate(AoStrenghtLvl);
        ATKDisplay.text = NotationScript.Notate(Player.Attack) + "";

        if (Buy.f3 == true)
        {
            Buy.MAXxMul(Monster.MonsterEssence, Monster.MonsterMeat, CostMonsterEssence, CostMonsterMeat);
            temp = Buy.NumxBuy;
            BuyAoSBtnDisplay.text = "BUY" + Environment.NewLine + "x" + NotationScript.Notate(temp);
        }
        else
        {
            temp = Buy.NumxBuy;
            BuyAoSBtnDisplay.text = "BUY" + Environment.NewLine + "x" + NotationScript.Notate(temp);
        }
    }

    public void UpgradeAoS()
    {
        if (Buy.f3 == true)
        {
            Buy.MAXxMul(Monster.MonsterEssence, Monster.MonsterMeat, CostMonsterEssence, CostMonsterMeat);
            BuyComparator();
        }
        else
        {
            BuyComparator();
        }
    }

    void BuyComparator()
    {
        if (Monster.MonsterEssence >= (CostMonsterEssence * Buy.NumxBuy) &&
            Monster.MonsterMeat >= (CostMonsterMeat * Buy.NumxBuy))
        {
            AoStrenghtLvl += (1 * Buy.NumxBuy);
            Monster.MonsterEssence -= (CostMonsterEssence * Buy.NumxBuy);
            Monster.MonsterMeat -= (CostMonsterMeat * Buy.NumxBuy);

            Player.Attack = (Player.Attack + (AoStrenghtLvl * 15));
        }
    }
}
