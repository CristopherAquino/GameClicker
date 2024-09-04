using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkilltree : MonoBehaviour
{
    public SkillTreeManager skillTreeManager;
    public List<Button> skillButtons;

    void Update()
    {
        UpdateSkillButtons();
    }

    void UpdateSkillButtons()
    {
        for (int i = 0; i < skillTreeManager.skills.Count; i++)
        {
            if (skillTreeManager.skills[i].isUnlocked)
            {
                skillButtons[i].interactable = true;
                skillButtons[i].GetComponentInChildren<Text>().text = skillTreeManager.skills[i].skillName + " (Unlocked)";
            }
            else
            {
                skillButtons[i].interactable = false;
                skillButtons[i].GetComponentInChildren<Text>().text = skillTreeManager.skills[i].skillName;
            }
        }
    }
}
