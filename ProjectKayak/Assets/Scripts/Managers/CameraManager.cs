using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Camera[] cameras;
    private Camera activeCamera;
    private int activeIndex;
    [SerializeField] private Camera defaultCamera;

    // Start is called before the first frame update
    void Start()
    {
        cameras = FindObjectsOfType<Camera>();

        //turn off non active
        foreach(Camera cam in cameras)
        {
            cam.enabled = false;
        }

        //turn on default camera- if none avaiable use first in list
        if(defaultCamera == null) { defaultCamera = cameras[0]; }
        activeCamera = defaultCamera;
        activeCamera.enabled = true;
        activeIndex = System.Array.IndexOf(cameras, activeCamera);
    }

    public void CycleCamera()
    {
        //disable current camera
        activeCamera.enabled = false;

        //up active cam
        activeIndex = (activeIndex + 1) % (cameras.Length);
        activeCamera = cameras[activeIndex];
        activeCamera.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            CycleCamera();
        }
    }
}
