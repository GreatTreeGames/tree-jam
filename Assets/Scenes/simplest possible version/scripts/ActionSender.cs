using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSender : MonoBehaviour
{
    public void SendAction(int index)
    {
        InputAction a = possibleActions[index];
        ResourcesStatuses testZero = new ResourcesStatuses();
        testZero = target.resources - a.toSpend;
        if (!ResourcesStatuses.checkZero(testZero))
        {
            target.growth+= a.toApply;
            target.resources -= a.toSpend;
        }
        else
        {
            print("not enough sun or water");
        }
        
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

