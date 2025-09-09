using BreakInfinity;
using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Base Stats")]
    public BigDouble Attack;
    public BigDouble Defense;
    public BigDouble MaxHP;
    public BigDouble CurrentHP;
    public int CriticalRate;   
    public BigDouble CriticalDamage; 
    public BigDouble CurrentAttack;
    public BigDouble CurrentDefense;
    public int CurrentCR; 
    public BigDouble CurrentCD;

    [Header("Skill Modifiers (permanentes)")]
    public int CurrentEvasionChance = 0;
    public bool hasLifeDrain = false;
    public bool hasExecution = false;
    public bool hasRegeneration = false;
    public bool hasShieldBreaker = false;
    public bool hasGuardiansBane = false;
    public bool hasCriticalFocus = false;
    public bool hasPowerStrike = false;
    public bool hasExpertSwordman = false;
    public bool hasMastery = false;
    public BigDouble xpMultiplier = 1;

    [Header("HUD Flags (ya tenías esto)")]
    public BattleHUDPlayer hud;
    public BigDouble LootMultiplier;
    public bool flag;
    public bool flagPotion = false;
    public bool wasinstak = false;

    public float durationA;
    public float durationD;
    public float durationCR;
    public float durationCD;

    private void Start()
    {
        // Inicializar stats dinámicos
        setCurrentStats();
    }

    public void setCurrentStats()
    {
        CurrentAttack = Attack;
        CurrentDefense = Defense;
        CurrentHP = MaxHP;
        CurrentCR = CriticalRate;
        CurrentCD = CriticalDamage;
        CurrentEvasionChance = 0; // solo skills lo activan
    }

    // ------------------------
    // Pociones
    // ------------------------

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
        if (CurrentCR > 100) CurrentCR = 100;
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

    // ------------------------
    // Cálculo de daño
    // ------------------------

    public BigDouble CalculateDamage(Unit enemy)
    {
        // Base damage
        BigDouble baseDamage = (CurrentAttack * 2) - enemy.Defense;

        // ShieldBreaker (5%) y GuardiansBane (15%) - ignoran parte de la defensa
        if (hasShieldBreaker && Random.value < 0.05f)
            baseDamage += enemy.Defense * 0.5;
        if (hasGuardiansBane && Random.value < 0.15f)
            baseDamage += enemy.Defense * 0.5;

        // Execution - 5% instakill
        if (hasExecution && Random.value < 0.50f)
        {
            flag = false;
            wasinstak = true;
            return enemy.currentHP; // valor grande = instakill (puedes adaptarlo)
            
        }

        // Críticos
        bool isCritical = IsCriticalHit(CurrentCR);
        if (isCritical)
        {
            BigDouble critMultiplier = 1 + (CurrentCD / 100);
            baseDamage *= critMultiplier;
            flag = true;

            // Critical Focus - chance de doble crítico
            if (hasCriticalFocus && Random.value < 0.05f)
                baseDamage *= 2;
        }
        else
        {
            flag = false;
        }

        // PowerStrike (25%) + ExpertSwordman (50%)
        if (hasPowerStrike)
            baseDamage *= 1.25;
        if (hasExpertSwordman)
            baseDamage *= 1.50;

        // LifeDrain
        if (hasLifeDrain && baseDamage > 0)
        {
            BigDouble healAmount = baseDamage * 0.25;
            Heal(healAmount);
        }
        wasinstak = false;
        return BigDouble.Max(baseDamage, 0);
        
    }

    private bool IsCriticalHit(int criticalRate)
    {
        float roll = Random.Range(1f, 100f);
        return roll <= criticalRate;
    }

    // ------------------------
    // Soporte skills
    // ------------------------

    public void Heal(BigDouble amount)
    {
        CurrentHP += amount;
        if (CurrentHP > MaxHP) CurrentHP = MaxHP;
    }

    public bool TryEvade()
    {
        if (CurrentEvasionChance <= 0) return false;
        float roll = Random.Range(1f, 100f);
        return roll <= CurrentEvasionChance;
    }

    public bool TakeDamage(BigDouble dmg)
    {
        CurrentHP -= dmg;

        if (CurrentHP <= 0)
            return true;
        else
            return false;
    }

    private IEnumerator ResetStatAfterDuration(BigDouble amount, float duration, System.Action resetAction)
    {
        yield return new WaitForSeconds(duration);
        resetAction.Invoke();
    }
}