using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShooterFeatures
{
    public class SimpleShooter: ActorController
    {
        private WeaponController m_WeaponController;

        void Awake()
        {
            stats.health = 100;
            m_WeaponController = GetComponentInChildren<WeaponController>();
            name = transform.gameObject.name;
        }
        private void Update()
        {
            Death();
            if (m_WeaponController) {
                Shoot();
            }
        }
    }
}
