using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSender : MonoBehaviour
{
    public void SendAction(int index)
    {
        InputAction a = possibleActions[index];
        target.growth+= a.toApply;
        target.resources += a.toSpend;
    }

    public List<InputAction> possibleActions;
    public Treestatus target;
}

[System.Serializable]
public class InputAction
{
    public string name;
    public GrowthStatuses toApply;
    public ResourcesStatuses toSpend;
}

