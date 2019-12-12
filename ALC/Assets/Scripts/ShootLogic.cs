using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLogic : MonoBehaviour
{
    private Weapon weapon;
    public float speed; 
    public float nextFire;
    public int howManyBullets;
    public GameObject bullet;


    void Start()
    {
        weapon = GetComponentInParent<Weapon>();
        nextFire = 0;
        speed = weapon._speed;
        howManyBullets = weapon._weaponData.BulletsPerShoot;
    }
    void Update()
    {
         
    }

    public void Shoot()
    {

        if (Time.time > nextFire) { 
                speed = weapon._speed;
                howManyBullets = weapon._weaponData.BulletsPerShoot;

            while (howManyBullets != 0)
            {
                RaycastHit hit;
                Vector3 randomDirection = RandomRayPoint(weapon._spread, weapon._weaponData.ShootingRange);

                Vector3 position = transform.position;
                GameObject newBullet = Instantiate(bullet, position, transform.rotation) as GameObject;

                Bullet bull = newBullet.GetComponent<Bullet>();
                bull.Direction = randomDirection;

                if (Physics.Raycast(transform.position, randomDirection, out hit))
                {
                    Debug.Log(hit.transform.name);
                    if (hit.transform.GetComponent<PlayerController>()) {
                        hit.transform.GetComponent<PlayerController>().TakeDamage(weapon._weaponData.Damage);
                    }
                    if (hit.transform.GetComponent<ManekenController>())
                    {
                        hit.transform.GetComponent<ManekenController>().TakeDamage(weapon._weaponData.Damage);
                    }        
                }
                Debug.DrawRay(transform.position, RandomRayPoint(weapon._spread, weapon._weaponData.ShootingRange), Color.yellow, 1);
                howManyBullets--;
            }
            nextFire = Time.time + speed;
        }
    }

    Vector3 RandomRayPoint(float spread, int range) 
    {
        float degree = Random.Range(-spread/2, spread/2);
        Quaternion angle = Quaternion.AngleAxis(degree, new Vector3(0, 1, 0));
        return angle * transform.forward * range;
    }

}


