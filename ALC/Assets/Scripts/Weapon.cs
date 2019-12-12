﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Weapon : MonoBehaviour
{
    [SerializeField]
    public WeaponData _weaponData;

    public float _spread;
    public float _speed;

    void Start()
    {
        _spread = GetSpredingDegree(_weaponData.ShootingRange,_weaponData.Spreading);
        _speed = NormilizedShootingSpeed(_weaponData.ShotsPerSecond);
        Debug.Log("weaponInit");
    }


    float GetSpredingDegree(int range, int spreading)
    {
        return 180 - 2 * Mathf.Rad2Deg * Mathf.Atan(range * 2 / spreading);
    }

    float NormilizedShootingSpeed(float speed)
    {
        return 1 / speed;
    }

    public void Shoot() {
        GetComponentInChildren<ShootLogic>().Shoot();
    }
}