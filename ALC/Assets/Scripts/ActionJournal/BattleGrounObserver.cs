using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillList
{
    public string Killer { get; set; }
    public string Victum { get; set; }
    public string Weapon { get; set; }
}

/*public class ShotInfo
{
    public Object Killer { get; set; }
    public Object Victum { get; set; }
    public float damage { get; set; }
}*/

public class BattleGrounObserver : MonoBehaviour
{
    public Dictionary<int, KillList> killJournal = new Dictionary<int, KillList>();

    public static BattleGrounObserver instance;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Info();
        }
    }
    void Info() {

        for (int i = 0; i < killJournal.Count; i++) {
            Debug.Log(killJournal[i].Killer + " -> " +  killJournal[i].Victum);
        }
    }

    public void AddKill(KillList record) {
            killJournal.Add(killJournal.Count, record); 
    }

}
