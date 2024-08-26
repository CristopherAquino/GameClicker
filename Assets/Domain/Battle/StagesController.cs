using BreakInfinity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StagesController : MonoBehaviour
{
    BigDouble LvlRange;

    public TMP_Text TrainingLvlDisplay;
    public TMP_Text DarkFLvlDisplay;
    public TMP_Text MysticGLvlDisplay;
    public TMP_Text BasaltCLvlDisplay;
    public TMP_Text GlacialPLvlDisplay;
    public TMP_Text DesertRLvlDisplay;

    public PlayerStats player;
    public XPManager lv;
    public BattleSystem battle;
    public ScriptMenuTabController view;

    void Update()
    {
        TrainingLvlDisplay.text = "" + NotationScript.Notate(lv.Level);
        DarkFLvlDisplay.text = "" + NotationScript.Notate(lv.Level+100);
        MysticGLvlDisplay.text = "" + NotationScript.Notate(lv.Level+1000);
        BasaltCLvlDisplay.text = "" + NotationScript.Notate(lv.Level+5000);
        GlacialPLvlDisplay.text = "" + NotationScript.Notate(lv.Level+10000);
        DesertRLvlDisplay.text = "" + NotationScript.Notate(lv.Level+100000);
    }

    public void ChooseStage(string name)
    {
        string Stage;

        switch (name)
        {
            case "Training":
                Stage = "TrainingLBackground";
                LvlRange = lv.Level;

                battle.StartTheStage(Stage);
                break;
            case "Dark":
                Stage = "DarkFBackground";
                LvlRange = (lv.Level + 100);

                battle.StartTheStage(Stage);
                break;
            case "Mystic":
                Stage = "MysticGBackground";
                LvlRange = (lv.Level + 1000);

                battle.StartTheStage(Stage);
                break;
            case "Basalt":
                Stage = "BasaltCBackground";
                LvlRange = (lv.Level + 5000);

                battle.StartTheStage(Stage);
                break;
            case "Glacial":
                Stage = "GlacialPBackground";
                LvlRange = (lv.Level + 10000);

                battle.StartTheStage(Stage);
                break;
            case "Desert":
                Stage = "DesertRBackground";
                LvlRange = (lv.Level + 100000);

                battle.StartTheStage(Stage);
                break;
        }
        ChangerCanvasGroup(true, view.StageScreen);
        ChangerCanvasGroup(false, view.BattlesScreen);
        ChangerCanvasGroup(false, view.ResourcesPanelScreen);

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

    public void exitBtn()
    {
        ChangerCanvasGroup(false, view.StageScreen);
        ChangerCanvasGroup(true, view.BattlesScreen);
        ChangerCanvasGroup(true, view.ResourcesPanelScreen);
        ChangerCanvasGroup(false, view.DeadScreen);
        battle.destroyClones(true);
    }

    public void YouAreDead(bool action)
    {
        ChangerCanvasGroup(action, view.DeadScreen);
    }
}
