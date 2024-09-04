using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
    public string skillName;
    public bool isUnlocked = false;
    public int requiredLevel;
    PlayerStats effect;

    public Skill(string name, int level)
    {
        skillName = name;
        requiredLevel = level;
    }

    public void Unlock()
    {
        if (!isUnlocked)
        {
            isUnlocked = true;
            Debug.Log(skillName + " unlocked!");
            ApplyEffect();
        }
    }

    public void ApplyEffect()
    {
        switch (skillName)
        {
            case "VitalityBoost":
                // Effect: Increase 50% the player's MaxHP permanently in battle
                // permanently increases the player's MaxHP by 50% during battles.
                effect.SetVitalityBoost();
                break;
            case "BetterShield":
                // Example effect: Increase player shield strength
                
                break;
            case "LifeDrain":
                // Example effect: Enable life drain on attacks
                
                break;
            case "CriticalFocus":
                // Example effect: Increase critical hit chance
               
                break;
            case "Evasion":
                // Example effect: Increase dodge chance
                break;
            // Add more cases for other skills
            case "Mastery":
                // Example effect: Overall boost to all stats
                break;
        }
    }
}
