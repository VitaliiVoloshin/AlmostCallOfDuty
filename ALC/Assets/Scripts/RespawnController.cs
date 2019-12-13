using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    public GameObject maneken;
    private PlayerController player;

    void Start()
    {
    }

    void Update()
    {

        if (!FindObjectOfType<PlayerController>())
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void UnitRespawn(Vector3 position, Quaternion rotation) {
        GameObject unit = maneken;
        unit.transform.position = position;
        unit.transform.rotation = rotation;
        //StartCoroutine(ManekenRespawn(unit, 3f));
        Instantiate(unit);
    }

    public void UnitRespawn(GameObject unit) {
        GameObject unitClone = new GameObject("ManekenClone");
        unitClone = unit;
        StartCoroutine(ManekenRespawn(unitClone, 3f));
    }


    public IEnumerator ManekenRespawn(GameObject unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        //Instantiate(unit);
        
    }

    public void RespawnPlayerAtRandomPoint() {
        Spawner[] mc = FindObjectsOfType<Spawner>();
        Transform point = mc[Random.Range(0, mc.Length)].gameObject.transform;
        mc[Random.Range(0, mc.Length)].Spawn(Resources.Load("Player"));
    }
}
