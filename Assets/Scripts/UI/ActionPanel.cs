using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPanel : ControlPanel {

    // Use this for initialization
    void Start()
    {
        //On prend un pointeur sur tous nos options menu
        choixMenu = new Image[4];
        for(int i = 0; i < 4; i++)
        {
            choixMenu[i] = gameObject.transform.GetChild(i).gameObject.GetComponent<Image>();
        }
        couleurBase = choixMenu[0].color;
        couleurSelection = couleurBase + new Color(0, 0, 0, 0.3f);
        indexSelection = 0;
        choixMenu[indexSelection].color = couleurSelection;
    }

    //Initialise les informations pour le combat (noms, hp, etc)
    void InitCombatInfo()
    {

    }

    //Update lorsqu'un personnage est prêt
    public override void ProcessAction()
    {
        //Bouge le curseur
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (indexSelection < 3)
            {
                choixMenu[indexSelection].color = couleurBase;
                ++indexSelection;
                choixMenu[indexSelection].color = couleurSelection;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (indexSelection > 0)
            {
                choixMenu[indexSelection].color = couleurBase;
                --indexSelection;
                choixMenu[indexSelection].color = couleurSelection;
            }
        }

        //Confirm le choix
        if(Input.GetKeyDown(KeyCode.Return))
        {
            ConfirmAction();
        }
    }

    private void ConfirmAction()
    {
        switch(indexSelection)
        {
            //Attaque
            case 0:
                BattleEventHandler.AttackEnnemyState();
                break;
            //Skill
            case 1:
                BattleEventHandler.SelectSkillState();
                break;
            //Item
            case 2:
                break;
            //Run
            case 3:
                break;
        }
    }
}
