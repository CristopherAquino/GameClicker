using BreakInfinity;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class BankBuyBtnController : MonoBehaviour
{
    //Numeric Variable
    public BigDouble BankLvl;
    public BigDouble CostCoin;
    public BigDouble CostStone;
    public BigDouble CoinsPerSecond;

    //UI Variables
    public TMP_Text CostCoinDisplay;
    public TMP_Text CostStoneDisplay;
    public TMP_Text BankLvlDisplay;
    public TMP_Text CoinsPerSecondDisplay;
    public TMP_Text BuyBankBtnDisplay;

    //Variable on other class
    public GenerateCoins Coins;
    public StoneBuyBtnController Stone;
    public BtnBuyPerController Buy;

    // Start is called before the first frame update
    void Start()
    {
        CostCoin = 10;
        CostStone = 10;
        CostCoinDisplay.text = "" + NotationScript.Notate(CostCoin);
        CostStoneDisplay.text = "" + NotationScript.Notate(CostStone);
    }

    // Update is called once per frame
    void Update()
    {
        BigDouble temp;
        CostCoinDisplay.text = "" + NotationScript.Notate(CostCoin);
        CostStoneDisplay.text = "" + NotationScript.Notate(CostStone);
        BankLvlDisplay.text = "" + NotationScript.Notate(BankLvl);
        CoinsPerSecondDisplay.text = NotationScript.Notate(CoinsPerSecond) + "/s";

        CoinsPerSecond = 2 * BankLvl;
        Coins.Coins += CoinsPerSecond / 25;

        if (Buy.f3 == true)
        {
            Buy.MAXxMul(Coins.Coins, Stone.Stone, CostCoin, CostStone);
            temp = Buy.NumPerBuy;
            BuyBankBtnDisplay.text = "BUY" + Environment.NewLine + "x"+NotationScript.Notate(temp);
        }
        else
        {
            temp = Buy.NumPerBuy;
            BuyBankBtnDisplay.text = "BUY" + Environment.NewLine + "x" + NotationScript.Notate(temp);
        }

    }

    public void BuyBanks()
    {
        if (Buy.f3 == true)
        {
            Buy.MAXxMul(Coins.Coins, Stone.Stone, CostCoin, CostStone);

            if (Coins.Coins >= (CostCoin * Buy.NumPerBuy) &&
                Stone.Stone >= (CostStone * Buy.NumPerBuy))
            {
                BankLvl += (1 * Buy.NumPerBuy);
                Coins.Coins -= (CostCoin * Buy.NumPerBuy);
                Stone.Stone -= (CostStone * Buy.NumPerBuy);
            }
        }
        else
        {
            if (Coins.Coins >= (CostCoin * Buy.NumPerBuy) &&
                Stone.Stone >= (CostStone * Buy.NumPerBuy))
            {
                BankLvl += (1 * Buy.NumPerBuy);
                Coins.Coins -= (CostCoin * Buy.NumPerBuy);
                Stone.Stone -= (CostStone * Buy.NumPerBuy);
            }
        }
    }
}
