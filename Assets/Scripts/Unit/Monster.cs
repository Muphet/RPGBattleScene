using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    public void LaunchBasicAttack()
    {
        Attack baseAttack = new Attack(this);
        //Choose a random target for your attack
        int nPlayers = BattleEventHandler.Player.Count;
        baseAttack.AddTarget(BattleEventHandler.Player[Random.Range(0, nPlayers)]);
        baseAttack.ExecuteAction();
    }

}
