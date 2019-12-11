using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLogic : MonoBehaviour
{
    public Vector3 shootLenght;
    public Vector3 shootLeftSide;
    public Vector3 shootRightSide;
    public int range;
    public int spreading;
    private Weapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponentInParent<Weapon>();
        range = weapon.range;
        spreading = weapon.spreading;

    }

    // Update is called once per frame
    void Update()
    {
        shootLenght = transform.forward * range;
        //
        shootRightSide = transform.forward * range;

        Debug.DrawRay(transform.position, shootLenght , Color.green);
        Debug.DrawRay(transform.position, shootLeftSide, Color.red) ;
        Debug.DrawRay(transform.position, shootRightSide, Color.blue);
    }
}
