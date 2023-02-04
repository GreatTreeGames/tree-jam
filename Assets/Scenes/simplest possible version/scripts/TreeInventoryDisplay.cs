using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TreeInventoryDisplay : MonoBehaviour
{
    public TextMeshProUGUI sunamount;
    public TextMeshProUGUI wateramount;

    void Start()
    {
        updateGraphics(new ResourcesStatuses());
    }
    
    public void updateGraphics(ResourcesStatuses currentvalue)
    {
        sunamount.text = currentvalue.sun.ToString();
        wateramount.text = currentvalue.water.ToString();
    }
}
