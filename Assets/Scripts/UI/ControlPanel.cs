using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ControlPanel : MonoBehaviour {
    protected int indexSelection;
    protected int personnagePret;
     
    protected Image[] choixMenu;
    protected Color couleurBase;
    protected Color couleurSelection;

    public abstract void ProcessAction();

}
