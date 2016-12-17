using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSkill : IAction {

    ISkill skillUsed;

    public UseSkill(Unit current, ISkill skill)
        :base(current)
    {
        skillUsed = skill;
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
                int damage = skillUsed.calculateEffectiveness(curUnit, targets[i]);
                targets[i].curHp -= damage;

                //Pay the cost
                curUnit.curHp -= skillUsed.hpCost;
                curUnit.curMana -= skillUsed.manaCost;

                Debug.Log(targets[i].unitName + " takes " + damage + " - Hp Left: " + targets[i].curHp);
            }
        }
    }
}
