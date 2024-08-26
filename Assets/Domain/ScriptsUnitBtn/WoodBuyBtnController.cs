using BreakInfinity;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WoodBuyBtnController : MonoBehaviour
{
    //Numeric Variable
    public BigDouble Wood;
    public BigDouble CostWood;

    //UI Variables
    public TMP_Text CostWoodDisplay;
    public TMP_Text WoodDisplay;
    public TMP_Text BuyWoodBtnDisplay;

    //Variable on other class
    public GenerateCoins Coins;
    public BtnBuyPerController Buy;

    // Start is called before the first frame update
    void Start()
    {
        CostWood = 5;
        CostWoodDisplay.text = "" + NotationScript.Notate(CostWood);
    }

    // Update is called once per frame
    void Update()
    {
        BigDouble temp;

        WoodDisplay.text = "" + NotationScript.Notate(Wood);

        if (Buy.f3 == true)
        {
            Buy.MAXxOne(Coins.Coins, CostWood);
            temp = Buy.NumPerBuy;
            BuyWoodBtnDisplay.text = "BUY" + Environment.NewLine + "x" + NotationScript.Notate(temp);
        }
        else
        {
            temp = Buy.NumPerBuy;
            BuyWoodBtnDisplay.text = "BUY" + Environment.NewLine + "x" + NotationScript.Notate(temp);
        }
    }

    public void BuyWood()
    {
        if (Buy.f3 == true)
        {
            Buy.MAXxOne(Coins.Coins, CostWood);

            if (Coins.Coins >= (CostWood * Buy.NumPerBuy))
            {
                Wood += (1 * Buy.NumPerBuy);
                Coins.Coins -= (CostWood * Buy.NumPerBuy);
            }
        }
        else
        {
            if (Coins.Coins >= (CostWood * Buy.NumPerBuy))
            {
                Wood += (1 * Buy.NumPerBuy);
                Coins.Coins -= (CostWood * Buy.NumPerBuy);
            }
        }
    }
}
