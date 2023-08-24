using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Character
{
    public PlayerWeapon[] Weapons;
    private PlayerController _myController;
    public float moveSpeed;
    
    
    // Start is called before the first frame update
    void Start()
    {
        InitCharacter();
        _myController = GetComponent<PlayerController>();
        this.tag = "Player";
        Weapons = GetComponentsInChildren<PlayerWeapon>();
    }

    private void StayInBounds()
    {
        if (!_rigidbody2D)
        {
            return;
        }
        
        Debug.Log(GetRigidBodyBounds());
    }

    void Fire()
    {
        if (Weapons.Length == 0) return;
        Weapons[0].Fire();
    }
    // Update is called once per frame
    void Update()
    {
        if (!_myController) return;
        Vector2 moveVector = new Vector2(
            _myController.moveVector.x * moveSpeed,
            _myController.moveVector.y * moveSpeed 
        );

        moveVector *= Time.deltaTime;

        transform.position += new Vector3(
            moveVector.x,
            moveVector.y,
            0
        );
        
        StayInBounds();

        if (_myController.fire.Value > 0.0f)
        {
            Fire();
        }
    }
}
