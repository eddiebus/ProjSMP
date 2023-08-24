using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    
    // Start is called before the first frame update
    void Start()
    {
        InitCharacter();
        Tag = "Enemy";
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
