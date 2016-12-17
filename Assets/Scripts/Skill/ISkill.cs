using System.Collections;
using System.Collections.Generic;

public abstract class ISkill
{
    public string skillName;
    public int manaCost;
    public int hpCost;

    public int minDamage;
    public int maxDamage;

    public bool targetsAllies;

    public abstract int calculateEffectiveness(Unit user, Unit target);
	
}
