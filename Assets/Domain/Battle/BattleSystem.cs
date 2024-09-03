using BreakInfinity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, WAITING }

public class BattleSystem : MonoBehaviour
{
	public GameObject enemyPrefab;
	public GameObject BackgroundPrefab;
    Animator anim;

	public PlayerStats playerUnit;
	Unit enemyUnit;

	public TMP_Text dialogueText;
    string namestage;
	bool flag;


	public BattleHUDPlayer playerHUD;
	public BattleHUDEnemy enemyHUD;
	public StagesController method;
	public PotionsController potion;
	public XPManager lv;
	public MonsterResourcesController drop;

    GameObject enemyGO;
	GameObject backgroundGO;

    public BattleState state;

	GameObject getBackground(string name, GameObject prefab)
	{
        // Get the child
        GameObject specificStage = prefab.transform.Find(name).gameObject;

        if (specificStage != null)
		{
            // Instantiate the child
            return prefab = Instantiate(specificStage);
        }
		else
		{
			return null;
        }
    }

    public BigDouble setEnemylvl(string name)
    {
		BigDouble setlvl;
		if (name == "TrainingLBackground")
		{
			setlvl = lv.Level;
			return setlvl;
		}
		else if (name == "DarkFBackground")
		{
			setlvl = lv.Level + 100;
			return setlvl;
		}
		else if (name == "MysticGBackground")
		{
			setlvl = lv.Level + 1000;
			return setlvl;
		}
		else if (name == "BasaltCBackground")
		{
			setlvl = lv.Level + 5000;
			return setlvl;
		}
		else if (name == "GlacialPBackground")
		{
			setlvl = lv.Level + 10000;
			return setlvl;
		}
		else if (name == "DesertRBackground")
		{
			setlvl = lv.Level + 100000;
			return setlvl;
		}
		else
		{
			return setlvl = 0;
		}
    }

	void mods(string name)
	{
        drop.ResetModifiers();
        if (name == "TrainingLBackground")
        {
			flag = true;
        }
        else if (name == "DarkFBackground")
        {
			flag = false;
        }
        else if (name == "MysticGBackground")
        {
			flag = false;
            drop.IncreaseDropChance(drop.MonsterMeatD, 25f);
            drop.IncreaseDropChance(drop.MonsterBoneD, 25f);
            drop.IncreaseDropChance(drop.MonsterEyeD, 25f);
        }
        else if (name == "BasaltCBackground")
        {
            flag = false;
            drop.IncreaseDropChance(drop.MonsterMeatD, 50f);
            drop.IncreaseDropChance(drop.MonsterBoneD, 50f);
            drop.IncreaseDropChance(drop.MonsterEyeD, 50f);
        }
        else if (name == "GlacialPBackground")
        {
            flag = false;
            drop.IncreaseDropChance(drop.MonsterMeatD, 75f);
            drop.IncreaseDropChance(drop.MonsterBoneD, 75f);
            drop.IncreaseDropChance(drop.MonsterEyeD, 75f);
        }
        else if (name == "DesertRBackground")
        {
            flag = false;
            drop.IncreaseDropChance(drop.MonsterMeatD, 100f);
            drop.IncreaseDropChance(drop.MonsterBoneD, 100f);
            drop.IncreaseDropChance(drop.MonsterEyeD, 100f);
        }
    }

    public void StartTheStage(string name)
	{
		backgroundGO = getBackground(name, BackgroundPrefab);
		namestage = name;
		drop.cleanpanel();
		mods(name);
        playerHUD.setNewStart();
        if (playerUnit.currentHP != playerUnit.maxHP)
        {
            playerUnit.currentHP = playerUnit.maxHP;
        }
        StartNewBattle();
    }

	public void StartNewBattle()
	{
        state = BattleState.START;
        playerUnit.setCurrentStats();
        StartCoroutine(SetupBattle());
        StartCoroutine(ShowRewards());
    }

	IEnumerator SetupBattle()
	{
        enemyGO = RandomEncounter(enemyPrefab);
		enemyUnit = enemyGO.GetComponent<Unit>();
		enemyUnit.SetStats(setEnemylvl(namestage));
		anim = enemyGO.GetComponentInChildren<Animator>();

		dialogueText.text = "Battle Beggin!";

		playerHUD.SetHUD(playerUnit);
		enemyHUD.SetHUD(enemyUnit);

		yield return new WaitForSeconds(1f);

		state = BattleState.PLAYERTURN;
		PlayerTurn();
	}

