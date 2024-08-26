using BreakInfinity;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IronBuyBtnController : MonoBehaviour
{
    //Numeric Variable
    public BigDouble Iron;
    public BigDouble CostIron;

    //UI Variables
    public TMP_Text CostIronDisplay;
    public TMP_Text IronDisplay;
    public TMP_Text BuyIronBtnDisplay;

    //Variable on other class
    public GenerateCoins Coins;
    public BtnBuyPerController Buy;

    // Start is called before the first frame update
    void Start()
    {
        CostIron = 10;
        CostIronDisplay.text = "" + NotationScript.Notate(CostIron);
    }

    // Update is called once per frame
    void Update()
    {
        BigDouble temp;

        IronDisplay.text = "" + NotationScript.Notate(Iron);

        if (Buy.f3 == true)
        {
            Buy.MAXxOne(Coins.Coins, CostIron);
            temp = Buy.NumPerBuy;
            BuyIronBtnDisplay.text = "BUY" + Environment.NewLine + "x" + NotationScript.Notate(temp);
        }
        else
        {
            temp = Buy.NumPerBuy;
            BuyIronBtnDisplay.text = "BUY" + Environment.NewLine + "x" + NotationScript.Notate(temp);
        }
    }

    public void BuyIron()
    {
        if (Buy.f3 == true)
        {
            Buy.MAXxOne(Coins.Coins, CostIron);

            if (Coins.Coins >= (CostIron * Buy.NumPerBuy))
            {
                Iron += (1 * Buy.NumPerBuy);
                Coins.Coins -= (CostIron * Buy.NumPerBuy);
            }
        }
        else
        {
            if (Coins.Coins >= (CostIron * Buy.NumPerBuy))
            {
                Iron += (1 * Buy.NumPerBuy);
                Coins.Coins -= (CostIron * Buy.NumPerBuy);
            }
        }
    }
}
