using BreakInfinity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUDEnemy : MonoBehaviour
{
	public TMP_Text levelText;
	public Slider hpSlider;
	public TMP_Text HPDisplay;
	BigDouble maxhp;
	BigDouble currenthp;

    public void SetHUD(Unit unit)
	{
		levelText.text = "" + NotationScript.Notate(unit.unitLevel);
		hpSlider.maxValue = 1;

		currenthp = unit.currentHP;
		maxhp = unit.maxHP;
		SetHP(currenthp);
	}

    public void SetHP(BigDouble current)
    {
        BigDouble fillvalueEnemy = current / maxhp;

        hpSlider.value = (float)fillvalueEnemy.ToDouble();
		if (current <= 0)
		{
            HPDisplay.text = NotationScript.Notate(0)
				+ "/" + NotationScript.Notate(maxhp);
        }
		else
		{
            HPDisplay.text = NotationScript.Notate(current)
				+ "/" + NotationScript.Notate(maxhp);
        }
    }
}
