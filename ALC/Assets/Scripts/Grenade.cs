using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Grenade : MonoBehaviour
{

    [SerializeField] private Transform forceTransform;
    private SpriteMask forceSpriteMask;
    float testForce;
    public LineRenderer trajectory;
    public GameObject targetArea;
    Ray cameraRay;
    RaycastHit cameraRayHit;

    private void Awake()
    {
        //forceSpriteMask = forceTransform.Find("mask").GetComponent<SpriteMask>();
        //HideForce();
    }

    private void Update()
    {
        /*forceTransform.position = transform.position;
        Vector3 dir = ThrowDirection().normalized;
        //forceTransform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
        */

        if (Input.GetKeyDown(KeyCode.G)) {
            testForce = 0;
        }

        if (Input.GetKey(KeyCode.G)) {
            testForce += MAX_FORCE * Time.deltaTime;
            if (testForce > MAX_FORCE) {
                testForce = MAX_FORCE;
            }
            CastParabola(testForce);
           
        }
        if (Input.GetKeyUp(KeyCode.G)) {
            trajectory.SetVertexCount(0);
            targetArea.SetActive(false);
            Launch(testForce);
        }

    }

    private void HideForce()
    {
        forceSpriteMask.alphaCutoff = 1;
    }



    public const float MAX_FORCE = 20f;

    public void Launch(float force)
    {

    }


    void CastParabola(float range) {
        Vector3[] point;
        point = new Vector3[(int)range+2];
        int numberOnInstances = (int)range+2;
        int i = 0;
        while (numberOnInstances > 0) {
            Vector3 parabolaPoint;
            Vector3 direction = (transform.forward*range * i) / range;            
            Vector3 worldPoint;
            parabolaPoint = Vector3.zero;
            parabolaPoint.y =  (- Mathf.Pow(i,2) + range * i)/range;
            parabolaPoint += direction;          
            worldPoint = transform.position + parabolaPoint;
            point[i] = worldPoint;
            i++;
            numberOnInstances--;            
        }
        trajectory.SetVertexCount((int)range);
        trajectory.SetPositions(point);
        trajectory.SetWidth(0.4f, 0.4f);
        targetArea.transform.position = point[point.Length -1];
        targetArea.SetActive(true);



    }
    public void ShowForce(float force)
    {
        forceSpriteMask.alphaCutoff = 1 - force / MAX_FORCE;
    }

}
