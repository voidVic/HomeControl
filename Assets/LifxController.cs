using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class LifxController : MonoBehaviour, ITrackableEventHandler
{
    public GameObject lifxOptions;
    public GameObject lifxSwitch;
    public GameObject lifxRed;
    public GameObject lifxGreen;
    public GameObject lifxBlue;


    string btnName;
    public string deviceName;

    public bool switchState = false;

    public Button testBtn;


    Quaternion lifxOptionsOR, lifxSwitchOR;
    // Start is called before the first frame update
    void Start()
    {
        AdapterService ass = gameObject.AddComponent<AdapterService>();
        ass.GetAuthToken();
        lifxOptions = GameObject.Find(deviceName + "OptionToggle");
        lifxSwitch = GameObject.Find(deviceName + "SwitchToggle");

        //find colors
        lifxRed = GameObject.Find(deviceName + "Red");
        lifxGreen = GameObject.Find(deviceName + "Green");
        lifxBlue = GameObject.Find(deviceName + "Blue");

        lifxSwitch.GetComponent<Renderer>().enabled = false;

        lifxOptionsOR = lifxOptions.transform.rotation;
        lifxSwitchOR = lifxSwitch.transform.rotation;


        testBtn.onClick.AddListener(SetSwitchToggle);
    }

    void RepositionPlanes()
    {
        Quaternion camRotation = Camera.main.transform.rotation;
        lifxOptions.transform.rotation = camRotation * lifxOptionsOR;
        lifxSwitch.transform.rotation = camRotation * lifxSwitchOR;
    }

    void RepositionLabels()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RepositionPlanes();
        RepositionLabels();
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out RaycastHit Hit))
            {
                btnName = Hit.transform.name;
                Debug.Log(btnName);

                if(btnName == deviceName + "OptionToggle")
                {
                    //lifxSwitch.GetComponent<Renderer>().enabled = !lifxSwitch.GetComponent<Renderer>().enabled;
                    switchState = !switchState;
                    SetSwitch(switchState ? "ON" : "OFF");

                }

                /*if (btnName == deviceName + "SwitchToggle")
                {
                    switchState = !switchState;
                    SetSwitch(switchState ? "ON" : "OFF");
                }*/

                if (btnName == deviceName + "Red")
                {
                    switchState = true;
                    SetColor("Red");
                }
                if (btnName == deviceName + "Green")
                {
                    switchState = true;
                    SetColor("Green");
                }
                if (btnName == deviceName + "Blue")
                {
                    switchState = true;
                    SetColor("Blue");
                }
            }
        }
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        Debug.Log("aaaa gayaaa!!!!");
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            OnTrackingFound();
        else
            OnTrackingLost();
    }

    private void OnTrackingFound()
    {
        Debug.Log("mil gayaaa!!!!");
        //if (transform.childCount > 0)
            //SetChildrenActive(true);
    }

    private void OnTrackingLost()
    {
        Debug.Log("kho gayaaa!!!!");
        //if (transform.childCount > 0)
            //SetChildrenActive(false);
    }

    private void SetChildrenActive(bool setActive)
    {
        for(int i = 0; i <= transform.childCount; i++)
        {
            transform.GetChild(i++).gameObject.SetActive(setActive);
        }
    }

    public void SetSwitch(string toggleState)
    {
        AdapterService ass = gameObject.AddComponent<AdapterService>();
        ass.SetSwitch(toggleState, deviceName);
    }

    public void SetColor(string color)
    {
        AdapterService ass = gameObject.AddComponent<AdapterService>();
        ass.SetColor(color, deviceName);
    }

    public void SetSwitchToggle()
    {
        switchState = !switchState;
        SetSwitch(switchState ? "ON" : "OFF");
    }

    public int checkInternetStrength()
    {
       _ = Application.internetReachability;

        return 0;
    }
}

