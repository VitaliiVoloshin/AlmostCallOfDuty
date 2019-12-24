using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    /*public Vector3[] trajectory;
    public float force;
    int index;
    private Vector3[] cleanTrajectory;
    public GameObject mesh;
    public ParticleSystem explotionEffect;
    public Object owner;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        StartCoroutine(Detonate(1f));
      
    }

    // Update is called once per frame
    void Update()
    {
        if (trajectory != null)
        {
            if (index <= trajectory.Length-1)
            {
                Debug.DrawLine(gameObject.transform.position, trajectory[index], Color.yellow, 1f);
                transform.position = trajectory[index];
                index+=2;
            }

        }
    }


    private IEnumerator Detonate(float value) {
        yield return new WaitForSeconds(value);
        explotionEffect.Play();
        mesh.SetActive(false);
        UnitHolder temp = FindObjectOfType<UnitHolder>();
        List<GameObject> manekens = temp.manekens;
        for (int i = 0; i < temp.manekens.Count; i++)
        {
            if (manekens[i] != null)
            {
                if (Vector3.Distance(transform.position, manekens[i].transform.position) <= 5)
                {
                    Debug.DrawLine(transform.position, manekens[i].transform.position, Color.red, 2f);
                    Debug.DrawRay(gameObject.transform.position, manekens[i].transform.position - transform.position, Color.green, 2f);

                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, manekens[i].transform.position - transform.position, out hit))
                    {
                        
                        if (hit.transform.GetComponent<ManekenController>())
                        {
                            ManekenController enemy = hit.transform.GetComponent<ManekenController>();
                            enemy.TakeDamage(50);

                            if (enemy.hp - 50 <= 0)
                            {
                                FindObjectOfType<BattleGrounObserver>().AddKill(new KillList { Killer = owner.name, Victum = enemy.gameObject.name });
                            }
                        }
                    }
                }
            }
        }



        Invoke(nameof(Destroy), 1f);

        
    }

    void Destroy() {
        Destroy(gameObject);
    }

    */
}
