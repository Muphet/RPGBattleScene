using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class BattleEventHandler : MonoBehaviour
{
    static UserInterface UI;
    MapHandler manHandler;
    static ControlPanel activePanel;

    static ActionState battleState = ActionState.INIT;
    public static ActionState BattleState { get { return battleState; } }

    static List<Character> playerChars;
    static Character curCharacter;
    static public List<Character> Player { get { return playerChars; } }

    static List<Monster> monsters;
    static public List<Monster> Monsters { get { return monsters; } }

    static List<IAction> actions;
    static public List<IAction> Actions { get { return actions; } }

    public void Start()
    {
        UI = GameObject.Find("Interface").GetComponent<UserInterface>();
        manHandler = GameObject.Find("3DDisplay").GetComponent<MapHandler>();
        Init();
        UpdateInfo();
        manHandler.Init(playerChars, monsters);
        ChangeState(ActionState.ACTION);   
    }

    public void Update()
    {
        if(activePanel != null)
            activePanel.ProcessAction();
    }

    public void Init()
    {
        actions = new List<IAction>();

        //Set players
        playerChars = new List<Character>();
        playerChars.Add(new Character());
        playerChars.Add(new Character());

        playerChars[0].unitName = "David Wolff";
        playerChars[0].maxHp = 20;
        playerChars[0].curHp = 20;
        playerChars[0].maxMana = 10;
        playerChars[0].curMana = 10;
        playerChars[0].baseAtk = 4;
        playerChars[0].baseDef = 4;
        playerChars[0].baseSpeed = 3;

        playerChars[1].unitName = "Mandrake Kollyd";
        playerChars[1].maxHp = 15;
        playerChars[1].curHp = 8;
        playerChars[1].maxMana = 20;
        playerChars[1].curMana = 16;
        playerChars[1].baseAtk = 3;
        playerChars[1].baseDef = 5;
        playerChars[1].baseSpeed = 4;

        //Set monsters
        monsters = new List<Monster>();
        monsters.Add(new Monster());
        monsters.Add(new Monster());

        monsters[0].unitName = "Wolf";
        monsters[0].maxHp = 10;
        monsters[0].curHp = 10;
        monsters[0].maxMana = 2;
        monsters[0].curMana = 2;
        monsters[0].baseAtk = 3;
        monsters[0].baseDef = 2;
        monsters[0].baseSpeed = 4;

        monsters[1].unitName = "Wolf Cub";
        monsters[1].maxHp = 6;
        monsters[1].curHp = 6;
        monsters[1].maxMana = 0;
        monsters[1].curMana = 0;
        monsters[1].baseAtk = 2;
        monsters[1].baseDef = 1;
        monsters[1].baseSpeed = 3;

        curCharacter = playerChars[0];

    }

    static public void UpdateInfo()
    {
        UI.Init(playerChars);
    }

    static public void AddPlayerAction(IAction newAction)
    {
        actions.Add(newAction);
        //If all actions were chosen, go to monsters action selection. Else, proceed to next player
        if (actions.Count == playerChars.Count)
            ChangeState(ActionState.MONSTER);
        else
        {
            curCharacter = playerChars[actions.Count];
            ChangeState(ActionState.ACTION);
        }
    }

    //Visual changes to UI
    static public void ChangeState(ActionState newState)
    {
        switch (newState)
        {
            case ActionState.ACTION:
                //Highlight the active player
                UI.HighlightActiveCharacter(actions.Count);
                UI.SkillP.gameObject.SetActive(false);
                UI.TargetP.gameObject.SetActive(false);
                UI.AllCP.SetActive(true);
                activePanel = UI.ActionP;
                break;
            case ActionState.TARGET:
                UI.TargetP.gameObject.SetActive(true);
                UI.SkillP.gameObject.SetActive(false);
                UI.AllCP.SetActive(false);
                activePanel = UI.TargetP;
                break;
            case ActionState.SKILL:
                UI.SkillP.gameObject.SetActive(true);
                UI.TargetP.gameObject.SetActive(false);
                UI.AllCP.SetActive(false);
                activePanel = UI.SkillP;
                break;
            case ActionState.ITEM:
                break;
            case ActionState.MONSTER:
                UI.TargetP.gameObject.SetActive(false);
                UI.AllCP.SetActive(true);
                activePanel = null;
                GenerateMonsterActions();
                ResolveTurn();
                break;
            case ActionState.INIT:
                break;
            default:
                break;
        }
    }

    //Transition to attacking ennemy menu
    static public void AttackEnnemyState()
    {
        Attack newAttack = new Attack(curCharacter);
        UI.TargetP.Init(monsters, newAttack);
        ChangeState(ActionState.TARGET);
    }

    //Transition to selecting a skill to use menu
    static public void SelectSkillState()
    {
        UI.SkillP.UpdateList(curCharacter);
        ChangeState(ActionState.SKILL);
    }

    //Transition to target selection for skill
    static public void UseSkillState(ISkill skill)
    {
        UseSkill useSkill = new UseSkill(curCharacter, skill);
        if (skill.targetsAllies)
            UI.TargetP.Init(playerChars, useSkill);
        else
            UI.TargetP.Init(monsters, useSkill);

        ChangeState(ActionState.TARGET);
    }

    static void GenerateMonsterActions()
    {
        for(int i = 0; i < monsters.Count; ++i)
        {
            Attack newAttack = new Attack(monsters[i]);
            newAttack.AddTarget(playerChars[Random.Range(0, playerChars.Count)]);
            actions.Add(newAttack);
        }
    }

    static void ResolveTurn()
    {
        //Order all the actions by priorities
        List<IAction> actionsOrdonnes = actions.OrderByDescending(a => a.priority).ToList();

        //On résout les actions une à une
        for (int i = 0; i < actionsOrdonnes.Count; ++i)
        {
            actionsOrdonnes[i].ExecuteAction();
            //On met à jour les HP etc.
            UpdateInfo();
        }

        //On vide la liste d'actions
        actions.Clear();

        //Remove dead monsters
        for (int i = monsters.Count - 1; i >= 0; --i)
            if (monsters[i].curHp <= 0)
                monsters.RemoveAt(i);

        if (monsters.Count == 0)
            Debug.Log("You win the fight!");

        ChangeState(ActionState.ACTION);
    }

    public enum ActionState
    {
        ACTION,
        TARGET,
        SKILL,
        ITEM,
        MONSTER,
        INIT
    }
}
