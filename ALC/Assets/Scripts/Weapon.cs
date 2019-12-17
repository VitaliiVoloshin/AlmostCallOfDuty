using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Weapon : MonoBehaviour
{
    [SerializeField]
    public WeaponData _weaponData;

    public float _spread;
    public float _speed;
    public float _oneBulletDamage;
    public Color bulletColor;
    public Object owner;

    void Start()
    {
        _spread = GetSpredingDegree(_weaponData.ShootingRange, _weaponData.Spreading);
        _speed = NormilizedShootingSpeed(_weaponData.ShotsPerSecond * OwnersAtackSpeed());
        _oneBulletDamage = NormilizedDamageDeal(_oneBulletDamage);
        bulletColor = BulletColorDependsOnOwner();

    }

    float GetSpredingDegree(int range, int spreading)
    {
        return 180 - 2 * Mathf.Rad2Deg * Mathf.Atan(range * 2 / spreading);
    }

    float NormilizedDamageDeal(float damage) {

        if (owner as PlayerController)
        {
            PlayerController player = owner as PlayerController;
            return _weaponData.Damage * player.stats.damageCaused;
        }
        else {
            return _weaponData.Damage;
        }
    }

    float NormilizedShootingSpeed(float speed)
    {
        return 1 / speed;
    }

    float OwnersAtackSpeed() {
        if (owner is PlayerController)
        {
            PlayerController temp = owner as PlayerController;
            return temp.stats.attackSpeed;
        }
        else
            return 1;
    }

    Color BulletColorDependsOnOwner()
    {
        if (owner is PlayerController)
        {
            return Color.green;
        }
        else
            return Color.red;
    }

    public int Shoot() {
        return GetComponentInChildren<ShootLogic>().Shoot();
    }
}
