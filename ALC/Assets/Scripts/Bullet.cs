using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Vector3 Direction;

    private void FixedUpdate()
    {
        transform.position += Direction * Time.deltaTime * 3;
        StartCoroutine(SelfDestruction(1.5f));
    }


    private IEnumerator SelfDestruction(float value)
    {        
        yield return new WaitForSeconds(value);
        Destroy(gameObject);
    }

    private void OnCollisionStay(Collision collision)
    {
        if(!collision.gameObject.GetComponent<Bullet>())
        Destroy(gameObject);
    }
}
