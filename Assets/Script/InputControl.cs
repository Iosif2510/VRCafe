using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;

public class InputControl : MonoBehaviour
{
    public ControlUI controlUI;
    public SteamVR_LaserPointer laserPointer;
    
    public SteamVR_Action_Boolean Menu;
    
    // Update is called once per frame
    void Update()
    {
        ShowMenu();
    }

    private void ShowMenu()
    {
        if (Menu.GetStateDown(SteamVR_Input_Sources.Any))
        {
            controlUI.gameObject.SetActive(true);
            laserPointer.enabled = true;
            Debug.Log("Button Down");
        }
        else if (Menu.GetStateUp(SteamVR_Input_Sources.Any))
        {
            controlUI.gameObject.SetActive(false);
            laserPointer.enabled = false;
            Debug.Log("Button Up");
        }
    }
}
