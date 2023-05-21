using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class InputCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        ClickGrab();
        ClickMenu();
        ClickTrigger();
        ClickTrackPad();
    }
    public SteamVR_Action_Boolean Grab;
    public SteamVR_Action_Boolean Menu;
    public SteamVR_Action_Boolean Trigger;
    public SteamVR_Action_Boolean TrackPad;

    void ClickMenu()
    {
        if (Menu.GetStateDown(SteamVR_Input_Sources.Any))
        {
            Debug.Log("Menu Button Down");
        }
    }

    void ClickTrigger()
    {
        if (Trigger.GetState(SteamVR_Input_Sources.Any))
        {
            Debug.Log("Menu Button Down");
        }
    }

    void ClickTrackPad()
    {
        if (TrackPad.GetState(SteamVR_Input_Sources.LeftHand))
        {
            Debug.Log("Left Pad Click");
        }
        else if (TrackPad.GetState(SteamVR_Input_Sources.RightHand))
        {
            Debug.Log("Right Pad Click");
        }

    }

    void ClickGrab()
    {
        if (Grab.GetStateDown(SteamVR_Input_Sources.Any))
        {
            Debug.Log("Grab Button Down");
        }
    }
    
}
