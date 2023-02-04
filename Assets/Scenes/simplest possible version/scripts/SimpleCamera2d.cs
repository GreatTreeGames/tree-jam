using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCamera2d : MonoBehaviour
{
    public GameObject target;
    float currentvelocity = 1f;

    public float focusrate = 1f;
    public float compositionsize = 1f;

    // Update is called once per frame
    void Update()
    {
        Camera.main.orthographicSize =Mathf.SmoothDamp(Camera.main.orthographicSize, GetMaxBounds(target).extents.y * compositionsize, ref currentvelocity, focusrate);
    }

    Bounds GetMaxBounds(GameObject g)
    {
        var b = new Bounds(g.transform.position, Vector3.zero);
        foreach (Renderer r in g.GetComponentsInChildren<Renderer>())
        {
            b.Encapsulate(r.bounds);
        }
        return b;
    }
}
