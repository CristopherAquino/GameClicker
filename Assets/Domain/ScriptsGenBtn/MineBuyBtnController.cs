using BreakInfinity;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MineBuyBtnController : MonoBehaviour
{
    //Numeric Variable
    public BigDouble MineLvl;
    public BigDouble CostCoin;
    public BigDouble CostWood;
    public BigDouble StonePerSecond;
    public BigDouble IronPerSecond;

    //UI Variables
    public TMP_Text CostCoinDisplay;
    public TMP_Text CostWoodDisplay;
    public TMP_Text MineLvlDisplay;
    public TMP_Text StonePerSecondDisplay;
    public TMP_Text IronPerSecondDisplay;
    public TMP_Text BuyMineBtnDisplay;

    //Variable on other class
    public GenerateCoins Coins;
    public WoodBuyBtnController Wood;
    public StoneBuyBtnController Stone;
    public IronBuyBtnController Iron;
    public BtnBuyPerController Buy;

    // Start is called before the first frame update
    void Start()
    {
        CostCoin = 10;
        CostWood = 10;
        CostCoinDisplay.text = "" + NotationScript.Notate(CostCoin) ;
        CostWoodDisplay.text = "" + NotationScript.Notate(CostWood);
    }

    void Update()
    {
        BigDouble temp;
        CostCoinDisplay.text = "" + NotationScript.Notate(CostCoin);
        CostWoodDisplay.text = "" + NotationScript.Notate(CostWood);
        MineLvlDisplay.text = "" + NotationScript.Notate(MineLvl);
        StonePerSecondDisplay.text = NotationScript.Notate(StonePerSecond) + "/s";
        IronPerSecondDisplay.text = NotationScript.Notate(IronPerSecond) + "/s";

        StonePerSecond = 2 * MineLvl;
        IronPerSecond = 2 * MineLvl;
        Stone.Stone += StonePerSecond / 25;
        Iron.Iron += IronPerSecond / 25;

        if (Buy.f3 == true)
        {
            Buy.MAXxMul(Coins.Coins, Wood.Wood, CostCoin, CostWood);
            temp = Buy.NumPerBuy;
            BuyMineBtnDisplay.text = "BUY" + Environment.NewLine + "x" + NotationScript.Notate(temp);
        }
        else
        {
            temp = Buy.NumPerBuy;
            BuyMineBtnDisplay.text = "BUY" + Environment.NewLine + "x" + NotationScript.Notate(temp);
        }
    }

    public void BuyMines()
    {
        if (Buy.f3 == true)
        {
            Buy.MAXxMul(Coins.Coins, Wood.Wood, CostCoin, CostWood);

            if (Coins.Coins >= (CostCoin * Buy.NumPerBuy) &&
                Wood.Wood >= (CostWood * Buy.NumPerBuy))
            {
                MineLvl += (1 * Buy.NumPerBuy);
                Coins.Coins -= (CostCoin * Buy.NumPerBuy);
                Wood.Wood -= (CostWood * Buy.NumPerBuy);
            }
        }
        else
        {
            if (Coins.Coins >= (CostCoin * Buy.NumPerBuy) &&
                Wood.Wood >= (CostWood * Buy.NumPerBuy))
            {
                MineLvl += (1 * Buy.NumPerBuy);
                Coins.Coins -= (CostCoin * Buy.NumPerBuy);
                Wood.Wood -= (CostWood * Buy.NumPerBuy);
            }
        }
    }
}
