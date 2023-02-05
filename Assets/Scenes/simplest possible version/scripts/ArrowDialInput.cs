using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class ArrowDialInput : MonoBehaviour
{

    public RectTransform arrow;
    public holdangledata selected;
    public List<holdangledata> angledatas;
    
    // Start is called before the first frame update
    void Start()
    {
        angledatas = GetComponentsInChildren<holdangledata>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 input = new Vector3 (h,v,0);
        if (input != Vector3.zero)
        {
            float inputAngledegrees;
            if (h < 0)
            {
                inputAngledegrees = Vector3.Angle(Vector3.up, input);
            }
            else
            {
                inputAngledegrees = 360f - Vector3.Angle(Vector3.up, input);
            }
            print (inputAngledegrees);
            holdangledata oldselected = selected;
            selected = ReturnNearestRadialItem(inputAngledegrees);
            
            if (selected != oldselected)
            {
                selected.greenimage();
                if (oldselected != null)
                {oldselected.whiteimage();}
            }
            
            arrow.rotation = Quaternion.Euler(0, 0, inputAngledegrees);
        }
    }

    holdangledata ReturnNearestRadialItem(float angle)
    {
        float largestdifference = float.PositiveInfinity;
        holdangledata returned = null;
        foreach(holdangledata h in angledatas)
        {
            if (Mathf.Abs(h.angle-angle ) < largestdifference)
            {
                largestdifference = Mathf.Abs(angle - h.angle);
                returned = h;
            }
        }
        return returned;
    }
}
