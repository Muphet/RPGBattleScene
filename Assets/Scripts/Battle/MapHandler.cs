using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    List<Character> characters;
    List<Monster> monsters;

    GameObject[] playerModels;
    GameObject[] monsterModels;

    public void Init(List<Character> charList, List<Monster> monsterList)
    {
        playerModels = new GameObject[4];
        monsterModels = new GameObject[4];
        for(int i = 0; i < 4; ++i)
        {
            playerModels[i] = transform.GetChild(0).GetChild(i).gameObject;
            monsterModels[i] = transform.GetChild(1).GetChild(i).gameObject;
        }
        characters = charList;
        monsters = monsterList;

        //Show units that are in play
        for(int i = 0; i < characters.Count; i++)
        {
            playerModels[i].SetActive(true);
        }

        for (int i = 0; i < monsters.Count; i++)
        {
            monsterModels[i].SetActive(true);
        }
    }

    //Show or hide indicators with the following functions
    public void DeselectPlayer(int index)
    {
        playerModels[index].transform.GetChild(0).gameObject.SetActive(false);
    }

    public void SelectPlayer(int index)
    {
        playerModels[index].transform.GetChild(0).gameObject.SetActive(true);
    }

    public void DeselectMonster(int index)
    {
        monsterModels[index].transform.GetChild(0).gameObject.SetActive(false);
    }

    public void SelectMonster(int index)
    {
        monsterModels[index].transform.GetChild(0).gameObject.SetActive(true);
    }
}
