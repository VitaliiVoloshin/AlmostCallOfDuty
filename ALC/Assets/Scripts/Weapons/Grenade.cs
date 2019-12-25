using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShooterFeatures
{
    public class Grenade: MonoBehaviour
    {
        public Vector3[] trajectory;
        public GameObject mesh;
        public ParticleSystem explotionEffect;
        public Object owner;

        private int m_Index;

        void Start()
        {
            m_Index = 0;
            StartCoroutine(Detonate(1f));
        }

        void Update()
        {
            if (trajectory != null) {
                if (m_Index <= trajectory.Length - 1) {
                    Debug.DrawLine(gameObject.transform.position, trajectory[m_Index], Color.yellow, 1f);
                    transform.position = trajectory[m_Index];
                    m_Index += 2;
                }
            }
        }

        private IEnumerator Detonate(float value)
        {
            yield return new WaitForSeconds(value);
            explotionEffect.Play();
            mesh.SetActive(false);
            UnitHolder temp = FindObjectOfType<UnitHolder>();
            List<GameObject> units = temp.units;
            for (int i = 0; i < temp.units.Count; i++) {
                if (units[i] != null) {
                    if (Vector3.Distance(transform.position, units[i].transform.position) <= 5) {
                        Debug.DrawLine(transform.position, units[i].transform.position, Color.red, 2f);
                        Debug.DrawRay(gameObject.transform.position, units[i].transform.position - transform.position, Color.green, 2f);

                        RaycastHit hit;
                        if (Physics.Raycast(transform.position, units[i].transform.position - transform.position, out hit)) {

                            if (hit.transform.GetComponent<ActorController>()) {
                                ActorController enemy = hit.transform.GetComponent<ActorController>();
                                enemy.TakeDamage(50);

                                if (enemy.stats.health - 50 <= 0) {
                                    FindObjectOfType<BattleGrounObserver>().AddKill(new KillList { Killer = owner.name, Victum = enemy.gameObject.name });
                                }
                            }
                        }
                    }
                }
            }
            Invoke(nameof(Destroy), 1f);
        }

        void Destroy()
        {
            Destroy(gameObject);
        }
    }
}