using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[

CreateAssetMenu(fileName = "New WeaponData", menuName = "Weapon Data", order = 51)]
public class WeaponData : ScriptableObject
{
    [SerializeField]
    private string _identificator;
    [SerializeField]
    private int _shotsPerSecond;
    [SerializeField]
    private float _reloadSpeed;
    [SerializeField]
    private float _damage;
    [SerializeField]
    private int _bulletsPerShoot;
    //[SerializeField]
    //private Sprite _icon;
    [SerializeField]
    private int _shootingRange;
    [SerializeField]
    private int _spreading;
    [SerializeField]
    private int _bulletsInMagazine;



    public string Identificator
    {
        get
        {
            return _identificator;
        }
    }

    public int ShotsPerSecond
    {
        get
        {
            return _shotsPerSecond;
        }
    }

    public float RealadSpeed 
    {
        get
        {
            return _reloadSpeed;
        }
    }

    public float Damage
    {
        get
        {
            return _damage;
        }
    }

    public int BulletsPerShoot 
    {
        get
        {
            return _bulletsPerShoot;
        }
    }

    /*public Sprite Icon
    {
        get
        {
            return _icon;
        }
    }*/

    public int ShootingRange    
    {
        get
        {
            return _shootingRange;
        }
    }

    public int Spreading
    {
        get
        {
            return _spreading;
        }
    }

    public int bulletsInMagazine
    {
        get
        {
            return _bulletsInMagazine;
        }
    }

}
