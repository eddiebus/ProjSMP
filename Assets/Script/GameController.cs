using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public UnityEvent OnLevelLoad;
    public readonly Vector2 SceneSize = new Vector2(9, 20);
    protected float _TimeActive;

    //Search scene with existing GameController
    //Return True if older GameController is found
    private bool FindExistingGameController()
    {
        GameObject[] otherControllers = GameObject.FindGameObjectsWithTag("GameController");
        foreach (var Controller in otherControllers)
        {
            var gmComponent = Controller.GetComponent<GameController>();
            if (gmComponent)
            {
                if (gmComponent._TimeActive > this._TimeActive)
                {
                    // Older GameController has been found
                    return true; 
                }
            }
        }
        // No older game controller found
        return false;
    }
    
    private void Init()
    {
        if (FindExistingGameController())
        {
            GameObject.Destroy(this.gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        _TimeActive += Time.deltaTime;
    }
}
