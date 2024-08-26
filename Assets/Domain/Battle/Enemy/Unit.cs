using BreakInfinity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	public BigDouble unitLevel;

	public BigDouble maxHP;
	public BigDouble currentHP;

    public BigDouble Attack;
    public BigDouble Defense;
    public int CriticalRate;
    public BigDouble CriticalDamage;
    public bool flagenemy = false;

    public bool TakeDamage(BigDouble dmg)
	{
		currentHP -= dmg;

		if (currentHP <= 0)
			return true;
		else
			return false;
	}

	public void SetStats(BigDouble lvl)
	{
		unitLevel = lvl;
		maxHP = (1000 * lvl);
		currentHP = maxHP;
		Attack = 50*lvl;
        Defense = 25 * lvl;

        if(lvl > 1000 && lvl < 5000)
        {
            CriticalRate = 40;
            CriticalDamage = 200;
        }
        else if(lvl > 5000 && lvl < 10000)
        {
            CriticalRate = 60;
            CriticalDamage = 300;
        }
        else if (lvl > 10000)
        {
            CriticalRate = 75;
            CriticalDamage = 400;
        }
        else
        {
            CriticalRate = 5;
            CriticalDamage = 50;
        }
    }

    public BigDouble CalculateDamage(BigDouble target)
    {
        // Base damage calculation
        BigDouble baseDamage = (Attack * 2) - target;

        // Adjust for critical hits
        bool isCritical = IsCriticalHit(CriticalRate);
        if (isCritical == true)
        {
            BigDouble CriticalDamageMultiplier = 1 + (CriticalDamage / 100);
            baseDamage *= CriticalDamageMultiplier;
            flagenemy = true;
        }

        // Apply additional modifiers (e.g., abilities, equipment bonuses)
        //baseDamage += CalculateAdditionalModifiers(attacker);

        // Ensure damage is positive and return the result
        return BigDouble.Max(baseDamage, 0);
    }

    private bool IsCriticalHit(int criticalRate)
    {
        int roll = Random.Range(0, 100);

        if (roll < criticalRate)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
