using BreakInfinity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BtnMAXController : MonoBehaviour
{
    //Numeric Variable
    public BigDouble NumxBuy;

    //flags
    bool f1 = false;
    bool f2 = false;
    public bool f3 = false;

    //UI Variable
    public TMP_Text BtnPerDisplay;

    // Start is called before the first frame update
    void Start()
    {
        NumxBuy = 1;
        BtnPerDisplay.text = "" + NotationScript.Notate(NumxBuy);
    }

    public void MAXxMul(BigDouble resource1, BigDouble resource2, BigDouble costresource1, BigDouble costresource2)
    {
        BigDouble cant1, cant2;
        cant1 = resource1.Truncate() / costresource1.Truncate();
        cant2 = resource2.Truncate() / costresource2.Truncate();
        if (cant1 <= cant2)
        {
            NumxBuy = cant1.Truncate();
        }
        else
        {
            NumxBuy = cant2.Truncate();
        }
    }

    public void ChangeQuanBuy()
    {
        if (BtnPerDisplay.text == "1")
        {
            f1 = true;
            NumxBuy = 10;
            BtnPerDisplay.text = "" + NotationScript.Notate(NumxBuy);
        }
        else if (f1 == true)
        {
            f1 = false;
            f2 = true;
            NumxBuy = 100;
            BtnPerDisplay.text = "" + NotationScript.Notate(NumxBuy);
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
            NumxBuy = 1;
            BtnPerDisplay.text = "" + NotationScript.Notate(NumxBuy);
        }
    }
}
