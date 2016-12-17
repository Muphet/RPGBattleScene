using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : ISkill
{
    //Calculations of a very basic skill
    public override int calculateEffectiveness(Unit user, Unit target)
    {
        int damage = (int)(Random.Range(minDamage, maxDamage) * ((float)user.baseAtk / target.baseDef));
        return damage;        
    }
}
