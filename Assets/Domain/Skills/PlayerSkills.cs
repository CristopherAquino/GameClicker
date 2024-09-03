using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkills
{
    public event EventHandler<OnSkillUnlockedEventArgs> OnSkillUnlocked;
    public class OnSkillUnlockedEventArgs : EventArgs
    {
        public SkillType skillType;
    }
    
    public enum SkillType
    {
        None, VitalityBoost, BetterShield, LifeDrain, CriticalFocus,
        Evasion, ShieldBreaker, Regeneration, PowerStrike,
        Execution, QuickStep, Fortify, GuardiansBane,
        ExpertSwordman, Mastery
    }

    private List<SkillType> unlockedSkillTypeList;

    public PlayerSkills()
    {
        unlockedSkillTypeList = new List<SkillType>();
    }

    public void UnlockSkill(SkillType skillType) 
    {
        if (!IsSkillUnlocked(skillType)) {
            unlockedSkillTypeList.Add(skillType);
            OnSkillUnlocked?.Invoke(this, new OnSkillUnlockedEventArgs { skillType = skillType});
        }
    }

    public bool IsSkillUnlocked(SkillType skillType)
    {
        return unlockedSkillTypeList.Contains(skillType);   
    }

    public SkillType GetSkillRequeriment(SkillType skillType)
    {
        switch(skillType)
        {
            case SkillType.VitalityBoost: return SkillType.BetterShield; 
            case SkillType.BetterShield: return SkillType.LifeDrain;
            case SkillType.LifeDrain: return SkillType.CriticalFocus;
            case SkillType.CriticalFocus: return SkillType.Evasion;
            case SkillType.Evasion: return SkillType.ShieldBreaker;
            case SkillType.ShieldBreaker: return SkillType.Regeneration;
            case SkillType.Regeneration: return SkillType.PowerStrike;
            case SkillType.PowerStrike: return SkillType.Execution;
            case SkillType.Execution: return SkillType.QuickStep;
            case SkillType.QuickStep: return SkillType.Fortify;
            case SkillType.Fortify: return SkillType.GuardiansBane;
            case SkillType.GuardiansBane: return SkillType.ExpertSwordman;
            case SkillType.ExpertSwordman: return SkillType.Mastery;
        }
        return SkillType.None;
    }

    public bool TryUnlockSkill(SkillType skillType)
    {
        SkillType skillRequeriment = GetSkillRequeriment(skillType);
        if(skillRequeriment != SkillType.None)
        {
            if (IsSkillUnlocked(skillRequeriment))
            {
                UnlockSkill(skillType);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            UnlockSkill(skillType);
            return true;
        }
    }
}
