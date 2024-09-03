using BreakInfinity;
using TMPro;
using UnityEngine;

public class XPManager : MonoBehaviour
{
    public BigDouble Level = 1;
    public BigDouble currentLevel = 1;      // Current level of the player
    public BigDouble currentXP = 0;         // Current XP of the player
    public BigDouble xpToNextLevel = 100;   // XP needed to reach the next level

    public TMP_Text LvlUPDisplay;
    public TMP_Text HPUPDisplay;
    public TMP_Text ATKUPDisplay;
    public TMP_Text DEFUPDisplay;
    public TMP_Text CDUPDisplay;
    public TMP_Text HPIncreaseUPDisplay;
    public TMP_Text ATKIncreaseUPDisplay;
    public TMP_Text DEFIncreaseUPDisplay;
    public TMP_Text CDIncreaseUPDisplay;

    public PlayerStats player;
    public ScriptMenuTabController tab;

    // Call this method to add XP
    public void AddXP(BigDouble xpToAdd)
    {
        currentXP += xpToAdd;
        CheckLevelUp();
    }

    // Check if the player has reached the XP needed for the next level
    void CheckLevelUp()
    {
        while (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;     // Subtract the XP threshold from current XP
            currentLevel++;                 // Increase the player's level
            Level = currentLevel;
            xpToNextLevel = CalculateXPForNextLevel(currentLevel);  // Calculate new XP threshold
            
            LevelUpEffects();
        }
    }

    // Calculate the XP needed for the next level
    BigDouble CalculateXPForNextLevel(BigDouble level)
    {
        return 100 + (level * level);
    }

    public void AddXPFromEnemy(BigDouble enemyLevel)
    {
        BigDouble xpToAdd = CalculateXPFromLevelDifference(enemyLevel);
        AddXP(xpToAdd);
    }

    // Calculate XP based on level difference
    private BigDouble CalculateXPFromLevelDifference(BigDouble enemyLevel)
    {
        BigDouble levelDifference = enemyLevel - currentLevel;
        BigDouble baseXP = 50;  // Base XP for defeating an enemy of the same level

        // Adjust XP gain based on the level difference
        if (levelDifference >= 0)  // Enemy level is the same or higher
        {
            return (baseXP * (1 + 0.25f * levelDifference));
        }
        else  // Enemy level is lower
        {
            return BigDouble.Max(10, baseXP + 10 * levelDifference);  // Ensure some minimum XP gain
        }
    }

    // Handle level-up effects, animations, etc.
    void LevelUpEffects()
    {
        int hpUP = 100;
        int atkUP = Random.Range(3, 5);
        int defUP = Random.Range(4, 5);
        int cdUP = Random.Range(1, 3);


        if (Level > 100 && Level <= 500)
        {
            hpUP = (hpUP * 10);
            atkUP = (atkUP * 10);
            defUP = (defUP * 10);
            cdUP = (cdUP * 10);
        }
        
        if(Level > 500)
        {
            hpUP = (hpUP * 100);
            atkUP = (atkUP * 100);
            defUP = (defUP * 100);
            cdUP = (cdUP * 20);
        }

        oldstat();

        player.maxHP += hpUP;
        player.Attack += atkUP;
        player.Defense += defUP;
        player.CriticalDamage += cdUP;

        LvlUPDisplay.text = "Level UP: " + Level;
        HPIncreaseUPDisplay.text = "" + player.maxHP;
        ATKIncreaseUPDisplay.text = "" + player.Attack;
        DEFIncreaseUPDisplay.text = "" + player.Defense;
        CDIncreaseUPDisplay.text = "" + player.CriticalDamage;

        player.setCurrentStats();
        tab.increaseLVL(true);
    }

    void oldstat()
    {
        HPUPDisplay.text = "HP: " + player.maxHP;
        ATKUPDisplay.text = "ATK: " + player.Attack;
        DEFUPDisplay.text = "DEF: " + player.Defense;
        CDUPDisplay.text = "CD: " + player.CriticalDamage;
    }

    public void OnOKBtn()
    {
        tab.increaseLVL(false);
    }
}

