using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowDialInput : MonoBehaviour
{

    public RectTransform arrow;
    public GameObject selected;
    public RadialLayout layout;
    // Start is called before the first frame update
    void Start()
    {
        
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

            selected = ReturnNearestRadialItem(inputAngledegrees,layout );
            arrow.rotation = Quaternion.Euler(0, 0, inputAngledegrees);
        }
    }

    GameObject ReturnNearestRadialItem(float angle, RadialLayout layout)
    {
        //for an angle
        return null; 
    }
}
