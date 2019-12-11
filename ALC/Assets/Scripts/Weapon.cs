using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Weapon : MonoBehaviour
{
    public Wtype weaponType;
    public int howManyBullets;
    public int range;
    public int spreading;

    // Start is called before the first frame update
    void Start()
    {
        if (weaponType == Wtype.shotgun) {
            howManyBullets = Random.Range(2, 8);
        }
        if (weaponType == Wtype.auto)
        {
            howManyBullets = 1;
        }
        if (weaponType == Wtype.pistol)
        {
            howManyBullets = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
