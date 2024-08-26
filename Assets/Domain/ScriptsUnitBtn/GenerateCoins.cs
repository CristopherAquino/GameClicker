using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BreakInfinity;

public class GenerateCoins : MonoBehaviour
{
    //Numerical Variable
    public BigDouble Coins;

    //UI Variable
    public TMP_Text CoinsDisplay;

    

    // Update is called once per frame
    void Update()
    {
        CoinsDisplay.text = "" + NotationScript.Notate(Coins);
    }

    //Add more coins (Click)
    public void MoreCoins()
    {
        Coins += 1;
        CoinsDisplay.text = "" + NotationScript.Notate(Coins);
    }
}
