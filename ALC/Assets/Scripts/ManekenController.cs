using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManekenController : MonoBehaviour
{
    public int hp;
    WeaponController weaponController;
    // Start is called before the first frame update
    void Start()
    {
        weaponController = GetComponentInChildren<WeaponController>();
        hp = 100;
        name = transform.gameObject.name;
    }
    private void Update()
    {
        if (weaponController) {
            weaponController.activeWeapon.Shoot();
        }
        //GetComponentInChildren<WeaponController>().activeWeapon.Shoot();

        if (hp <= 0) {
            Die(); 
        }
    }

    public void TakeDamage(int damage) {
        hp -= damage;
    }



    void Die() {
        Destroy(gameObject);
    }
}
