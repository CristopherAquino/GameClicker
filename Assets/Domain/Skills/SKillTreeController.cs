using BreakInfinity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SKillTreeController : MonoBehaviour
{
    // Reference to the button's Image component
    [SerializeField]
    private Image Ico1, Ico2, Ico3, Ico4, Ico5, Ico6, Ico7, Ico8,
        Ico9, Ico10, Ico11, Ico12, Ico13, Ico14;
    private Image buttonImage;

    [SerializeField]
    private Image Line1, Line2, Line3, Line4, Line5, Line6, Line7, Line8,
    Line9, Line10, Line11, Line12, Line13;

    bool active = false;

    [SerializeField]
    private Button Getbtn;
    string SkillSelected;

    public TMP_Text DescriptionSkillDisplay;
    public TMP_Text TitleSkillDisplay;
    public TMP_Text GetBtnDisplay;

    // Reference to the target UI Image to display the clicked button's sprite
    public Image targetImage;

    private void Start()
    {
        DescriptionSkillDisplay.text = "";
        TitleSkillDisplay.text = "";
        targetImage.gameObject.SetActive(false);
        Getbtn.gameObject.SetActive(false);
    }

    // Method to handle the button click event
    public void OnButtonClick(string name)
    {
        changerico(name);
        // Check if the Image component exists and the target UI Image is assigned
        if (name != null)
        {
            Getbtn.gameObject.SetActive(true);
            targetImage.gameObject.SetActive(true);
            // Get the sprite currently displayed on the button
            Sprite buttonSprite = buttonImage.sprite;

            // Set the sprite of the target UI Image to the clicked button's sprite
            targetImage.sprite = buttonSprite;

            // Optionally, you can adjust other properties of the target UI Image as needed
            // For example, you can set the fill mode, color, etc.
            // targetImage.fillMethod = ...;
            // targetImage.color = ...;
        }
        else
        {
            DescriptionSkillDisplay.text = "";
            TitleSkillDisplay.text = "";
            targetImage.sprite = null;
            targetImage.gameObject.SetActive(false);
            Getbtn.gameObject.SetActive(false);
        }
    }

    void changerico(string name)
    {
        SkillSelected = name;
        switch (name)
        {
            case "skill1":
                buttonImage = Ico1.GetComponent<Image>();
                TitleSkillDisplay.text = "Vitality Boost";
                DescriptionSkillDisplay.text = "Permanently increases the player's MaxHP by 50% during battles.";
                

            break;
            case "skill2":
                buttonImage = Ico2.GetComponent<Image>();
                TitleSkillDisplay.text = "Better Shield";
                DescriptionSkillDisplay.text = "Boosts the player's defense by 10% whenever attacked by an enemy.";

                break;
            case "skill3":
                buttonImage = Ico3.GetComponent<Image>();
                TitleSkillDisplay.text = "Life Drain";
                DescriptionSkillDisplay.text = "After attacking, the player heals 25% of the total damage dealt.";

                break;
            case "skill4":
                buttonImage = Ico4.GetComponent<Image>();
                TitleSkillDisplay.text = "Critical Focus";
                DescriptionSkillDisplay.text = "Grants the player a 5% chance to deal double critical damage.";

                break;
            case "skill5":
                buttonImage = Ico5.GetComponent<Image>();
                TitleSkillDisplay.text = "Evasion";
                DescriptionSkillDisplay.text = "Grants the player a 10% chance to evade enemy attacks.";

                break;
            case "skill6":
                buttonImage = Ico6.GetComponent<Image>();
                TitleSkillDisplay.text = "Shield Breaker";
                DescriptionSkillDisplay.text = "Provides a 5% chance to ignore 50% of the enemy's defense.";

                break;
            case "skill7":
                buttonImage = Ico7.GetComponent<Image>();
                TitleSkillDisplay.text = "Regeneration";
                DescriptionSkillDisplay.text = "Restores 2% of the player's max HP every turn.";

                break;
            case "skill8":
                buttonImage = Ico8.GetComponent<Image>();
                TitleSkillDisplay.text = "Power Strike";
                DescriptionSkillDisplay.text = "Increases the player's total damage by 25%.";

                break;
            case "skill9":
                buttonImage = Ico9.GetComponent<Image>();
                TitleSkillDisplay.text = "Execution";
                DescriptionSkillDisplay.text = "Grants a 1% chance to execute an instakill attack.";

                break;
            case "skill10":
                buttonImage = Ico10.GetComponent<Image>();
                TitleSkillDisplay.text = "QuickStep";
                DescriptionSkillDisplay.text = "Grants the player a 20% chance to evade enemy attacks.";

                break;
            case "skill11":
                buttonImage = Ico11.GetComponent<Image>();
                TitleSkillDisplay.text = "Fortify";
                DescriptionSkillDisplay.text = "Increases the player's defense by 20% when attacked by the enemy.";

                break;
            case "skill12":
                buttonImage = Ico12.GetComponent<Image>();
                TitleSkillDisplay.text = "Guardian's Bane";
                DescriptionSkillDisplay.text = "Provides a 15% chance to ignore 50% of the enemy's defense.";

                break;
            case "skill13":
                buttonImage = Ico13.GetComponent<Image>();
                TitleSkillDisplay.text = "Expert Swordsman";
                DescriptionSkillDisplay.text = "Increases the player's total damage by 50%.";

                break;
            case "skill14":
                buttonImage = Ico14.GetComponent<Image>();
                TitleSkillDisplay.text = "Mastery Skill";
                DescriptionSkillDisplay.text = "Triples the player's experience gain in battles.";

                break;
        }
    }

    public void OnGETbutton()
    {
        switch (SkillSelected)
        {
            case "skill1":
                Line1.GetComponent<Image>();
                Line1.color = Color.blue;
                active = true;

                break;
            case "skill2":
                Line2.GetComponent<Image>();
                Line2.color = Color.blue;

                break;
            case "skill3":
                Line3.GetComponent<Image>();
                Line3.color = Color.blue;

                break;
            case "skill4":
                Line4.GetComponent<Image>();
                Line4.color = Color.blue;

                break;
            case "skill5":
                Line5.GetComponent<Image>();
                Line5.color = Color.blue;

                break;
            case "skill6":
                Line6.GetComponent<Image>();
                Line6.color = Color.blue;

                break;
            case "skill7":
                Line7.GetComponent<Image>();
                Line7.color = Color.blue;

                break;
            case "skill8":
                Line8.GetComponent<Image>();
                Line8.color = Color.blue;

                break;
            case "skill9":
                Line9.GetComponent<Image>();
                Line9.color = Color.blue;

                break;
            case "skill10":
                Line10.GetComponent<Image>();
                Line10.color = Color.blue;

                break;
            case "skill11":
                Line11.GetComponent<Image>();
                Line11.color = Color.blue;

                break;
            case "skill12":
                Line12.GetComponent<Image>();
                Line12.color = Color.blue;

                break;
            case "skill13":
                Line13.GetComponent<Image>();
                Line13.color = Color.blue;

                break;
            case "skill14":
                buttonImage = Ico14.GetComponent<Image>();
                TitleSkillDisplay.text = "Mastery Skill";
                DescriptionSkillDisplay.text = "Triples the player's experience gain in battles.";

                break;
        }
    }


}