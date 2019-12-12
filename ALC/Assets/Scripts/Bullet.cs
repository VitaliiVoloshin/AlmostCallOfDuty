using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Vector3 Direction;

    private void Update()
    {
        transform.position += Direction * Time.deltaTime * 3;
        StartCoroutine(SelfDestruction(1.5f));
    }


    private IEnumerator SelfDestruction(float value)
    {        
        yield return new WaitForSeconds(value);
        Destroy(gameObject);
    }
}
