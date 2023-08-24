using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[ExecuteAlways]
public class GameCamera : MonoBehaviour
{
    private Camera _myCamera;
    private GameController gameController;
    public Vector2 TargetSize = new Vector2(9, 20);
    private float CameraHeight = 1280.0f;
    private void _UpdateProperties()
    {
        float TargetWidthRatio = gameController.SceneSize.x / gameController.SceneSize.y;
        float ActualWidthRatio = (float)Screen.width / Screen.height;

        Vector2 CameraSize = new Vector2(gameController.SceneSize.y * ActualWidthRatio, gameController.SceneSize.y);

        // Screen is too thin
        // Grow vertical space
        if (ActualWidthRatio < TargetWidthRatio)
        {
            float difference = TargetWidthRatio - ActualWidthRatio;
            CameraSize.x = gameController.SceneSize.x;
            CameraSize.y = gameController.SceneSize.y * (1 + difference);
        }
        _myCamera.projectionMatrix = Matrix4x4.Ortho(
            (float)-CameraSize.x / 2.0f,
            (float)CameraSize.x / 2.0f,
            -CameraSize.y / 2,
            CameraSize.y / 2,
            0.3f, 1000
            ); 
        
    } 
    // Start is called before the first frame update
    void Start()
    {
        _myCamera = GetComponent<Camera>();
        GameObject gameControllerObj = GameObject.FindGameObjectWithTag("GameController");
        gameController = gameControllerObj.GetComponent<GameController>();
        if (!gameController)
        {
            Debug.LogWarning($"GameCamera{this.name} - Cannot Find GameController in scene");
            this.enabled = false;
        }
        else if (!_myCamera)
        {
            Debug.LogWarning($"GameCamera({this.name}) - Does not have a Camera Component to function");
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _UpdateProperties();
    }
}
