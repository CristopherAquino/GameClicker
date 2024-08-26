using BreakInfinity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AoHHBtnController : MonoBehaviour
{
    //Numeric Variable
    public BigDouble AoHHLvl;
    public BigDouble CostMonsterBone;
    public BigDouble CostMonsterEssence;
    int max = 20;
    BigDouble temp;

    //UI Variables
    public TMP_Text CostMonsterBoneDisplay;
    public TMP_Text CostMonsterEssenceDisplay;
    public TMP_Text AoHHLvlDisplay;
    public TMP_Text CRDisplay;
    public TMP_Text BuyAoHHBtnDisplay;
    public Button Button;

    //Variable on other class
    public MonsterResourcesController monster;
    public BtnMAXController Buy;
    public PlayerStats Player;

    // Start is called before the first frame update
    void Start()
    {
        CostMonsterEssence = 10;
        CostMonsterBone = 10;
        CostMonsterEssenceDisplay.text = "" + NotationScript.Notate(CostMonsterEssence);
        CostMonsterBoneDisplay.text = "" + NotationScript.Notate(CostMonsterBone);
    }

    // Update is called once per frame
    void Update()
    {
        BigDouble t2, t3;
        CostMonsterEssenceDisplay.text = "" + NotationScript.Notate(CostMonsterEssence);
        CostMonsterBoneDisplay.text = "" + NotationScript.Notate(CostMonsterBone);
        AoHHLvlDisplay.text =""+ NotationScript.Notate(AoHHLvl) + "/" + max;
        CRDisplay.text = NotationScript.Notate(Player.CriticalRate) + "";


        if (AoHHLvl == max)
        {
            BuyAoHHBtnDisplay.text = "BUY" + Environment.NewLine + "x" + NotationScript.Notate(0);
            Button.interactable = false;
        }

        if (Buy.f3 == true)
        {
            Buy.MAXxMul(monster.MonsterEssence, monster.MonsterBone, CostMonsterEssence, CostMonsterBone);
            limit(AoHHLvl, max);
        }
        else
        {
            t2 = Buy.NumxBuy + AoHHLvl;
            t3 = Buy.NumxBuy;
            if (t2 > max)
            {
                t3 = max - AoHHLvl;
            }
            BuyAoHHBtnDisplay.text = "BUY" + Environment.NewLine + "x" + NotationScript.Notate(t3);
            temp = t3;
        }
    }

    void BuyComparator(BigDouble buys)
    {
        if (monster.MonsterEssence >= (CostMonsterEssence * buys) &&
            monster.MonsterBone >= (CostMonsterBone * buys))
        {
            AoHHLvl += (1 * buys);
            monster.MonsterEssence -= (CostMonsterEssence * buys);
            monster.MonsterBone -= (CostMonsterBone * buys);

            int LvlTemp = (int)AoHHLvl.ToDouble(); 
            Player.CriticalRate = LvlTemp * 5;
        }
    }

    public void UpgradeAoHH()
    {
        if (Buy.f3 == true)
        {
            BuyComparator(temp);
        }
        else
        { 
            BuyComparator(temp);
        }
    }

    void limit(BigDouble QuantityInUse, BigDouble maxLimitPerObject)
    {
        // Calculate remaining quantity that can be bought
        BigDouble remainingQuantity = maxLimitPerObject - QuantityInUse;

        // Calculate the maximum quantity that can be bought based on money available
        BigDouble maxQuantityToBuy = Buy.NumxBuy;

        // Limit the purchase to the maximum allowed per click
        BigDouble quantityToBuy = BigDouble.Min(maxQuantityToBuy,maxLimitPerObject);

        // Check if the remaining quantity allows for the purchase
        if (remainingQuantity >= quantityToBuy)
        {
            BuyAoHHBtnDisplay.text = "BUY" + Environment.NewLine + "x" + NotationScript.Notate(quantityToBuy);
            temp = quantityToBuy;
        }
    }
}
