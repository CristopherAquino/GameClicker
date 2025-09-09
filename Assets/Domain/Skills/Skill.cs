using BreakInfinity;
using UnityEngine;

public enum SkillState { Locked, Available, Unlocked }

public enum SkillType
{
    None,
    VitalityBoost,
    BetterShield,
    LifeDrain,
    CriticalFocus,
    Evasion,
    ShieldBreaker,
    Regeneration,
    PowerStrike,
    Execution,
    QuickStep,
    Fortify,
    GuardiansBane,
    ExpertSwordman,
    Mastery
}

[System.Serializable]
public class Skill
{
    [Header("General Info")]
    public string skillName;
    [TextArea] public string description;
    public Sprite icon;

    [Header("Progress")]
    public SkillState state;
    public SkillType skillType;
    public BigDouble requiredLevel;

    [Header("Dependencies")]
    public SkillType prerequisite = SkillType.None; // skill previa necesaria

    // referencia a stats del jugador
    private PlayerStats effect;

    public Skill(string name, SkillType type, BigDouble level, string desc = "", Sprite skillIcon = null, SkillType prereq = SkillType.None)
    {
        skillName = name;
        skillType = type;
        requiredLevel = level;
        description = desc;
        icon = skillIcon;
        prerequisite = prereq;
        state = SkillState.Locked;
    }

    public void Unlock(PlayerStats stats)
    {
        if (state != SkillState.Unlocked)
        {
            state = SkillState.Unlocked;
            effect = stats;
            ApplyEffect(stats);
        }
    }

    public void ApplyEffect(PlayerStats stats)
    {
        switch (skillType)
        {
            case SkillType.VitalityBoost:
                stats.MaxHP *= 1.5;
                stats.CurrentHP = stats.MaxHP;
                break;

            case SkillType.BetterShield:
                stats.CurrentDefense += stats.Defense * 0.10;
                break;

            case SkillType.LifeDrain:
                stats.hasLifeDrain = true;
                break;

            case SkillType.CriticalFocus:
                stats.hasCriticalFocus = true;
                stats.CurrentCR = Mathf.Min(100, stats.CurrentCR + 5);
                break;

            case SkillType.Evasion:
                stats.CurrentEvasionChance += 10;
                break;

            case SkillType.ShieldBreaker:
                stats.hasShieldBreaker = true;
                break;

            case SkillType.Regeneration:
                stats.hasRegeneration = true;
                break;

            case SkillType.PowerStrike:
                stats.hasPowerStrike = true;
                break;

            case SkillType.Execution:
                stats.hasExecution = true;
                break;

            case SkillType.QuickStep:
                stats.CurrentEvasionChance += 20;
                break;

            case SkillType.Fortify:
                stats.CurrentDefense += stats.Defense * 0.20;
                break;

            case SkillType.GuardiansBane:
                stats.hasGuardiansBane = true;
                break;

            case SkillType.ExpertSwordman:
                stats.hasExpertSwordman = true;
                break;

            case SkillType.Mastery:
                stats.hasMastery = true;
                stats.xpMultiplier = 3;
                break;
        }
    }
}