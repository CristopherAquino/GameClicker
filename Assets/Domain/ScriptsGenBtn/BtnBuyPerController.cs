using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BreakInfinity;

public class BtnBuyPerController : MonoBehaviour
{
    //Numeric Variable
    public BigDouble NumPerBuy;
    public BigDouble MaxBuyPer;

    //flags
    bool f1 = false;
    bool f2 = false;
    public bool f3 = false;

    //UI Variable
    public TMP_Text BtnPerDisplay;

    //Variable on other class
    public GenerateCoins Coins;
    public WoodBuyBtnController Wood;
    public StoneBuyBtnController Stone;
    public IronBuyBtnController Iron;
    public BankBuyBtnController Bank;

    // Start is called before the first frame update
    void Start()
    {
        NumPerBuy = 1;
        BtnPerDisplay.text = "" + NotationScript.Notate(NumPerBuy);
    }

    public void MAXxOne(BigDouble coin, BigDouble cost)
    {
        BigDouble cant;
        cant = coin.Truncate() / cost.Truncate();
        NumPerBuy = cant.Truncate();
    }
    public void MAXxMul(BigDouble resource1, BigDouble resource2, BigDouble costresource1, BigDouble costresource2)
    {
        BigDouble cant1,cant2;
        cant1 = resource1.Truncate() / costresource1.Truncate();
        cant2 = resource2.Truncate() / costresource2.Truncate();
        if (cant1<=cant2)
        {
            NumPerBuy = cant1.Truncate();
        }
        else
        {
            NumPerBuy = cant2.Truncate();
        }
    }

    public void MAXx3(BigDouble r1, BigDouble r2,BigDouble r3,
        BigDouble costr1, BigDouble costr2, BigDouble costr3)
    {
        BigDouble cant1, cant2, cant3;
        cant1 = r1.Truncate() / costr1.Truncate();
        cant2 = r2.Truncate() / costr2.Truncate();
        cant3 = r3.Truncate() / costr3.Truncate();
        if (cant1 <= cant2 && cant1 <= cant3)
        {
            NumPerBuy = cant1.Truncate();
        }
        else if (cant2 <= cant3)
        {
            NumPerBuy = cant2.Truncate();
        }
        else
        {
            NumPerBuy = cant3.Truncate();
        }
    }

    public void ChangeQuanBuy()
    {
        if(NumPerBuy == 1)
        {
            f1 = true;
            NumPerBuy = 10;
            BtnPerDisplay.text = "" + NotationScript.Notate(NumPerBuy);
        }
        else if (f1 == true)
        {
            f1 = false;
            f2 = true;
            NumPerBuy = 100;
            BtnPerDisplay.text = "" + NotationScript.Notate(NumPerBuy);
        }
        else if (f2 == true)
        {
            f2 = false;
            f3 = true;
            BtnPerDisplay.text = "MAX";
        }
        else if (f3 == true)
        {
            f3 = false;
            NumPerBuy = 1;
            BtnPerDisplay.text = "" + NotationScript.Notate(NumPerBuy);
        }
    }
}
