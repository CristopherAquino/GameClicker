using BreakInfinity;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SawmillBuyBtnController : MonoBehaviour
{
    //Numeric Variable
    public BigDouble SawmillLvl;
    public BigDouble CostCoin;
    public BigDouble CostIron;
    public BigDouble WoodPerSecond;

    //UI Variables
    public TMP_Text CostCoinDisplay;
    public TMP_Text CostIronDisplay;
    public TMP_Text SawmillLvlDisplay;
    public TMP_Text WoodPerSecondDisplay;
    public TMP_Text BuySawmillBtnDisplay;

    //Variable on other class
    public GenerateCoins Coins;
    public IronBuyBtnController Iron;
    public WoodBuyBtnController Wood;
    public BtnBuyPerController Buy;

    // Start is called before the first frame update
    void Start()
    {
        CostCoin = 10;
        CostIron = 10;
        CostCoinDisplay.text = "" + NotationScript.Notate(CostCoin);
        CostIronDisplay.text = "" + NotationScript.Notate(CostIron);
    }

    // Update is called once per frame
    void Update()
    {
        BigDouble temp;
        CostCoinDisplay.text = "" + NotationScript.Notate(CostCoin);
        CostIronDisplay.text = "" + NotationScript.Notate(CostIron);
        SawmillLvlDisplay.text = "" + NotationScript.Notate(SawmillLvl);
        WoodPerSecondDisplay.text = NotationScript.Notate(WoodPerSecond) + "/s";

        WoodPerSecond = 2 * SawmillLvl;
        Wood.Wood += WoodPerSecond / 25;

        if (Buy.f3 == true)
        {
            Buy.MAXxMul(Coins.Coins, Iron.Iron, CostCoin, CostIron);
            temp = Buy.NumPerBuy;
            BuySawmillBtnDisplay.text = "BUY" + Environment.NewLine + "x" + NotationScript.Notate(temp);
        }
        else
        {
            temp = Buy.NumPerBuy;
            BuySawmillBtnDisplay.text = "BUY" + Environment.NewLine + "x" + NotationScript.Notate(temp);
        }
    }

    public void BuySawmill()
    {
        if (Buy.f3 == true)
        {
            Buy.MAXxMul(Coins.Coins, Iron.Iron, CostCoin, CostIron);

            if (Coins.Coins >= (CostCoin * Buy.NumPerBuy) &&
                Iron.Iron >= (CostIron * Buy.NumPerBuy))
            {
                SawmillLvl += (1 * Buy.NumPerBuy);
                Coins.Coins -= (CostCoin * Buy.NumPerBuy);
                Iron.Iron -= (CostIron * Buy.NumPerBuy);
            }
        }
        else
        {
            if (Coins.Coins >= (CostCoin * Buy.NumPerBuy) &&
                Iron.Iron >= (CostIron * Buy.NumPerBuy))
            {
                SawmillLvl += (1 * Buy.NumPerBuy);
                Coins.Coins -= (CostCoin * Buy.NumPerBuy);
                Iron.Iron -= (CostIron * Buy.NumPerBuy);
            }
        }
    }
}
