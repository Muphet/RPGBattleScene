using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : IAction
{
    public Attack(Unit current)
        :base(current)
    {

    }

    //Execute l'attaque sur toutes les cibles
    public override void ExecuteAction()
    {
        //Check if attacker still alive
        if (curUnit.curHp > 0)
        {          
            //If current target is dead, acquire new target
            for (int j = targets.Count - 1; j >= 0; --j)
            {
                bool targetValid = targets[j].curHp > 0;

                if (!targetValid)
                {
                    //Remove invalid target and try to replace with valid target
                    targets.Remove(targets[j]);

                    //Character trying to attack monster
                    if (curUnit.GetType().Equals(typeof(Character)))
                    {
                        for (int i = 0; i < BattleEventHandler.Monsters.Count && !targetValid; ++i)
                        {
                            if (BattleEventHandler.Monsters[i].curHp > 0 && !targets.Contains(BattleEventHandler.Monsters[i]))
                            {
                                targets.Add(BattleEventHandler.Monsters[i]);
                                targetValid = true;
                            }
                        }
                    }

                    //Monster trying to attack character
                    else
                    {
                        for (int i = 0; i < BattleEventHandler.Player.Count && !targetValid; ++i)
                        {
                            if (BattleEventHandler.Player[i].curHp > 0 && !targets.Contains(BattleEventHandler.Player[i]))
                            {
                                targets.Add(BattleEventHandler.Player[i]);
                                targetValid = true;
                            }
                        }
                    }
                }
            }

            //Execute the action if a valid target exists
            for (int i = 0; i < targets.Count; ++i)
            {
                int damage = (int)(Random.Range(2, 6) * ((float)curUnit.baseAtk / targets[i].baseDef));
                targets[i].curHp -= damage;
                Debug.Log(targets[i].unitName + " takes " + damage + " - Hp Left: " + targets[i].curHp);
            }      
        }
    }
}
