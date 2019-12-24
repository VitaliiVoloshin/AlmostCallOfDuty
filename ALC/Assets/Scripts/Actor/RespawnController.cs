using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController:MonoBehaviour
{
    private UnitHolder m_unitHolder;
    private GameObject[] m_SpawnPoints;
    private float m_nextScan;

    void Start()
    {
        m_nextScan = 0;
        m_unitHolder = UnitHolder.instance;
        m_SpawnPoints = new GameObject [GameObject.FindGameObjectsWithTag("Respawn").Length];
        m_SpawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        
    }

    void Update()
    {
        if (Time.time > m_nextScan) {
            UnitHolderScan();
            m_nextScan = Time.time+1;
        }
    }

    void UnitHolderScan() {
        
        int i = 0;
        while (i < m_unitHolder.units.Count) {
            if (!m_unitHolder.units[i].activeSelf) {
                if (m_unitHolder.units[i].GetComponent<PlayerController>()) { 
                    RespawnPlayerAtRandomPoint(m_SpawnPoints,m_unitHolder.units[i]);
                } else {
                    RespawnSimpleShooter(m_unitHolder.units[i]);
                }
            }
            i++;
        }
    }

    public void RespawnPlayerAtRandomPoint(GameObject[] spawnPoints, GameObject player) {
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length-1)].transform;
        player.GetComponent<ActorController>().stats.health = 100;
        player.transform.position = point.position;
        player.SetActive(true);
    }

    void RespawnSimpleShooter(GameObject simpleShooter) {
                StartCoroutine(SimpleShooterResurectionProcessStart(5f,simpleShooter));
    }

    IEnumerator SimpleShooterResurectionProcessStart(float delay, GameObject simpleShooter)
    {
        yield return new WaitForSeconds(delay);
        simpleShooter.GetComponent<ActorController>().stats.health = 100;
        simpleShooter.SetActive(true);
    }

    void UISwitcher() {
        if (!FindObjectOfType<PlayerController>())
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
