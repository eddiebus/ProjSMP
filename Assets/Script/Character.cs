using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public UnityEvent OnDeath;
    private float _health = 1.0f;
    public float Health
    {
        get { return _health; }
    }

    public void Damage(float Ammount)
    {
        bool canDie = false;
        if (_health > 0.0f)
        {
            canDie = true;
        }
        _health -= Mathf.Abs(Ammount);

        if (_health <= 0.0f && canDie)
        {
            OnDeath.Invoke();
        }
        return;
    }
    public void Heal(float Ammount)
    {
        _health += Ammount;
        return;
    }
}
