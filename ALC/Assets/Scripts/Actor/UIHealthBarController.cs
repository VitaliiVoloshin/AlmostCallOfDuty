using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIHealthBarController : MonoBehaviour
{
    private Camera m_Camera;
    Image healthBar;
    float maxHealth;
    StatsController parentStats;
    // Start is called before the first frame update
    Quaternion rotation;
    void Start()
    {
        parentStats = GetComponentInParent<StatsController>();
        healthBar = transform.GetChild(1).GetComponent<Image>();
        m_Camera = Camera.main;
        maxHealth = parentStats.health;
    }

    private void Awake()
    {
      rotation = transform.rotation;
    }

    // Update is called once per frame

    private void Update()

    {

        if (parentStats != null)
        {
           
            if (parentStats.fraction == UnitStats.Fraction.green)
                transform.GetChild(1).GetComponent<Image>().color = Color.green;

            if (parentStats.fraction == UnitStats.Fraction.red)
                transform.GetChild(1).GetComponent<Image>().color = Color.red;

        }


        if (parentStats != null)
        {           
            healthBar.fillAmount = parentStats.health / maxHealth;    
        }
    }

    void LateUpdate()
    {
        transform.rotation = rotation;
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward, m_Camera.transform.rotation * Vector3.up);
    }
}
