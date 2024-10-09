using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    public Camera mainCamera;

    void Start()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float orthographicSize = (35 * (screenHeight/screenWidth)) / ((float)1920/1080); 
        mainCamera.orthographicSize = orthographicSize;
    }
        
}
