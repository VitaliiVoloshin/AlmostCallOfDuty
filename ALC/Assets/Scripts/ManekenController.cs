using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManekenController : MonoBehaviour
{
    public int hp;
    // Start is called before the first frame update
    void Start()
    {
        hp = 100;
        name = transform.gameObject.name;
    }
    private void Update()
    {
        GetComponentInChildren<WeaponController>().activeWeapon.Shoot();

        if (hp <= 0) {
            //FindObjectOfType<RespawnController>().UnitRespawn(transform.position, transform.rotation);
            Destroy(gameObject);   
        }
    }

    public void TakeDamage(int damage) {
        hp -= damage;
    }



   
}
