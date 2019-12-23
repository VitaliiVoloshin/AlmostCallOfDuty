using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManekenController : MonoBehaviour
{
    public float hp;
    WeaponController weaponController;
    public StatsController stats;
    // Start is called before the first frame update
    private void Awake()
    {
        stats = GetComponent<StatsController>();
    }
    void Start()
    {
        weaponController = GetComponentInChildren<WeaponController>();
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
