using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShooter : ActorController
{
    WeaponController weaponController;

    void Awake()
    {
        stats.health = 100;
        weaponController = GetComponentInChildren<WeaponController>();
        name = transform.gameObject.name;
    }
    private void Update()
    {
        Death();
        if (weaponController) {
            Shoot();
        }
    }
}
