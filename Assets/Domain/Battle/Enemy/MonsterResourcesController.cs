using BreakInfinity;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

[System.Serializable]
public class LootItem
{
    public string itemName;
    public float baseDropChance;
    public float dropChanceModifier;

    public LootItem(string name, float baseChance)
    {
        itemName = name;
        baseDropChance = baseChance;
        dropChanceModifier = 0f;
    }

    // Calculate the effective drop chance
    public float EffectiveDropChance()
    {
        return baseDropChance + dropChanceModifier;
    }
}

public class MonsterResourcesController : MonoBehaviour
{
    public LootItem MonsterMeatD;
    public LootItem MonsterBoneD;
    public LootItem MonsterEyeD;

    public BigDouble MonsterEssence;//100%
    public BigDouble MonsterMeat;   //50%
    public BigDouble MonsterBone;   //30%
    public BigDouble MonsterEye;    //10%
    public bool flag = false;
    public string RewardDisplay;

    public TMP_Text MonsterEssenceDisplay;
    public TMP_Text MonsterMeatDisplay;
    public TMP_Text MonsterBoneDisplay;
    public TMP_Text MonsterEyeDisplay;
    public TMP_Text RewardPanelDisplay;

    public PlayerStats player;

    void Start()
    {
        // Initialize items with base drop chances
        MonsterMeatD = new LootItem("MonsterMeat", 50f);
        MonsterBoneD = new LootItem("MonsterBone", 30f);
        MonsterEyeD = new LootItem("MonsterEye", 10f);
    }

    // Update is called once per frame
    void Update()
    {
        MonsterEssenceDisplay.text = ""+ NotationScript.Notate(MonsterEssence);
        MonsterMeatDisplay.text = "" + NotationScript.Notate(MonsterMeat);
        MonsterBoneDisplay.text = "" + NotationScript.Notate(MonsterBone);
        MonsterEyeDisplay.text = "" + NotationScript.Notate(MonsterEye);
        RewardPanelDisplay.text = RewardDisplay.Trim();
    }
    public void GenerateLoot(bool mid)
    {
        BigDouble multiplier, reward;
        if (mid == true)
        {
            multiplier = (player.LootMultiplier / 2)+0.1;
        }
        else
        {
            multiplier = player.LootMultiplier;
        }
        reward = 5 * multiplier.Round();
        MonsterEssence += reward;
        RewardDisplay += "Monster Drop Some Items:\n";
        RewardDisplay += "You Got " + NotationScript.Notate(reward) + " Monster Essence\n";
        CheckDrop(MonsterMeatD, multiplier.Round());
        CheckDrop(MonsterBoneD, multiplier.Round());
        CheckDrop(MonsterEyeD, multiplier.Round());
    }

    private void CheckDrop(LootItem item, BigDouble multi)
    {
        float randomNumber = Random.Range(0f, 100f);
        if (randomNumber <= item.EffectiveDropChance())
        {
            if(item == MonsterMeatD)
            {
                int drop = Random.Range(1,4);
                BigDouble reward = drop * multi;
                MonsterMeat += reward;
                RewardDisplay += "You Got " + NotationScript.Notate(reward) + " Monster Meat\n";
            }
            if (item == MonsterBoneD)
            {
                int drop = Random.Range(1, 3);
                BigDouble reward = drop * multi;
                MonsterBone += reward;
                RewardDisplay += "You Got " + NotationScript.Notate(reward) + " Monster Bone\n";
            }
            if (item == MonsterEyeD)
            {
                int drop = Random.Range(1, 2);
                BigDouble reward = drop * multi;
                MonsterEye += reward;
                RewardDisplay += "You Got " + NotationScript.Notate(reward) + " Monster Eye\n";
            }
        }
    }

    public void IncreaseDropChance(LootItem item, float increaseBy)
    {
        item.dropChanceModifier += increaseBy;
    }

    public void ResetModifiers()
    {
        MonsterMeatD.dropChanceModifier = 0f;
        MonsterBoneD.dropChanceModifier = 0f;
        MonsterEyeD.dropChanceModifier = 0f;
    }

    public void cleanpanel()
    {
        RewardDisplay = "";
    }

}
