using BreakInfinity;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AoFBtnController : MonoBehaviour
{
    //Numeric Variable
    public BigDouble AoFortitudeLvl;
    public BigDouble CostIron;
    public BigDouble CostMonsterEssence;

    //UI Variables
    public TMP_Text CostIronDisplay;
    public TMP_Text CostMonsterEssenceDisplay;
    public TMP_Text AoFortitudeLvlDisplay;
    public TMP_Text DEFDisplay;
    public TMP_Text BuyAoFBtnDisplay;

    //Variable on other class
    public MonsterResourcesController monster;
    public IronBuyBtnController iron;
    public BtnMAXController Buy;
    public PlayerStats Player;

    // Start is called before the first frame update
    void Start()
    {
        CostMonsterEssence = 10;
        CostIron = 10;
        CostMonsterEssenceDisplay.text = "" + NotationScript.Notate(CostMonsterEssence);
        CostIronDisplay.text = "" + NotationScript.Notate(CostIron);
    }

    // Update is called once per frame
    void Update()
    {
        BigDouble temp;
        CostMonsterEssenceDisplay.text = "" + NotationScript.Notate(CostMonsterEssence);
        CostIronDisplay.text = "" + NotationScript.Notate(CostIron);
        AoFortitudeLvlDisplay.text = "" + NotationScript.Notate(AoFortitudeLvl);
        DEFDisplay.text = NotationScript.Notate(Player.Defense) + "";

        if (Buy.f3 == true)
        {
            Buy.MAXxMul(monster.MonsterEssence, iron.Iron, CostMonsterEssence, CostIron);
            temp = Buy.NumxBuy;
            BuyAoFBtnDisplay.text = "BUY" + Environment.NewLine + "x" + NotationScript.Notate(temp);
        }
        else
        {
            temp = Buy.NumxBuy;
            BuyAoFBtnDisplay.text = "BUY" + Environment.NewLine + "x" + NotationScript.Notate(temp);
        }
    }

    public void UpgradeAoF()
    {
        if (Buy.f3 == true)
        {
            Buy.MAXxMul(monster.MonsterEssence, iron.Iron, CostMonsterEssence, CostIron);
            BuyComparator();
        }
        else
        {
            BuyComparator();
        }
    }

    void BuyComparator()
    {
        if (monster.MonsterEssence >= (CostMonsterEssence * Buy.NumxBuy) &&
            iron.Iron >= (CostIron * Buy.NumxBuy))
        {
            AoFortitudeLvl += (1 * Buy.NumxBuy);
            monster.MonsterEssence -= (CostMonsterEssence * Buy.NumxBuy);
            iron.Iron -= (CostIron * Buy.NumxBuy);

            Player.Defense = (Player.Defense + (AoFortitudeLvl * 5));
        }
    }
}
