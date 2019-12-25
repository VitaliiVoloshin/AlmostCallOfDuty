using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShooterFeatures
{
    public class ShootLogic
    {
        public Transform shootPoint;

        public string[] ShootBullets(WeaponData weaponData)
        {

            int howmany = weaponData.BulletsPerShoot;
            string[] victums = new string[howmany];
            int i = 0;
            while (howmany != 0) {
                Vector3 direction = GetRandomDirection(new Vector2(weaponData.spreadingDegree, weaponData.ShootingRange));
                BulletInstantiation(direction);
                victums[i] = SingleBulletEffect(direction, weaponData);
                howmany--;
            }
            return null;
        }

        Vector3 GetRandomDirection(Vector2 direction)
        {
            return RandomRayPoint(direction.x, direction.y);
            //return RandomRayPoint(weapon._spread, weapon._weaponData.ShootingRange);
        }

        Vector3 RandomRayPoint(float spread, float range)
        {
            float degree = Random.Range(-spread / 2, spread / 2);
            Quaternion angle = Quaternion.AngleAxis(degree, new Vector3(0, 1, 0));
            return angle * shootPoint.forward * range;
        }

        void BulletInstantiation(Vector3 direction)
        {

            GameObject newBullet = Object.Instantiate(Resources.Load("Bullet"), shootPoint.position, shootPoint.rotation) as GameObject;
            newBullet.GetComponent<Bullet>().Direction = direction;
            newBullet.GetComponent<Renderer>().material.color = Color.blue;
        }

        string SingleBulletEffect(Vector3 direction, WeaponData weaponData)
        {
            RaycastHit hit;
            if (Physics.Raycast(shootPoint.position, direction, out hit)) {

                if (hit.transform.GetComponent<ActorController>()) {

                    ActorController enemy = hit.transform.GetComponent<ActorController>();
                    if (enemy.stats.health - weaponData.Damage <= 0) {
                        enemy.stats.health -= weaponData.Damage;
                        return enemy.name;
                    } else {
                        enemy.stats.health -= weaponData.Damage;
                    }
                }
            }
            return "";
        }
    }
}