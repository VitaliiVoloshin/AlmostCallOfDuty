using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActorController : MonoBehaviour {

    [SerializeField] public ActorStats stats;
    [SerializeField] protected WeaponController m_weaponController;
    public float hp;

    public void Death() {
        if (stats.health <= 0) {
            gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        AddToUnitHolder();


    }
    protected void AddToUnitHolder() {
        UnitHolder unitHolder = UnitHolder.instance;
        unitHolder.units.Add(GetComponent<ActorController>().gameObject);
        transform.parent = unitHolder.transform;
    }

    public void TakeDamage(float damage) {
        stats.health -= damage * stats.damageTaken;
    }

    public void Shoot() {
         m_weaponController.activeWeapon.Shoot();
    }

}

