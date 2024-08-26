using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BreakInfinity;
using TMPro;
using System;
using UnityEngine.UI;

public class AoLBtnController : MonoBehaviour
{
    //Numeric Variable
    public BigDouble AoLifeLvl;
    public BigDouble CostCoin;
    public BigDouble CostMonsterEssence;

    //UI Variables
    public TMP_Text CostCoinDisplay;
    public TMP_Text CostMonsterEssenceDisplay;
    public TMP_Text AoLifeLvlDisplay;
    public TMP_Text HPDisplay;
    public TMP_Text BuyAoLBtnDisplay;
    public Button Btn;

    //Variable on other class
    public GenerateCoins Coins;
    public MonsterResourcesController Monster;
    public BtnMAXController Buy;
    public PlayerStats Player;

    // Start is called before the first frame update
    void Start()
    {
        CostCoin = 10;
        CostMonsterEssence = 10;
        CostCoinDisplay.text = "" + NotationScript.Notate(CostCoin);
        CostMonsterEssenceDisplay.text = "" + NotationScript.Notate(CostMonsterEssence);
    }

    // Update is called once per frame
    void Update()
    {
        BigDouble temp;
        CostCoinDisplay.text = "" + NotationScript.Notate(CostCoin);
        CostMonsterEssenceDisplay.text = "" + NotationScript.Notate(CostMonsterEssence);
        AoLifeLvlDisplay.text = "" + NotationScript.Notate(AoLifeLvl);
        HPDisplay.text = NotationScript.Notate(Player.maxHP) + ""; 

        if (Buy.f3 == true)
        {
            Buy.MAXxMul(Monster.MonsterEssence, Coins.Coins, CostMonsterEssence, CostCoin);
            temp = Buy.NumxBuy;
            BuyAoLBtnDisplay.text = "BUY" + Environment.NewLine + "x" + NotationScript.Notate(temp);
        }
        else
        {
            temp = Buy.NumxBuy;
            BuyAoLBtnDisplay.text = "BUY" + Environment.NewLine + "x" + NotationScript.Notate(temp);
        }
    }

    public void UpgradeAoL()
    {
        if (Buy.f3 == true)
        {
            Buy.MAXxMul(Monster.MonsterEssence, Coins.Coins, CostMonsterEssence, CostCoin);
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
            Coins.Coins >= (CostCoin * Buy.NumxBuy))
        {
            AoLifeLvl += (1 * Buy.NumxBuy);
            Coins.Coins -= (CostCoin * Buy.NumxBuy);
            Monster.MonsterEssence -= (CostMonsterEssence * Buy.NumxBuy);

            Player.maxHP = (Player.maxHP + (AoLifeLvl * 100));
        }
    }
}
