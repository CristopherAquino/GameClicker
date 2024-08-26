using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills
{
    public enum SkillType
    {
        VitalityBoost, BetterShield, LifeDrain, CriticalFocus,
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
        }
    }

    public bool IsSkillUnlocked(SkillType skillType)
    {
        return unlockedSkillTypeList.Contains(skillType);   
    }

}
