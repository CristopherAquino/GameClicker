using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptMenuTabController : MonoBehaviour
{
    public CanvasGroup MainScreen;
    public CanvasGroup ResourcesPanelScreen;
    public CanvasGroup ResourcesScreen;
    public CanvasGroup UpgradeScreen;
    public CanvasGroup PotionsScreen;
    public CanvasGroup BattlesScreen;
    public CanvasGroup StageScreen;
    public CanvasGroup DeadScreen;
    public CanvasGroup SkillsScreen;
    public CanvasGroup LVLUP;

    public bool flagVisible = false;

    // Start is called before the first frame update
    void Start()
    {
        ChangerCanvasGroup(true, ResourcesScreen);
        ChangerCanvasGroup(true, ResourcesPanelScreen);
        ChangerCanvasGroup(false, UpgradeScreen);
        ChangerCanvasGroup(false, PotionsScreen);
        ChangerCanvasGroup(false, BattlesScreen);
        ChangerCanvasGroup(false, StageScreen);
        ChangerCanvasGroup(false, DeadScreen);
        ChangerCanvasGroup(false, LVLUP);
        ChangerCanvasGroup(false, SkillsScreen);
    }

    void SetActiveRecursively(GameObject obj, bool active)
    {
        // Deactivate the GameObject
        obj.SetActive(active);

        // Deactivate all components attached to the GameObject
        foreach (Component component in obj.GetComponents<Component>())
        {
            // Skip Transform component to avoid deactivating the object's transform
            if (!(component is Transform))
            {
                component.gameObject.SetActive(active);
            }
        }

        // Deactivate all child GameObjects and their components
        foreach (Transform child in obj.transform)
        {
            SetActiveRecursively(child.gameObject, active);
        }
    }

    public void ChangerCanvasGroup(bool state, CanvasGroup name)
    {
        if (state)
        {
            name.alpha = 1;
            name.interactable = true;
            name.blocksRaycasts = true;
            return;
        }
        name.alpha = 0;
        name.interactable = false;
        name.blocksRaycasts = false;
    }

    public void ChangeTAB(string name)
    {
        switch (name)
        {
            case "Resources":
                ChangerCanvasGroup(true, ResourcesScreen);
                ChangerCanvasGroup(true, ResourcesPanelScreen);
                ChangerCanvasGroup(false, UpgradeScreen);
                ChangerCanvasGroup(false, PotionsScreen);
                ChangerCanvasGroup(false, BattlesScreen);
                ChangerCanvasGroup(false, StageScreen);
                ChangerCanvasGroup(false, SkillsScreen);
                break;
            case "Upgrades":
                ChangerCanvasGroup(false, ResourcesScreen);
                ChangerCanvasGroup(true, ResourcesPanelScreen);
                ChangerCanvasGroup(true, UpgradeScreen);
                ChangerCanvasGroup(false, PotionsScreen);
                ChangerCanvasGroup(false, BattlesScreen);
                ChangerCanvasGroup(false, StageScreen);
                ChangerCanvasGroup(false, SkillsScreen);
                break;
            case "Potions":
                ChangerCanvasGroup(false, ResourcesScreen);
                ChangerCanvasGroup(true, ResourcesPanelScreen);
                ChangerCanvasGroup(false, UpgradeScreen);
                ChangerCanvasGroup(true, PotionsScreen);
                ChangerCanvasGroup(false, BattlesScreen);
                ChangerCanvasGroup(false, StageScreen);
                ChangerCanvasGroup(false, SkillsScreen);
                break;
            case "Battles":
                ChangerCanvasGroup(false, ResourcesScreen);
                ChangerCanvasGroup(true, ResourcesPanelScreen);
                ChangerCanvasGroup(false, UpgradeScreen);
                ChangerCanvasGroup(false, PotionsScreen);
                ChangerCanvasGroup(true, BattlesScreen);
                ChangerCanvasGroup(false, StageScreen);
                ChangerCanvasGroup(false, SkillsScreen);
                flagVisible = true;
                break;
            case "Skills":
                ChangerCanvasGroup(false, ResourcesScreen);
                ChangerCanvasGroup(true, ResourcesPanelScreen);
                ChangerCanvasGroup(false, UpgradeScreen);
                ChangerCanvasGroup(false, PotionsScreen);
                ChangerCanvasGroup(false, BattlesScreen);
                ChangerCanvasGroup(false, StageScreen);
                ChangerCanvasGroup(true, SkillsScreen);
                break;
        }
    }

    public void increaseLVL(bool act)
    {
        ChangerCanvasGroup(act, LVLUP);
    }
}
