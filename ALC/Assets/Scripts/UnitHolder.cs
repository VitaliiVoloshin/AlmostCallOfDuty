using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHolder : MonoBehaviour
{
    public List<PlayerController> players;
    public List<ManekenController> manekens;

    private Vector3[] manekensPos;
    private Quaternion[] manekensRot;
    private bool m_manekensClone;
    // Start is called before the first frame update
    void Start()
    { 
        players.AddRange(FindObjectsOfType<PlayerController>());
        manekens.AddRange(FindObjectsOfType<ManekenController>());
        GetShit();
        manekensPos = new Vector3[manekens.Count];
        manekensRot = new Quaternion[manekens.Count];

        int i = 0;
        foreach (ManekenController boi in manekens) {
            if (boi != null) {
            manekensPos[i] = boi.transform.position;
            manekensRot[i] = boi.transform.rotation;
            
            }
            i++;
        }
    }

    private void Update()
    {
        int i = 0;
        /*foreach (ManekenController unit in manekens) {
            if (unit == null) {
                Debug.Log(manekensPos[i] + " + " + manekensRot[i]);
                GameObject tempunit = Instantiate(Resources.Load("AutoRifleManeken"),manekensPos[i],manekensRot[i]) as GameObject;
                tempunit.transform.parent = transform;
                manekens[i] = tempunit.GetComponent<ManekenController>();
            }
            i++;
        }*/

        while (i <= manekens.Count-1) {
            if (manekens[i] == null)
            {
                Debug.Log(manekensPos[i] + " + " + manekensRot[i]);
                GameObject tempunit = Instantiate(Resources.Load("AutoRifleManeken"), manekensPos[i], manekensRot[i]) as GameObject;
                tempunit.transform.parent = transform;
                manekens[i] = tempunit.GetComponent<ManekenController>();
            }
            i++;
        }
    }


    void GetShit() {
        foreach (PlayerController unit in players)
        {
            unit.transform.parent = transform;
        }
        foreach (ManekenController unit in manekens)
        {
            unit.transform.parent = transform;
        }
    }

}
