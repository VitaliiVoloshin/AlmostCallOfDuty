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
        SetUp();
    }

    void SetUp() {
        weapon = GetComponentInParent<Weapon>();
        nextFire = 0;

        /*speed = weapon._speed;
        howManyBullets = weapon._weaponData.BulletsPerShoot;*/
    }

    public int Shoot()
    {
        int damageDone = 0;

        if (Time.time > nextFire) {
            speed = weapon._speed;
            howManyBullets = weapon._weaponData.BulletsPerShoot;
            damageDone += ShootBullets(howManyBullets);


            nextFire = Time.time + speed;
        }

        return damageDone;
    }

    int ShootBullets(int howmany)
    {
        int damageDone = 0;

        while (howmany != 0)
        {
            Vector3 direction = GetRandomDirection();
           
            BulletInstantiation(direction);
            damageDone += SingleBulletEffect(direction);

            howmany--;
        }
        return damageDone;
    }

    Vector3 GetRandomDirection() {
        return RandomRayPoint(weapon._spread, weapon._weaponData.ShootingRange);
    }

    void BulletInstantiation(Vector3 direction) {

        GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        newBullet.GetComponent<Bullet>().Direction = direction;
        newBullet.GetComponent<Renderer>().material.color = weapon.bulletColor;
    }

    int SingleBulletEffect(Vector3 direction) {
        int damageDone=0;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            if (hit.transform.GetComponent<ManekenController>())
                {
                    ManekenController enemy = hit.transform.GetComponent<ManekenController>();
                    enemy.TakeDamage(weapon._oneBulletDamage);

                    if (enemy.hp - weapon._oneBulletDamage <= 0)
                    {
                        FindObjectOfType<BattleGrounObserver>().AddKill(new KillList { Killer = weapon.owner.name, Victum = enemy.gameObject.name });
                    }
            }
        }

        return damageDone;
    }



    Vector3 RandomRayPoint(float spread, int range) 
    {
        float degree = Random.Range(-spread/2, spread/2);
        Quaternion angle = Quaternion.AngleAxis(degree, new Vector3(0, 1, 0));
        return angle * transform.forward * range;
    }

}


