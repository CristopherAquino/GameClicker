using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISkilltree : MonoBehaviour
{
    //References
    public SkillTreeManager skillTreeManager;
    public List<Button> skillButtons = new List<Button>();
    public Button getButton;
    public TMP_Text getButtonText;           // Texto del botón GET
    public SkillLineManager lineManager;

    //Info panel
    public TMP_Text skillNameText;
    public TMP_Text skillDescriptionText;
    public Image skillIconImage;
    public GameObject infoPanel;

    private int selectedIndex = -1;

    void Start()
    {
        if (infoPanel) infoPanel.SetActive(false); // oculto al inicio
        if (getButton) getButton.onClick.AddListener(UnlockSelectedSkill);

        // Vincular botones de skill (ignora las de tipo None)
        int btnIndex = 0;
        for (int i = 0; i < skillTreeManager.skills.Count; i++)
        {
            var s = skillTreeManager.skills[i];
            if (s.skillType == SkillType.None) continue;

            if (btnIndex >= skillButtons.Count) break;

            var btn = skillButtons[btnIndex];
            int idx = i;
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => SelectSkill(idx));
            btnIndex++;
        }

        RefreshUI();
    }

    public void SelectSkill(int index)
    {
        if (!skillTreeManager || index < 0 || index >= skillTreeManager.skills.Count) return;

        selectedIndex = index;
        var s = skillTreeManager.skills[index];

        if (infoPanel) infoPanel.SetActive(true); // mostrar panel
        if (skillNameText) skillNameText.text = s.skillName;
        if (skillDescriptionText)
            skillDescriptionText.text = $"{s.description}\nRequired lvl: {s.requiredLevel}";
        if (skillIconImage) skillIconImage.sprite = s.icon;

        UpdateGetButton(s);
    }

    public void UnlockSelectedSkill()
    {
        if (!skillTreeManager || selectedIndex < 0) return;

        var s = skillTreeManager.skills[selectedIndex];
        if (s.state == SkillState.Available)
        {
            skillTreeManager.UnlockSkill(s);

            // Colorear línea asociada
            if (lineManager) lineManager.ColorLine(selectedIndex);

            RefreshUI();
            UpdateGetButton(s);
        }
    }

    // Llamado cuando cierres o cambies de menú
    public void HideInfoPanel()
    {
        selectedIndex = -1;
        if (infoPanel) infoPanel.SetActive(false);
    }


    private void RefreshUI()
    {
        if (!skillTreeManager || skillButtons == null) return;

        int btnIndex = 0;
        for (int i = 0; i < skillTreeManager.skills.Count; i++)
        {
            var s = skillTreeManager.skills[i];
            if (s.skillType == SkillType.None) continue;
            if (btnIndex >= skillButtons.Count) break;

            var btn = skillButtons[btnIndex];
            if (!btn) continue;

            var txt = btn.GetComponentInChildren<TMP_Text>();
            if (!txt) continue;

            switch (s.state)
            {
                case SkillState.Locked:
                    btn.interactable = false;
                    txt.text = $"{s.skillName} (Locked)";
                    break;
                case SkillState.Available:
                    btn.interactable = true;
                    txt.text = $"{s.skillName} (Available)";
                    break;
                case SkillState.Unlocked:
                    btn.interactable = false;
                    txt.text = $"{s.skillName} (Unlocked)";
                    break;
            }

            btnIndex++;
        }

        // Actualizar botón GET según la skill seleccionada
        if (selectedIndex >= 0 && selectedIndex < skillTreeManager.skills.Count)
        {
            UpdateGetButton(skillTreeManager.skills[selectedIndex]);
        }
    }

    private void UpdateGetButton(Skill s)
    {
        if (!getButton) return;

        switch (s.state)
        {
            case SkillState.Available:
                getButton.interactable = true;
                if (getButtonText) getButtonText.text = "Get";
                break;

            case SkillState.Unlocked:
                getButton.interactable = false;
                if (getButtonText) getButtonText.text = "Completed";
                break;

            default: // Locked u otros
                getButton.interactable = false;
                if (getButtonText) getButtonText.text = "Locked";
                break;
        }
    }
}