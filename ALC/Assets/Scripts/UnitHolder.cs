using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHolder : MonoBehaviour
{
    public PlayerController []players;
    public ManekenController[]manekens;
    public GameObject[] all;
    // Start is called before the first frame update
    void Start()
    { all = new GameObject[10];
       players = FindObjectsOfType<PlayerController>();
       manekens = FindObjectsOfType<ManekenController>();
       all = new GameObject[players.Length + manekens.Length];
        int i = 0;

        foreach (PlayerController unit in players) {
            all[i] = unit.gameObject;
            all[i].transform.parent = transform;
            i++;
        }

        foreach (ManekenController unit in manekens)
        {
            all[i] = unit.gameObject;
            all[i].transform.parent = transform;
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
