﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillList
{
    public string Killer { get; set; }
    public string Victum { get; set; }
}



public class BattleGrounObserver : MonoBehaviour
{
    public Dictionary<int, KillList> killJournal = new Dictionary<int, KillList>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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