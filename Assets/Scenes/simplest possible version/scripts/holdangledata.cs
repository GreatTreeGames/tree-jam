using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class holdangledata : MonoBehaviour
{
    public float angle;
    public Image background;

    public UnityEvent onSubmit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void whiteimage()
    {
        background.color = Color.white;
    }
    public void greenimage()
    {
        background.color = Color.green;
    }

}
