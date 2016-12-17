using System.Collections;
using System.Collections.Generic;

public abstract class IAction
{
    protected Unit curUnit;
    protected List<Unit> targets;
    public int priority;

    public IAction(Unit current)
    {
        targets = new List<Unit>();
        curUnit = current;
        priority = curUnit.baseSpeed;
    }

    public void AddTarget(Unit target)
    {
        if (!targets.Contains(target))
            targets.Add(target);
    }

    public abstract void ExecuteAction();
}
