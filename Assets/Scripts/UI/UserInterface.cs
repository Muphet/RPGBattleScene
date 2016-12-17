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

    public void Init(List<Character> characters)
    {
        //Initialisation des composantes
        AP = transform.FindChild("ActionPanel").GetComponent<ActionPanel>();
        TP = transform.FindChild("TargetPanel").GetComponent<TargetPanel>();
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

        //SP = new SkillPanel[8];
        //SPContrainer = transform.FindChild("SkillPanel").gameObject;
        //
        //for (int i = 0; i < 8; i++)
        //{
        //    SP[i] = SPContrainer.transform.GetChild(i + 1).GetComponent<SkillPanel>();
        //}

    }

    public void HighlightActiveCharacter(int index)
    {
        for(int i = 0; i < CP.Length; ++i)
        {
            if(CP[i] != null)
            {
                CP[i].SetHighlight(i == index);
            }
        }
    }

}
