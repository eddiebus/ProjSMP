using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    private Camera _myCamera;
    private float CameraHeight = 720.0f;
    private void _UpdateProperties()
    {

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
    }
}
