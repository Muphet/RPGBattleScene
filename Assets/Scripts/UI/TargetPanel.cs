using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetPanel : ControlPanel {

    int MAX_TARGETS = 4;
    List<Unit> validTargets;
    IAction action;

    //Object that controls the 3d visuals
    GameObject mapInterface;

    // Use this for initialization
    void Start () {
        //Used to communicate with 3d display
        mapInterface = GameObject.Find("3DDisplay");

        //On prend un pointeur sur tous nos options menu
        choixMenu = new Image[MAX_TARGETS];
        for (int i = 0; i < MAX_TARGETS; i++)
        {
            choixMenu[i] = gameObject.transform.GetChild(i).gameObject.GetComponent<Image>();
        }
        couleurBase = choixMenu[0].color;
        couleurSelection = couleurBase + new Color(0, 0, 0, 0.3f);
        indexSelection = 0;
        personnagePret = -1;
        choixMenu[indexSelection].color = couleurSelection;
    }

    public void Init<T>(List<T> targets, IAction newAction)
    {
        if(typeof(T).Equals(typeof(Character)) || typeof(T).Equals(typeof(Monster)) || typeof(T).Equals(typeof(Unit)))
        {
            //Initilize list
            validTargets = new List<Unit>();
            for (int i = 0; i < targets.Count; i++)
                validTargets.Add(targets[i] as Unit);

            indexSelection = 0;
            action = newAction;

            //Make invalid targets invisible and update valid target names
            for (int i = 0; i < MAX_TARGETS; i++)
            {
                if (i >= validTargets.Count)
                    transform.GetChild(i).gameObject.SetActive(false);
                else
                {
                    GameObject target = transform.GetChild(i).gameObject;
                    target.GetComponent<Image>().color = couleurBase;
                    target.transform.GetChild(0).GetComponent<Text>().text = validTargets[i].unitName;
                    target.SetActive(true);
                }
            }

            if (validTargets.Count > 0)
            {
                transform.GetChild(0).gameObject.GetComponent<Image>().color = couleurSelection;
                int index = 0;
                mapInterface.SendMessage("SelectMonster", index);
            }
        }
        else
        {
            Debug.Log("Mauvais type envoyé par liste pour les targets");
        }
    }

    public void InitChar(List<Character> targets, IAction newAction)
    {
        
    }

    public override void ProcessAction()
    {
        //Bouge le curseur
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (indexSelection < validTargets.Count - 1)
            {
                choixMenu[indexSelection].color = couleurBase;
                mapInterface.SendMessage("DeselectMonster", indexSelection);
                ++indexSelection;
                choixMenu[indexSelection].color = couleurSelection;
                mapInterface.SendMessage("SelectMonster", indexSelection);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (indexSelection > 0)
            {
                choixMenu[indexSelection].color = couleurBase;
                mapInterface.SendMessage("DeselectMonster", indexSelection);
                --indexSelection;
                choixMenu[indexSelection].color = couleurSelection;
                mapInterface.SendMessage("SelectMonster", indexSelection);
            }
        }

        //Confirm le choix
        if (Input.GetKeyDown(KeyCode.Return))
        {
            action.AddTarget(validTargets[indexSelection]);
            mapInterface.SendMessage("DeselectMonster", indexSelection);
            BattleEventHandler.AddPlayerAction(action);
        }

        //Cancel action
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Check if it was a skill or normal attack
            if(action.GetType() == typeof(UseSkill))
                BattleEventHandler.ChangeState(BattleEventHandler.ActionState.SKILL);
            else if(action.GetType() == typeof(Attack))
                BattleEventHandler.ChangeState(BattleEventHandler.ActionState.ACTION);
        }
    }
}
