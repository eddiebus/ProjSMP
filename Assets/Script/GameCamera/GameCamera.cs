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
        float TargetRatio = TargetSize.x / TargetSize.y;
        float ActualRatio = Screen.width / Screen.height;
        float AspectRatioWidth = Screen.height * (TargetSize.x / TargetSize.y);
        _myCamera.projectionMatrix = Matrix4x4.Ortho(
            -TargetSize.x / 2.0f,
            TargetSize.x / 2.0f,
            -TargetSize.y / 2,
            TargetSize.y / 2,
            0.3f,1000
            );
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
        Debug.Log("Hello Update Call");
    }
}
