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
    public ShotInfo shotInfo;
    int currentAmmo;
    int maxAmmo;
    bool isReloading;

    void Start()
    {
        SetUp();
    }

    void SetUp() {

        

        weapon = GetComponentInParent<Weapon>();
        currentAmmo = weapon._weaponData.bulletsInMagazine;
        nextFire = 0;
        maxAmmo = currentAmmo;

        /*speed = weapon._speed;
        howManyBullets = weapon._weaponData.BulletsPerShoot;*/
    }

    public void Shoot()
    {

        if (Time.time > nextFire) {
            speed = weapon._speed;
            howManyBullets = weapon._weaponData.BulletsPerShoot;
            if (isReloading) return;
            if (currentAmmo < 2)
            {
                StartCoroutine(Reload());
            }
            ShootBullets(howManyBullets);
            currentAmmo--;
            nextFire = Time.time + speed;
        }

        /*public void Shoot()
        {
            if (isReloading) return;
            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
            }
            GetComponentInChildren<ShootLogic>().Shoot();
            currentAmmo--;
        }

        IEnumerator Reload()
        {
            isReloading = true;
            yield return new WaitForSeconds(_weaponData.RealadSpeed);
            currentAmmo = maxAmmo;
            isReloading = false;

        }*/
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(weapon._weaponData.RealadSpeed);
        currentAmmo = maxAmmo;
        isReloading = false;

    }

    void ShootBullets(int howmany)
    {

        while (howmany != 0)
        {
            Vector3 direction = GetRandomDirection();
           
            BulletInstantiation(direction);
            if (FindObjectOfType<BattleGrounObserver>())
            {
                if (SingleBulletEffect(direction)!=null)
                FindObjectOfType<BattleGrounObserver>().addShotInfo(SingleBulletEffect(direction));
            }

            howmany--;
        }
    }

    Vector3 GetRandomDirection() {
        return RandomRayPoint(weapon._spread, weapon._weaponData.ShootingRange);
    }

    void BulletInstantiation(Vector3 direction) {

        GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        newBullet.GetComponent<Bullet>().Direction = direction;
        newBullet.GetComponent<Renderer>().material.color = weapon.bulletColor;
    }

    ShotInfo SingleBulletEffect(Vector3 direction) {
        ShotInfo shotInfo = new ShotInfo();
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            if (hit.transform.GetComponent<ManekenController>())
            {
                ManekenController enemy = hit.transform.GetComponent<ManekenController>();
                if (enemy.hp - weapon._oneBulletDamage <= 0)
                {
                    FindObjectOfType<BattleGrounObserver>().AddKill(new KillList { Killer = weapon.owner.name, Victum = enemy.gameObject.name });
                    shotInfo = new ShotInfo { Killer = weapon.owner, Victum = enemy, damage = weapon._oneBulletDamage };

                }
                enemy.TakeDamage(weapon._oneBulletDamage);


            }
            if (hit.transform.GetComponent<PlayerController>())
            {
                PlayerController enemy = hit.transform.GetComponent<PlayerController>();
                if (enemy.stats.health - weapon._oneBulletDamage <= 0)
                {
                    FindObjectOfType<BattleGrounObserver>().AddKill(new KillList { Killer = weapon.owner.name, Victum = enemy.gameObject.name });
                }
                enemy.TakeDamage(weapon._oneBulletDamage);

            }
        }
        return shotInfo;
    }



    Vector3 RandomRayPoint(float spread, int range) 
    {
        float degree = Random.Range(-spread/2, spread/2);
        Quaternion angle = Quaternion.AngleAxis(degree, new Vector3(0, 1, 0));
        return angle * transform.forward * range;
    }

}


