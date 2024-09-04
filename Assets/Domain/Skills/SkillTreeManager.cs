using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{
    public XPManager Level;
    public List<Skill> skills = new List<Skill>();

    void Start()
    {
        // Define each skill and its required level
        skills.Add(new Skill("None", 1));
        skills.Add(new Skill("VitalityBoost", 5));
        skills.Add(new Skill("BetterShield", 15));
        skills.Add(new Skill("LifeDrain", 25));
        skills.Add(new Skill("CriticalFocus", 30));
        skills.Add(new Skill("Evasion", 45));
        skills.Add(new Skill("ShieldBreaker", 60));
        skills.Add(new Skill("Regeneration", 65));
        skills.Add(new Skill("PowerStrike", 80));
        skills.Add(new Skill("Execution", 100));
        skills.Add(new Skill("QuickStep", 120));
        skills.Add(new Skill("Fortify", 140));
        skills.Add(new Skill("GuardiansBane", 165));
        skills.Add(new Skill("ExpertSwordman", 185));
        skills.Add(new Skill("Mastery", 200));
    }

    void Update()
    {
        CheckSkills();
    }

    void CheckSkills()
    {
        foreach (Skill skill in skills)
        {
            if (Level.currentLevel >= skill.requiredLevel && !skill.isUnlocked)
            {
                skill.Unlock();
            }
        }
    }
}
