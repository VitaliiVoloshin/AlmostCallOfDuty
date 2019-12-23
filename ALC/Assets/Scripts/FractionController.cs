using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FractionController : MonoBehaviour
{
    [SerializeField] private List<GameObject> greenPeace;
    private List<GameObject> redNation;

    public UnitHolder unitHolder;

    void OnEnable()
    {
        unitHolder = FindObjectOfType<UnitHolder>();
        greenPeace = unitHolder.players;
        redNation = unitHolder.manekens;
        FractionAutoSet();
    }

    void Update()
    {
        FractionAutoSet();
    }
    void FractionAutoSet() {
        foreach (GameObject unit in redNation)
        {
            if (unit.GetComponentInChildren<UIHealthBarController>())
            {
                unit.GetComponent<ManekenController>().stats.fraction = UnitStats.Fraction.red;

            }
        }
        foreach (GameObject unit in greenPeace)
        {
            /*if (unit.GetComponentInChildren<UIHealthBarController>())
            {
                //unit.GetComponent<PlayerController>().stats.fraction = UnitStats.Fraction.green;
            }*/
        }
    }

}
