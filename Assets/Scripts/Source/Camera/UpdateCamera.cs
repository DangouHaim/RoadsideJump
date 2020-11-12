using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCamera : MonoBehaviour
{
    private GameObject _pCamera;
    private GameObject _lCamera;

    void Start()
    {
        _pCamera = transform.Find("PCamera").gameObject;
        _lCamera = transform.Find("LCamera").gameObject;
    }

    void FixedUpdate()
    {
        DetectCameraOrientation();
    }
    
    private void DetectCameraOrientation()
    {
        if(Screen.orientation == ScreenOrientation.Portrait 
            || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
        {
            _lCamera.SetActive(false);
            _pCamera.SetActive(true);
        }
        else
        {
            _lCamera.SetActive(true);
            _pCamera.SetActive(false);
        }
    }
}
