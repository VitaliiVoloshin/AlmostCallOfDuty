using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UnitHolder : MonoBehaviour
{
    public List<GameObject> players;
    public List<GameObject> manekens;

    


    public List<GameObject> m_manekensClone;


    void Start()
    {
        AddAllManekens(manekens);
        AddAllPlayers(players);
        RestructCurrent();
    }

    void RestructCurrent() {
        foreach (GameObject player in players)
        {
            player.transform.parent = transform;
        }

        foreach (GameObject unit in manekens)
        {
            unit.transform.parent = transform;
        }
    }


    void AddAllManekens(List<GameObject> unit) {
        ManekenController[] whatever = FindObjectsOfType(typeof(ManekenController)) as ManekenController[];
        foreach (ManekenController enemy in whatever)
        {
            unit.Add(enemy.gameObject);
        }
    }

    void AddAllPlayers(List<GameObject> unit)
    {
        PlayerController[] whatever = FindObjectsOfType(typeof(PlayerController)) as PlayerController[];
        foreach (PlayerController enemy in whatever)
        {
            unit.Add(enemy.gameObject);
        }
    }
}
