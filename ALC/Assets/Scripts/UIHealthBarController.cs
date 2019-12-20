using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIHealthBarController : MonoBehaviour
{
    private Camera m_Camera;
    Image healthBar;
    float maxHealth;
    // Start is called before the first frame update
    Quaternion rotation;
    void Start()
    {

        healthBar = transform.GetChild(1).GetComponent<Image>();
        m_Camera = Camera.main;
        if (GetComponentInParent<PlayerController>()) {
            maxHealth = GetComponentInParent<PlayerController>().stats.health;
        }

        if (GetComponentInParent<ManekenController>()) {
            maxHealth = GetComponentInParent<ManekenController>().stats.health;
        }
    }

    private void Awake()
    {
      rotation = transform.rotation;
    }

    // Update is called once per frame

    private void Update()
    {
        if (GetComponentInParent<PlayerController>())
        {
            healthBar.fillAmount = GetComponentInParent<PlayerController>().stats.health/maxHealth;
        }

        if (GetComponentInParent<ManekenController>())
        {
            healthBar.fillAmount = GetComponentInParent<ManekenController>().stats.health/maxHealth;
        }
    }
    void LateUpdate()
    {
        transform.rotation = rotation;
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward, m_Camera.transform.rotation * Vector3.up);
    }
}
