using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    private Camera _myCamera;
    private float CameraHeight = 1280.0f;
    private void _UpdateProperties()
    {
        float width = Screen.width;
        if (width < 576)
        {
            width = 576;
        }
        _myCamera.projectionMatrix = Matrix4x4.Ortho(
            (float)-width / 2.0f,
            (float)width / 2.0f,
            -CameraHeight / 2,
            CameraHeight / 2,
            0.3f,1000
            );

        float TargetHeightRatio = (float)20 / 9;
        float CurrentHeightRatio = (float)Screen.height / Screen.width;
        Debug.Log($"Target = {TargetHeightRatio} | Current = {CurrentHeightRatio}");



    } 
    // Start is called before the first frame update
    void Start()
    {
        _myCamera = GetComponent<Camera>();
        if (!_myCamera)
        {
            Debug.LogWarning($"GameCamera({this.name}) - Does not have a Camera Component to function");
            this.enabled = false;
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _UpdateProperties();
    }
}
