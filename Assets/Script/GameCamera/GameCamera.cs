using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[ExecuteAlways]
public class GameCamera : MonoBehaviour
{
    private Camera _myCamera;
    public Vector2 TargetSize = new Vector2(9, 20);
    private float CameraHeight = 1280.0f;
    private void _UpdateProperties()
    {
        float TargetWidthRatio = TargetSize.x / TargetSize.y;
        float ActualWidthRatio = (float)Screen.width / Screen.height;

        Vector2 CameraSize = new Vector2(TargetSize.y * ActualWidthRatio, TargetSize.y);

        // Screen is too thin
        // Grow vertical space
        if (ActualWidthRatio < TargetWidthRatio)
        {
            float difference = TargetWidthRatio - ActualWidthRatio;
            CameraSize.x = TargetSize.x;
            CameraSize.y = TargetSize.y * (1 + difference);
        }

        _myCamera.projectionMatrix = Matrix4x4.Ortho(
            (float)-CameraSize.x / 2.0f,
            (float)CameraSize.x / 2.0f,
            -CameraSize.y / 2,
            CameraSize.y / 2,
            0.3f, 1000
            ); ;
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
