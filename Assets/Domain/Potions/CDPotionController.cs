using BreakInfinity;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CDPotionController : MonoBehaviour
{
    //Numeric Variable
    public BigDouble CostCoin;
    public BigDouble CostMonsterEssence;

    //UI Variables
    public TMP_Text CostCoinDisplay;
    public TMP_Text CostMonsterEssenceDisplay;
    public TMP_Text CDPotionQuantityDisplay;
    public TMP_Text BuyCDPBtnDisplay;

    //Variable on other class
    public GenerateCoins Coins;
    public StoneBuyBtnController Stone;
    public BtnPotionMAXController Buy;
    public PotionsController potion;

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
        CDPotionQuantityDisplay.text = "" + NotationScript.Notate(potion.CDPotionQuantity);

        if (Buy.f3 == true)
        {
            Buy.MAXxMul(Coins.Coins, Stone.Stone, CostCoin, CostMonsterEssence);
            temp = Buy.NumxBuy;
            BuyCDPBtnDisplay.text = "BUY" + Environment.NewLine + "x" + NotationScript.Notate(temp);
        }
        else
        {
            temp = Buy.NumxBuy;
            BuyCDPBtnDisplay.text = "BUY" + Environment.NewLine + "x" + NotationScript.Notate(temp);
        }
    }

    public void BuyCDPotion()
    {
        if (Buy.f3 == true)
        {
            Buy.MAXxMul(Coins.Coins, Stone.Stone, CostCoin, CostMonsterEssence);
            BuyComparator();
        }
        else
        {
            BuyComparator();
        }
    }

    void BuyComparator()
    {
        if (Coins.Coins >= (CostCoin * Buy.NumxBuy) &&
            Stone.Stone >= (CostMonsterEssence * Buy.NumxBuy))
        {
            potion.CDPotionQuantity += (1 * Buy.NumxBuy);
            Coins.Coins -= (CostCoin * Buy.NumxBuy);
            Stone.Stone -= (CostMonsterEssence * Buy.NumxBuy);
        }
    }

}