using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    ActionPanel AP;
    public ActionPanel ActionP { get { return AP; } }

    TargetPanel TP;
    public TargetPanel TargetP { get { return TP; } }

    GameObject CPContrainer;
    public GameObject AllCP { get { return CPContrainer; } }

    CharacterPanel[] CP;
    public CharacterPanel[] CharP { get { return CP; } }

    GameObject SPContrainer;
    SkillPanel SP;
    public SkillPanel SkillP { get { return SP; } }

    //Object that controls the 3d visuals
    GameObject mapInterface;

    public void Init(List<Character> characters)
    {
        //Initialisation des composantes
        AP = transform.FindChild("ActionPanel").GetComponent<ActionPanel>();
        TP = transform.FindChild("TargetPanel").GetComponent<TargetPanel>();
        mapInterface = GameObject.Find("3DDisplay");
        CPContrainer = transform.FindChild("CharPanel").gameObject;
        CP = new CharacterPanel[4];

        //Initialise chaque character panel
        for(int i = 0; i < 4; i++)
        {
            CP[i] = transform.GetChild(0).GetChild(i + 1).GetComponent<CharacterPanel>();

            if (i < characters.Count)
                CP[i].Init(characters[i]);
            else
                CP[i].Init(null);
        }

        SP = transform.FindChild("SkillPanel").gameObject.GetComponent<SkillPanel>();
    }

    public void HighlightActiveCharacter(int index)
    {
        for(int i = 0; i < CP.Length; ++i)
        {
            if(CP[i] != null)
            {
                if(i == index)
                {
                    CP[i].SetHighlight(true);
                    mapInterface.SendMessage("SelectPlayer", i);
                }
                else
                {
                    CP[i].SetHighlight(false);
                    mapInterface.SendMessage("DeselectPlayer", i);
                }
            }
        }
    }
}
