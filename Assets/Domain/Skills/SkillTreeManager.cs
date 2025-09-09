using BreakInfinity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{
    public XPManager xpManager;
    public List<Skill> skills = new List<Skill>();
    public PlayerStats stats;

    void Start()
    {
        foreach (var s in skills)
        {
            if (s.skillType != SkillType.None)
                s.state = SkillState.Locked;
        }

        CheckSkills();
    }

    void Update()
    {
        CheckSkills();
    }

    void CheckSkills()
    {
        if (!xpManager) return;

        BigDouble level = xpManager.Level;

        foreach (var s in skills)
        {
            if (s.skillType == SkillType.None) continue;

            // si ya está desbloqueada, ignorar
            if (s.state == SkillState.Unlocked) continue;

            // revisar nivel
            if (level < s.requiredLevel) continue;

            // revisar prerequisito
            if (s.prerequisite != SkillType.None)
            {
                Skill prereq = skills.Find(x => x.skillType == s.prerequisite);
                if (prereq == null || prereq.state != SkillState.Unlocked)
                    continue; // requisito no cumplido
            }

            // si cumple todo - marcar como disponible
            if (s.state == SkillState.Locked)
                s.state = SkillState.Available;
        }
    }

    public void UnlockSkill(Skill skill)
    {
        if (skill.state == SkillState.Available)
        {
            skill.Unlock(stats);
        }
    }
}
