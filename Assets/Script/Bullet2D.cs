using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet2D : MonoBehaviour
{
    public float speed;
    public float damageValue = 0.1f;
    public GameObject TargetObject;
    public Vector2 AimVector = Vector2.up;
    public float LifeTime = 100; //How Long until this object will destroy itself
    public string[] FriendTags = { }; // Tags the bullet will not damage
    private Rigidbody2D _rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
        if (!_rigidbody2D) _rigidbody2D = this.AddComponent<Rigidbody2D>();
        _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
    }
    
    
    private void Move()
    {
        LifeTime -= Time.deltaTime;
        if (TargetObject != null)
        {
            Vector3 dirToObject = (TargetObject.transform.position - this.transform.position).normalized;
            Vector3 moveVector = dirToObject * speed;
            _rigidbody2D.MovePosition(this.transform.position + moveVector);
        }
        else
        {
            AimVector = AimVector.normalized;
            Vector2 moveVector = AimVector.normalized * (speed * Time.deltaTime);
            _rigidbody2D.MovePosition(this.transform.position + (Vector3)moveVector);
        }
        
    }

    

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("asdasd");
        GameObject otherObj = col.gameObject;
        foreach (var tag in FriendTags)
        {
            if (tag == otherObj.tag)
            {
                return;
            }
        }
        Character otherCharacter = otherObj.GetComponent<Character>();
        if (otherCharacter)
        {
            otherCharacter.Damage(damageValue);
        }
        
        
    }
}
