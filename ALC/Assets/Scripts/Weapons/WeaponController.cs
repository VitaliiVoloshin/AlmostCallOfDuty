using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Wtype
{
    shotgun,
    auto,
    pistol
}

public class WeaponController : MonoBehaviour
{
    private int selectedWeapon = 0;
    public Weapon activeWeapon;
    // Start is called before the first frame update
    void OnEnable()
    {
        SelectWeapon();
        //Debug.Log(activeWeapon.owner);
    }

    // Update is called once per frame
    void Update()
    {
        int prevWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
            
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }

        if (prevWeapon != selectedWeapon) {
            SelectWeapon();
        }
    }

    void SelectWeapon() {
        int i = 0;
        foreach (Transform weapon in transform) {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                activeWeapon = weapon.GetComponent<Weapon>();
                activeWeapon.owner = GetComponentInParent<ActorController>();
            }
            else {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
