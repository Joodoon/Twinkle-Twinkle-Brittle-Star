using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera playerVCam;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && playerVCam.Priority == 0)
        {
            playerVCam.Priority = 2;
        }
        else if(Input.GetKeyDown(KeyCode.Tab) && playerVCam.Priority == 2)
        {
            playerVCam.Priority = 0;
        }
    }
}
