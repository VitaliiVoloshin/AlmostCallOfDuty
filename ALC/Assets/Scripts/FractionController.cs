using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FractionController : MonoBehaviour
{
    [SerializeField] private List<GameObject> greenPeace;
    private List<GameObject> redNation;

    public UnitHolder unitHolder;
    void Start()
    {
        unitHolder = FindObjectOfType<UnitHolder>();
        greenPeace = unitHolder.players;
        redNation = unitHolder.manekens;
        StartCoroutine(LateStart(0.1f));

    }
    IEnumerator LateStart(float delay) {
        yield return new WaitForSeconds(delay);

        foreach (GameObject unit in greenPeace)
        {
            if (unit.GetComponentInChildren<UIHealthBarController>())
            {
                unit.GetComponent<PlayerController>().stats.fraction = UnitStats.Fraction.green;
                //GameObject hpbar = unit.GetComponentInChildren<UIHealthBarController>().gameObject;
               // hpbar.transform.GetChild(1).GetComponent<Image>().color = Color.green;
            }
        }

        foreach (GameObject unit in redNation)
        {
            if (unit.GetComponentInChildren<UIHealthBarController>()) { 
            GameObject hpbar = unit.GetComponentInChildren<UIHealthBarController>().gameObject;
            hpbar.transform.GetChild(1).GetComponent<Image>().color = Color.red;
            }
        }
    }

    void Update()
    {
    }

}
