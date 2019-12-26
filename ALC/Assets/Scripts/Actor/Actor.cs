using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShooterFeatures
{
    public enum Fraction
    {
        RedTeam,
        GreenTeam
    }

    public abstract class ActorController: MonoBehaviour
    {
        public Fraction fraction;

        [SerializeField] public ActorStats stats;
        protected WeaponController m_weaponController;

        private Transform m_Transform;

        private void Start()
        {
            AddToUnitHolder(UnitHolder.instance);
            m_Transform = gameObject.transform;
            m_weaponController = GetComponentInChildren<WeaponController>();
        }

        private void OnDisable()
        {
            if (RespawnController.instance) {
                RespawnController.instance.RespawnRequest(gameObject);
            }
        }

        public void Death()
        {
            if (stats != null && (stats.health <= 0 || m_Transform.position.y <= -10)) {
                gameObject.SetActive(false);
            }
        }

        protected void AddToUnitHolder(UnitHolder unitHolder)
        {
            unitHolder.units.Add(gameObject);
            transform.parent = unitHolder.transform;
        }

        public void TakeDamage(float damage)
        {
            stats.health -= damage * stats.damageTaken;
        }

        public void Shoot()
        {
            if (m_weaponController && stats)
                m_weaponController.activeWeapon.Shoot();
        }
    }
}