	IEnumerator PlayerAttack()
	{
		bool isDead = enemyUnit.TakeDamage(playerUnit.CalculateDamage(enemyUnit.Defense));
		anim.SetBool("Damaged", true);
		if (playerUnit.flag == true)
		{
			dialogueText.text = "Critical Hit!";
		}
		else
		{
            dialogueText.text = "The attack is successful!";
        }
		state = BattleState.WAITING;

        enemyHUD.SetHP(enemyUnit.currentHP);

		yield return new WaitForSeconds(2f);

		if(isDead)
		{
			state = BattleState.WON;
            anim.SetBool("Damaged", false);
            yield return new WaitForSeconds(1f);
            EndBattle();
		} else
		{
            anim.SetBool("Damaged", false);
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
		}
	}

	IEnumerator EnemyTurn()
	{
		dialogueText.text = "Monster attacks!";
		anim.SetBool("attack", true);

        yield return new WaitForSeconds(1f);

		bool isDead = playerUnit.TakeDamage(enemyUnit.CalculateDamage(playerUnit.Defense));

		playerHUD.SetHP(playerUnit.currentHP);

		yield return new WaitForSeconds(1f);

		if(isDead)
		{
			state = BattleState.LOST;
			EndBattle();
        } else
		{
			state = BattleState.PLAYERTURN;
			PlayerTurn();
		}
	}

	void EndBattle()
	{
		if(state == BattleState.WON)
		{
			dialogueText.text = "You won the battle!";
			destroyClones(false);
			state = BattleState.WAITING;
			drop.GenerateLoot(flag);
			lv.AddXPFromEnemy(enemyUnit.unitLevel);
			StartNewBattle();
        } 
		else if (state == BattleState.LOST)
		{
			dialogueText.text = "You were defeated.";
            method.YouAreDead(true);
        }
	}

	void PlayerTurn()
	{
		dialogueText.text = "Your Turn!";
	}

	IEnumerator PlayerHeal()
	{
		playerUnit.Heal(playerUnit.maxHP);

		playerHUD.SetHP(playerUnit.currentHP);
		dialogueText.text = "Recovered 25% health";
		state = BattleState.WAITING;

		yield return new WaitForSeconds(1f);

		state = BattleState.ENEMYTURN;
		StartCoroutine(EnemyTurn());
	}

    IEnumerator ATKIncrease()
    {
		playerUnit.IncreaseAttack();

		dialogueText.text = "Attack Increase 10% for 20s";
        state = BattleState.WAITING;

        yield return new WaitForSeconds(1f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator DEFIncrease()
    {
        playerUnit.IncreaseDefense();

        dialogueText.text = "Defense Increase 20% for 20s";
        state = BattleState.WAITING;

        yield return new WaitForSeconds(1f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator CRIncrease()
    {
        playerUnit.IncreaseCR();

        dialogueText.text = "Critical Rate Increase 15% for 20s";
        state = BattleState.WAITING;

        yield return new WaitForSeconds(1f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator CDIncrease()
    {
        playerUnit.IncreaseCD();

        dialogueText.text = "Critical Damage Increase 50% for 20s";
        state = BattleState.WAITING;

        yield return new WaitForSeconds(1f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

	IEnumerator ShowRewards()
	{
		yield return new WaitForSeconds(3f);
		drop.cleanpanel();
	}

    public void OnAttackButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerAttack());
	}

	public void OnHealButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerHeal());
		potion.HealPotionQuantity -= 1;
	}

    public void OnATKPotionButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(ATKIncrease());
		potion.ATKPotionQuantity -= 1;
    }

    public void OnDEFPotionButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(DEFIncrease());
		potion.DEFPotionQuantity -= 1;
    }

    public void OnCRPotionButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(CRIncrease());
		potion.CRPotionQuantity -= 1;
    }

    public void OnCDPotionButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(CDIncrease());
		potion.CDPotionQuantity -= 1;
    }

    GameObject RandomEncounter(GameObject prefab)
	{
        int childCount = prefab.transform.childCount;

        if (childCount > 0)
        {
            // Generate a random index within the child count range
            int randomChildIndex = Random.Range(0, childCount);

            // Get the random child at the generated index
            GameObject randomChild = prefab.transform.GetChild(randomChildIndex).gameObject;

            // Instantiate the random child
            return prefab = Instantiate(randomChild);
        }
		else
		{
			return null;
		}
    }

	public void destroyClones(bool flagstage)
	{
        Destroy(enemyGO);
		if(flagstage == true)
		{
            Destroy(backgroundGO);
        }
    }

	public void OnContinue()
	{
        playerUnit.currentHP = playerUnit.maxHP;
        playerHUD.setNewStart();
        enemyUnit.currentHP = enemyUnit.maxHP;
		enemyHUD.SetHP(enemyUnit.currentHP);
		method.YouAreDead(false);
        playerHUD.SetHUD(playerUnit);
        state = BattleState.START;
        dialogueText.text = "Battle Beggin!";

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }


}
