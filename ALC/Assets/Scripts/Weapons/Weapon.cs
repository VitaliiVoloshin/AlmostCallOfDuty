using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponIdentificator
{
    shotgun,
    automaticRifle, 
    grenade
}

public class Weapon : MonoBehaviour
{
    public Transform shootPoint;
    private ShootLogic shootLogic = new ShootLogic();
    [SerializeField] public WeaponData _weaponData;
    BattleGrounObserver killJournal;
    private float m_speed;
    public float _oneBulletDamage;
    public Color bulletColor;
    public ActorController owner;
    private float m_nextFire;
    int currentAmmo;
    int maxAmmo;
    bool isReloading;

    void Start()
    {
        killJournal = BattleGrounObserver.instance;
        shootLogic.shootPoint = shootPoint;
        currentAmmo = _weaponData.bulletsInMagazine;
        m_speed = NormilizedShootingSpeed(_weaponData.ShotsPerSecond);
        m_nextFire = 0;
        maxAmmo = currentAmmo;
        bulletColor = BulletColorDependsOnOwner();
    }

    void AddOwnerStatsToWeapon() {
        _weaponData.Damage *= owner.stats.damageCaused;
        float convert = _weaponData.ShotsPerSecond;
        convert *= owner.stats.attackSpeed;
        _weaponData.ShotsPerSecond = Mathf.RoundToInt(convert);
    }

    float NormilizedShootingSpeed(float speed)
    {
        return 1 / speed;
    }

    Color BulletColorDependsOnOwner()
    {
        if (owner is PlayerController)
        {
            return Color.green;
        }
        else
            return Color.red;
    }

    public void Shoot()
    {
        string[] enemies = new string[_weaponData.BulletsPerShoot];
        if (Time.time > m_nextFire)
        {

            if (isReloading) return;
            if (currentAmmo < 2)
            {
                StartCoroutine(Reload());
            }
            enemies = shootLogic.ShootBullets(_weaponData);
            currentAmmo--;
            m_nextFire = Time.time + m_speed;
        }
    }

    void AddKilledEnemiesToJournal(string[] deadEnemies) {
        
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(2f);
        currentAmmo = maxAmmo;
        isReloading = false;

    }

}
