using BreakInfinity;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoneBuyBtnController : MonoBehaviour
{
    //Numeric Variable
    public BigDouble Stone;
    public BigDouble CostStone;

    //UI Variables
    public TMP_Text CostStoneDisplay;
    public TMP_Text StoneDisplay;
    public TMP_Text BuyStoneBtnDisplay;

    //Variable on other class
    public GenerateCoins Coins;
    public BtnBuyPerController Buy;

    // Start is called before the first frame update
    void Start()
    {
        CostStone = 5;
        CostStoneDisplay.text = "" + NotationScript.Notate(CostStone);
    }

    // Update is called once per frame
    void Update()
    {
        BigDouble temp;

        StoneDisplay.text = "" + NotationScript.Notate(Stone);

        if (Buy.f3 == true)
        {
            Buy.MAXxOne(Coins.Coins, CostStone);
            temp = Buy.NumPerBuy;
            BuyStoneBtnDisplay.text = "BUY" + Environment.NewLine + "x" + NotationScript.Notate(temp);
        }
        else
        {
            temp = Buy.NumPerBuy;
            BuyStoneBtnDisplay.text = "BUY" + Environment.NewLine + "x" + NotationScript.Notate(temp);
        }
    }

    public void BuyStone()
    {
        if (Buy.f3 == true)
        {
            Buy.MAXxOne(Coins.Coins, CostStone);

            if (Coins.Coins >= (CostStone * Buy.NumPerBuy))
            {
                Stone += (1 * Buy.NumPerBuy);
                Coins.Coins -= (CostStone * Buy.NumPerBuy);
            }
        }
        else
        {
            if (Coins.Coins >= (CostStone * Buy.NumPerBuy))
            {
                Stone += (1 * Buy.NumPerBuy);
                Coins.Coins -= (CostStone * Buy.NumPerBuy);
            }
        }
    }
}
