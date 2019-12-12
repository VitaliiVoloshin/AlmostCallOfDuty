using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManekenController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }
    private void Update()
    {
        GetComponentInChildren<WeaponController>().activeWeapon.Shoot();
    }

    // Update is called once per frame
}
