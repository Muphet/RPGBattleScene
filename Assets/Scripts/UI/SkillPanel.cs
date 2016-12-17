using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : ControlPanel {

    int MAX_SKILLS = 8;
    //List<Unit> curUnit.KnownSkills;
    Unit curUnit;
    IAction action;

    // Use this for initialization
    void Start()
    {
        //On prend un pointeur sur tous nos options menu
        choixMenu = new Image[MAX_SKILLS];
        for (int i = 0; i < MAX_SKILLS; i++)
        {
            choixMenu[i] = gameObject.transform.GetChild(i+1).gameObject.GetComponent<Image>();
        }
        couleurBase = choixMenu[0].color;
        couleurSelection = couleurBase + new Color(0, 0, 0, 0.3f);
        indexSelection = 0;
        choixMenu[indexSelection].color = couleurSelection;
    }

    public void UpdateList(Unit current)
    {
        curUnit = current;

        for(int i = 0; i < MAX_SKILLS; i++)
        {
            //If we are over our limit of skill, hide the skill
            if(i >= curUnit.KnownSkills.Count)
            {
                transform.GetChild(i + 1).gameObject.SetActive(false);
            }
            //Update and show the skill info
            else
            {
                transform.GetChild(i + 1).GetChild(0).GetComponent<Text>().text = curUnit.KnownSkills[i].skillName;
                transform.GetChild(i + 1).GetChild(2).GetComponent<Text>().text = curUnit.KnownSkills[i].manaCost + " MP";
                transform.GetChild(i + 1).gameObject.SetActive(true);
            }
        }
    }

    public override void ProcessAction()
    {
        //Bouge le curseur
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (indexSelection < curUnit.KnownSkills.Count - 2)
            {
                choixMenu[indexSelection].color = couleurBase;
                ++indexSelection;
                ++indexSelection;
                choixMenu[indexSelection].color = couleurSelection;
            }
            else if (indexSelection < curUnit.KnownSkills.Count - 1)
            {
                choixMenu[indexSelection].color = couleurBase;
                ++indexSelection;
                choixMenu[indexSelection].color = couleurSelection;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (indexSelection >= 2)
            {
                choixMenu[indexSelection].color = couleurBase;
                --indexSelection;
                --indexSelection;
                choixMenu[indexSelection].color = couleurSelection;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (indexSelection < curUnit.KnownSkills.Count - 2)
            {
                choixMenu[indexSelection].color = couleurBase;
                ++indexSelection;
                choixMenu[indexSelection].color = couleurSelection;
            }
            else if (indexSelection == curUnit.KnownSkills.Count - 2)
            {
                choixMenu[indexSelection].color = couleurBase;
                --indexSelection;
                choixMenu[indexSelection].color = couleurSelection;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (indexSelection > 0)
            {
                choixMenu[indexSelection].color = couleurBase;
                --indexSelection;
                choixMenu[indexSelection].color = couleurSelection;
            }
        }

        //Confirm le choix
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(curUnit.curHp > curUnit.KnownSkills[indexSelection].hpCost && curUnit.curMana >= curUnit.KnownSkills[indexSelection].manaCost)
                BattleEventHandler.UseSkillState(curUnit.KnownSkills[indexSelection]);
        }

        //Cancel action
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BattleEventHandler.ChangeState(BattleEventHandler.ActionState.ACTION);
        }
    }
}
