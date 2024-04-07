using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class InputData : MonoBehaviour
{
    public InputDevice rightController;
    public InputDevice leftController;
    // Start is called before the first frame update
    void Start()
    {
        if (!rightController.isValid || leftController.isValid )
        {
            InitializateInputDevices();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!rightController.isValid){
            InitializateInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, ref rightController);
        }
    }

     private void InitializateInputDevices()
    {
        if (!rightController.isValid)
            InitializateInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, ref rightController);

        if (!leftController.isValid)
            InitializateInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, ref leftController);
    }

    private void InitializateInputDevice(InputDeviceCharacteristics characteristics, ref InputDevice inputDevice)
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(characteristics,devices);
        if(devices.Count>0){
            inputDevice = devices[0];
        }
    }
}
