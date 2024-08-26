using BreakInfinity;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealPotionController : MonoBehaviour
{
    //Numeric Variable
    public BigDouble CostCoin;
    public BigDouble CostMonsterEssence;

    //UI Variables
    public TMP_Text CostCoinDisplay;
    public TMP_Text CostMonsterEssenceDisplay;
    public TMP_Text HealPotionQuantityDisplay;
    public TMP_Text BuyHealPBtnDisplay;

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
        HealPotionQuantityDisplay.text = "" + NotationScript.Notate(potion.HealPotionQuantity);

        if (Buy.f3 == true)
        {
            Buy.MAXxMul(Coins.Coins, Stone.Stone, CostCoin, CostMonsterEssence);
            temp = Buy.NumxBuy;
            BuyHealPBtnDisplay.text = "BUY" + Environment.NewLine + "x" + NotationScript.Notate(temp);
        }
        else
        {
            temp = Buy.NumxBuy;
            BuyHealPBtnDisplay.text = "BUY" + Environment.NewLine + "x" + NotationScript.Notate(temp);
        }
    }

    public void BuyHealPotion()
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
            potion.HealPotionQuantity += (1 * Buy.NumxBuy);
            Coins.Coins -= (CostCoin * Buy.NumxBuy);
            Stone.Stone -= (CostMonsterEssence * Buy.NumxBuy);
        }
    }
}
