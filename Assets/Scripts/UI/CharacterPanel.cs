using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    static Color couleurBase = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    static Color couleurSelection = new Color(0.5f, 1.0f, 1.0f, 0.5f);
    GameObject hp;
    GameObject mana;

    public void Init(Character characters)
    {
        gameObject.SetActive(false);
        hp = transform.GetChild(1).gameObject;
        mana = transform.GetChild(2).gameObject;
        UpdateCharInfo(characters);
    }

    void UpdateCharInfo(Character character)
    {
        if(character != null)
        {
            //Update Name
            transform.GetChild(0).GetComponent<Text>().text = character.unitName;
            UpdateHP(character);
            UpdateMana(character);
            gameObject.SetActive(true);
        }
    }

    public void UpdateHP(Character character)
    {
        //Adjust the text
        hp.transform.GetChild(1).GetComponent<Text>().text = character.curHp + " / " + character.maxHp;

        //Adjust the bar lenght and color
        float ratio = (float)character.curHp / character.maxHp;
        if (ratio > 0.98)
            ratio = 1.0f;
        hp.transform.GetChild(0).GetComponent<RectTransform>().anchorMax = (new Vector2(ratio, 1.0f));
        if(ratio > 0.5)
            hp.transform.GetChild(0).GetComponent<Image>().color = new Color((1-ratio)*2, 1.0f, 0f, 100f / 255);
        else
            hp.transform.GetChild(0).GetComponent<Image>().color = new Color(1.0f, (1 - ratio) * 2, 0f, 100f / 255);

    }

    public void UpdateMana(Character character)
    {
        //Adjust the text
        mana.transform.GetChild(1).GetComponent<Text>().text = character.curMana + " / " + character.maxMana;

        //Adjust the bar lenght and color
        float ratio = (float)character.curMana / character.maxMana;
        if (ratio > 0.98)
            ratio = 1.0f;
        mana.transform.GetChild(0).GetComponent<RectTransform>().anchorMax = (new Vector2(ratio, 1.0f));
    }

    public void SetHighlight(bool highlight)
    {
        if (highlight)
            gameObject.GetComponent<Image>().color = couleurSelection;
        else
            gameObject.GetComponent<Image>().color = couleurBase;
    }
}
