using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using Valve.VR;
using Valve.VR.Extras;

public class InputControl : MonoBehaviour
{
    public Camera myHead;
    
    public GameObject controlUI;
    // public SteamVR_LaserPointer laserPointer;
    
    public SteamVR_Action_Boolean Menu;
    public SteamVR_Action_Vector2 joyStick;
    
    // Update is called once per frame
    void Update()
    {
        ShowMenu();
    }

    private void FixedUpdate()
    {
        Vector3 moveDir = myHead.transform.TransformDirection(new Vector3(joyStick.axis.x, 0, joyStick.axis.y));
        transform.position += Vector3.ProjectOnPlane(2.0f * Time.deltaTime * moveDir, Vector3.up);
    }

    private void ShowMenu()
    {
        if (Menu.GetStateDown(SteamVR_Input_Sources.Any))
        {
            controlUI.SetActive(true);
            // laserPointer.active = true;
            Debug.Log("Button Down");
        }
        else if (Menu.GetStateUp(SteamVR_Input_Sources.Any))
        {
            controlUI.SetActive(false);
            // laserPointer.active = false;
            Debug.Log("Button Up");
        }
    }
}
