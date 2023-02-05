using Scenes.simplest_possible_version.scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    private void Update()
    {
        // AdjustScale();
    }

    private void AdjustScale()
    {
        if (transform.lossyScale.x >= 1 || transform.lossyScale.y >= 1) return;
        
        var inverseLossyScale = new Vector3(1, 1, transform.lossyScale.z).Divide(transform.lossyScale);
        transform.localScale = inverseLossyScale;
    }
}
