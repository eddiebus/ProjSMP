using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public UnityEvent OnDeath =  new UnityEvent();
    public string Tag = "Untagged";
    public float health = 1.0f;
    
    protected Rigidbody2D _rigidbody2D;

    private void Start()
    {
        InitCharacter();
    }

    protected void InitCharacter()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        if (_rigidbody2D)
        {
            Debug.Log("Found RigidBody 2D");
        }
    }

    public Bounds GetRigidBodyBounds()
    {
        Bounds boundsTotal = new Bounds(transform.position, Vector3.zero);
        Collider2D[] myColliders = GetComponentsInChildren<Collider2D>();
        Debug.Log($"{myColliders.Length}");
        foreach (Collider2D coll in myColliders)
        {
            boundsTotal.Encapsulate(coll.bounds);
        }
        return boundsTotal;
    }
    
    public void Damage(float Ammount)
    {
        health -= Mathf.Abs(Ammount);
    }
}
