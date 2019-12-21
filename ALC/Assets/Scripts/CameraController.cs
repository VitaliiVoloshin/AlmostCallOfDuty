using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    Vector3 offSet;
    private void Start()
    {
        Vector3 cameraPosition = transform.position;
        offSet = cameraPosition - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offSet;    
    }
}
