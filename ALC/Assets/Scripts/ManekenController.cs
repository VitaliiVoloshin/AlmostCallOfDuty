using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManekenController : MonoBehaviour
{
    public float hp;
    WeaponController weaponController;
    public UnitStats stats = new UnitStats();
    // Start is called before the first frame update
    void Start()
    {
        stats.fraction = UnitStats.Fraction.red;
        weaponController = GetComponentInChildren<WeaponController>();
        hp = 100;
        name = transform.gameObject.name;
    }
    private void Update()
    {
        if (weaponController) {
            weaponController.activeWeapon.Shoot();
        }

        if (stats.health <= 0) {
            Die(); 
        }
    }

    public void TakeDamage(float damage) {
        stats.health -= damage;
    }



    void Die() {
        Destroy(gameObject);
    }
}
