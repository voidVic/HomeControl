using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class LifxMarker_script : MonoBehaviour, IVirtualButtonEventHandler
{
    public GameObject lifxVB;
    // Start is called before the first frame update
    void Start()
    {
        lifxVB = GameObject.Find("LifxVirtualBtn");
        lifxVB.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);

        
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        Debug.Log("Button Pressed");
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
