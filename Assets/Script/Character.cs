using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public UnityEvent OnDeath;
    public float health = 1.0f;

    private Rigidbody2D _rigidbody2D;
    
    public void Damage(float Ammount)
    {
        bool canDie = false;
        if (health > 0.0f)
        {
            canDie = true;
        }
        health -= Mathf.Abs(Ammount);
        
        if (health <= 0.0f && canDie)
        {
            OnDeath.Invoke();
        }

    }

    
}
