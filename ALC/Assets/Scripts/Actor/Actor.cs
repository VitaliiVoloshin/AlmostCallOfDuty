using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public StatsController stats;

    void Death() {
        if (stats.health <= 0) {
            Debug.Log("Dead");
        }
    }
}
