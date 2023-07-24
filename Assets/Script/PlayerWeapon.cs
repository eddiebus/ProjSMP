using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    public Bullet2D BulletType;
    public float FireDelay;
    private float _TimeTillFire;

    public void Fire()
    {
        if (_TimeTillFire <= 0.0f)
        {
            GameObject.Instantiate(BulletType,this.transform.position,Quaternion.identity);
            _TimeTillFire = FireDelay;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _TimeTillFire -= Time.deltaTime;
        if (_TimeTillFire < 0) _TimeTillFire = 0;
    }
}
