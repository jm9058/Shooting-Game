using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    private void Start() {
        SetResolution();
    }

    public void SetResolution()
    {
        //10:16 비율로 설정
        int setWidth    = 450;
        int setHright   = 720;

       Screen.SetResolution(setWidth, setHright, false);
    }
}