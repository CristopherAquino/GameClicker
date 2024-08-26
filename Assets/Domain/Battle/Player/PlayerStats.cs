using BreakInfinity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public BigDouble maxHP;
    public BigDouble currentHP;

    public BigDouble Attack;
    public BigDouble CurrentAttack;

    public BigDouble Defense;
    public BigDouble CurrentDefense;

    public int CriticalRate;
    public int CurrentCR;

    public BigDouble CriticalDamage;
    public BigDouble CurrentCD;

    public BigDouble LootMultiplier;
    public BigDouble SkillPoints;
    public bool flag = false;
    public bool flagPotion = false;

    public BattleHUDPlayer hud;
    public float durationA;
    public float durationD;
    public float durationCR;
    public float durationCD;

    private PlayerSkills playerSkills;

    public void setCurrentStats()
    {
        CurrentAttack = Attack;
        CurrentDefense = Defense;
        CurrentCR = CriticalRate;
        CurrentCD = CriticalDamage;
    }

    public bool TakeDamage(BigDouble dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public void Heal(BigDouble amount)
    {
        BigDouble tamount = amount * 0.25;
        currentHP += tamount.Truncate();
        if (currentHP > maxHP)
            currentHP = maxHP;
    }

    public void IncreaseAttack()
    {
        BigDouble amount = Attack * 0.10;
        durationA = 20f;
        hud.flagAtk = true;
        CurrentAttack += amount;
        StartCoroutine(ResetStatAfterDuration(amount, durationA, () => CurrentAttack -= amount));
    }

    public void IncreaseDefense()
    {
        BigDouble amount = Defense * 0.20;
        durationD = 20f;
        hud.flagDef = true;
        CurrentDefense += amount;
        StartCoroutine(ResetStatAfterDuration(amount, durationD, () => CurrentDefense -= amount));
    }

    public void IncreaseCR()
    {
        BigDouble amount = 15;
        durationCR = 20f;
        hud.flagCR = true;
        CurrentCR += (int)amount.ToDouble();
        if(CurrentCR > 100)
        {
            CurrentCR = 100;
        }
        StartCoroutine(ResetStatAfterDuration(amount, durationCR, () => CurrentCR -= (int)amount.ToDouble()));
    }

    public void IncreaseCD()
    {
        BigDouble amount = 50;
        durationCD = 20f;
        hud.flagCD = true;
        CurrentCD += amount;
        StartCoroutine(ResetStatAfterDuration(amount, durationCD, () => CurrentCD -= amount));
    }

    public BigDouble CalculateDamage(BigDouble target)
    {
        // Base damage calculation
        BigDouble baseDamage = (CurrentAttack * 2) - target;

        // Adjust for critical hits
        bool isCritical = IsCriticalHit(CurrentCR);
        if (isCritical == true)
        {
            BigDouble CriticalDamageMultiplier = 1 + (CurrentCD / 100);
            baseDamage *= CriticalDamageMultiplier;
            flag = true;
        }
        else
        {
            flag = false;
        }
        // Apply additional modifiers (e.g., abilities, equipment bonuses)
        //baseDamage += CalculateAdditionalModifiers(attacker);

        // Ensure damage is positive and return the result
        return BigDouble.Max(baseDamage, 0);
    }

    private bool IsCriticalHit(int criticalRate)
    {
        float roll = Random.Range(1.0f,100.0f);
        roll = Mathf.RoundToInt(roll);

        if(roll < criticalRate)
        {
            return true;
        }
        else
        {
            return false;
        }    
    }

    public BigDouble tempoStat(string namePotion)
    {
        BigDouble stat;
        if (namePotion == "ATK")
        {
            BigDouble tamount = Attack * 0.10;
            stat = Attack + tamount;
            return stat.Truncate();
        }
        else if (namePotion == "DEF")
        {
            BigDouble tamount = Defense * 0.20;
            stat = Defense + tamount;
            return stat.Truncate();
        }
        else if (namePotion == "CR")
        {
            stat = CriticalRate + 15;
            return stat;
        }
        else if (namePotion == "CD")
        {
            stat = CriticalDamage + 50;
            return stat.Truncate();
        }
        else
        {
            return stat = 0;
        }
    }

    private IEnumerator ResetStatAfterDuration(BigDouble amount, float duration, System.Action resetAction)
    {
        yield return new WaitForSeconds(duration);
        resetAction.Invoke(); // Reset the stat after the duration
    }

    /*private int CalculateAdditionalModifiers(Character character)
    {
        int additionalDamage = 0;

        // Example: Add damage bonuses from equipped items or active abilities
        foreach (Item item in character.EquippedItems)
        {
            additionalDamage += item.DamageBonus;
        }

        // Example: Apply damage modifiers based on character's status effects or buffs
        foreach (StatusEffect effect in character.StatusEffects)
        {
            additionalDamage += effect.DamageModifier;
        }

        return additionalDamage;
    }*/

    public PlayerSkills GetPlayerSkills()
    {
        return playerSkills;
    }

    public bool CanUseVitalityBoost(){ return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.VitalityBoost); }
    public bool CanUseBetterShield() { return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.BetterShield); }
    public bool CanUseLifeDrain() { return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.LifeDrain); }
    public bool CanUseCriticalFocus() { return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.CriticalFocus); }
    public bool CanUseEvasion() { return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Evasion); }
    public bool CanUseShieldBreaker() { return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.ShieldBreaker); }
    public bool CanUseRegeneration() { return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Regeneration); }
    public bool CanUsePowerStrike() { return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.PowerStrike); }
    public bool CanUseExecution() { return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Execution); }
    public bool CanUseQuickStep() { return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.QuickStep); }
    public bool CanUseFortify() { return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Fortify); }
    public bool CanUseGuardiansBane() { return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.GuardiansBane); }
    public bool CanUseExpertSwordman() { return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.ExpertSwordman); }
    public bool CanUseMastery() { return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Mastery); }
}
