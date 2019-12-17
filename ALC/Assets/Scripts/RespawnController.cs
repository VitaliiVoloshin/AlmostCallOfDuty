using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectClone {
    public string name { get; set; }
    public Vector3 position { get; set; }
    public Quaternion rotation { get; set; }
}


public class RespawnController : MonoBehaviour
{
    private PlayerController player;
    private UnitHolder m_unitHolder;


    public Dictionary<int,GameObjectClone> manekens = new Dictionary<int, GameObjectClone>();


    void Start()
    {
        m_unitHolder = FindObjectOfType<UnitHolder>();
        FillAbstractDict();
    }

    void FillAbstractDict() {
        for (int i = 0; i < FindObjectOfType<UnitHolder>().manekens.Count; i++)
        {
            manekens.Add(i, ManekenAbstractClone(i));
        }
    }

    void Update()
    {
        UISwitcher();
        UnitHolderScan();
        
    }

    void UnitHolderScan() {
        int i = 0;
        while (i < m_unitHolder.manekens.Count)
        {
            if (m_unitHolder.manekens[i] == null)
            {
                RespawnAtIndex(i);
            }
            i++;
        }
    }


    GameObjectClone ManekenAbstractClone(int index) {
        return new GameObjectClone
        {
            name = FindObjectOfType<UnitHolder>().manekens[index].name,
            position = FindObjectOfType<UnitHolder>().manekens[index].transform.position,
            rotation = FindObjectOfType<UnitHolder>().manekens[index].transform.rotation
        };
    }

    public void RespawnPlayerAtRandomPoint() {
        Spawner[] mc = FindObjectsOfType<Spawner>();
        Transform point = mc[Random.Range(0, mc.Length)].gameObject.transform;
        mc[Random.Range(0, mc.Length)].Spawn(Resources.Load("Player"));
    }



    void RespawnAtIndex(int index) {
                GameObject temp = new GameObject("temp");
                m_unitHolder.manekens[index] = temp;
                temp.transform.parent = m_unitHolder.transform;
                StartCoroutine(ManekenResurectionProcessStart(index, 5f));
    }

    IEnumerator ManekenResurectionProcessStart(int index, float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject tempunit = Instantiate(Resources.Load(manekens[index].name), manekens[index].position, manekens[index].rotation) as GameObject;
        tempunit.name = manekens[index].name;
        tempunit.transform.parent = m_unitHolder.transform;
        Destroy(m_unitHolder.manekens[index].gameObject);
        m_unitHolder.manekens[index] = tempunit;
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
