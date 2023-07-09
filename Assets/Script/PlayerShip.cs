using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Character
{
    private PlayerController myController;
    public float moveSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        myController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!myController) return;
        Vector2 moveVector = new Vector2(
            myController.moveVector.x * moveSpeed,
            myController.moveVector.y * moveSpeed 
        );

        moveVector *= Time.deltaTime;

        transform.position += new Vector3(
            moveVector.x,
            moveVector.y,
            0
        );
    }
}
