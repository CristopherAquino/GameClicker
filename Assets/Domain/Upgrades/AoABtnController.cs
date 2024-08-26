using BreakInfinity;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AoABtnController : MonoBehaviour
{
    //Numeric Variable
    public BigDouble AoALvl;
    public BigDouble CostStone;
    public BigDouble CostMonsterEssence;

    //UI Variables
    public TMP_Text CostStoneDisplay;
    public TMP_Text CostMonsterEssenceDisplay;
    public TMP_Text AoAbundanceLvlDisplay;
    public TMP_Text LootMultiplierDisplay;
    public TMP_Text BuyAoABtnDisplay;

    //Variable on other class
    public MonsterResourcesController monster;
    public StoneBuyBtnController Stone;
    public BtnMAXController Buy;
    public PlayerStats Player;

    // Start is called before the first frame update
    void Start()
    {
        CostStone = 10;
        CostMonsterEssence = 10;
        CostMonsterEssenceDisplay.text = "" + NotationScript.Notate(CostMonsterEssence);
        CostStoneDisplay.text = "" + NotationScript.Notate(CostStone);
    }

    // Update is called once per frame
    void Update()
    {
        BigDouble temp;
        CostMonsterEssenceDisplay.text = "" + NotationScript.Notate(CostMonsterEssence);
        CostStoneDisplay.text = "" + NotationScript.Notate(CostStone);
        AoAbundanceLvlDisplay.text = "" + NotationScript.Notate(AoALvl);
        LootMultiplierDisplay.text = NotationScript.Notate(Player.LootMultiplier) + "";

        if (Buy.f3 == true)
        {
            Buy.MAXxMul(monster.MonsterEssence, Stone.Stone, CostMonsterEssence, CostStone);
            temp = Buy.NumxBuy;
            BuyAoABtnDisplay.text = "BUY" + Environment.NewLine + "x" + NotationScript.Notate(temp);
        }
        else
        {
            temp = Buy.NumxBuy;
            BuyAoABtnDisplay.text = "BUY" + Environment.NewLine + "x" + NotationScript.Notate(temp);
        }
    }

    public void UpgradeAoA()
    {
        if (Buy.f3 == true)
        {
            Buy.MAXxMul(monster.MonsterEssence, Stone.Stone, CostMonsterEssence, CostStone);
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
            Stone.Stone >= (CostStone * Buy.NumxBuy))
        {
            AoALvl += (1 * Buy.NumxBuy);
            monster.MonsterEssence -= (CostMonsterEssence * Buy.NumxBuy);
            Stone.Stone -= (CostStone * Buy.NumxBuy);

            Player.LootMultiplier = (AoALvl+1);
        }
    }
}
