using System.Collections;
using System.Collections.Generic;

public abstract class Unit {

    public string unitName;
    public int maxHp;
    public int curHp;
    public int maxMana;
    public int curMana;
    public int baseAtk;
    public int baseDef;
    public int baseSpeed;

    public List<ISkill> KnownSkills;

    public Unit()
    {
        KnownSkills = new List<ISkill>();
        Skill test = new Skill();
        test.skillName = "Test Skill1";
        test.minDamage = 3;
        test.maxDamage = 5;
        test.hpCost = 0;
        test.manaCost = 4;
        test.targetsAllies = false;
        Skill test2 = new Skill();
        test2.skillName = "Test Skill2";
        test2.minDamage = 3;
        test2.maxDamage = 5;
        test2.hpCost = 0;
        test2.manaCost = 4;
        test2.targetsAllies = true;

        KnownSkills.Add(test);
        KnownSkills.Add(test2);
        KnownSkills.Add(test);
        KnownSkills.Add(test);
        KnownSkills.Add(test);
        KnownSkills.Add(test);
        KnownSkills.Add(test);

    }
}
