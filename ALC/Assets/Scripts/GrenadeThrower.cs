using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GrenadeThrower : MonoBehaviour
{

    [SerializeField] private Transform forceTransform;
    private SpriteMask forceSpriteMask;
    float testForce;
    public LineRenderer trajectory;
    public GameObject targetArea;

    private LineRenderer m_trajectory;
    private GameObject m_targetArea;
    Ray cameraRay;
    RaycastHit cameraRayHit;

    const int ITTERATIONS = 100;
    public const float MAX_FORCE = 20f;


    bool stop;
    private void Awake()
    {
        stop = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) {
            testForce = 0;
            ShowForce();
        }

        if (Input.GetKey(KeyCode.G)) {



            testForce += MAX_FORCE * Time.deltaTime;

            if (testForce > MAX_FORCE) {
                testForce = MAX_FORCE;
            }

            CastParabola(testForce);

        }
        if (Input.GetKeyUp(KeyCode.G)) {
            Vector3[] traj = new Vector3[CastParabola(testForce).Length];
            traj = CastParabola(testForce);
            Launch(testForce,traj);
            HideForce();
            
        }



    }

    private void HideForce()
    {
        Destroy(m_trajectory.gameObject);
        Destroy(m_targetArea);
    }





    void Launch(float force, Vector3[] trajectory)
    {
        GameObject grenade = Instantiate(Resources.Load("Grenade"), transform.position, transform.rotation) as GameObject;
        grenade.GetComponent<Grenade>().trajectory = trajectory;
        grenade.GetComponent<Grenade>().owner = GetComponentInParent<PlayerController>();
    }

    Vector3[] CastParabola(float range) {
        Vector3[] point;

        point = new Vector3[ITTERATIONS];
        int numberOnInstances = ITTERATIONS;
        int instanceCount = numberOnInstances;
        float i = 0;
        while (instanceCount > 0)
        {
            Vector3 parabolaPoint;
            Vector3 direction = transform.position + transform.forward * i;
            direction.y = GameObject.FindGameObjectWithTag("Ground").transform.position.y;
            Vector3 worldPoint;
            parabolaPoint = Vector3.zero;
            parabolaPoint.y = (-Mathf.Pow(i, 2) + range * i) / range;
            parabolaPoint += direction;
            worldPoint = parabolaPoint;
            point[numberOnInstances - instanceCount] = worldPoint;
            i+=range/numberOnInstances;
            instanceCount--;
        }
        RaycastHit hit;
        if (Physics.Raycast(m_targetArea.transform.position, m_targetArea.transform.forward, out hit, 1f))
        {
            Debug.DrawRay(m_targetArea.transform.position, m_targetArea.transform.forward * 1, Color.red);
            stop = true;
        }
        else {
            stop = false;
        }

        m_trajectory.SetVertexCount(ITTERATIONS);
        m_trajectory.SetPositions(point);
        m_trajectory.SetWidth(0.4f, 0.4f);


        if (point.Length == ITTERATIONS) {
            m_targetArea.transform.position = point[point.Length - 1];
            Debug.DrawRay(m_targetArea.transform.position, m_targetArea.transform.forward * 5);

            /*RaycastHit hit;
            if (Physics.Raycast(m_targetArea.transform.position, m_targetArea.transform.forward, out hit, 5f))
            {
                Debug.DrawRay(m_targetArea.transform.position, m_targetArea.transform.forward * 5, Color.red);  
            }*/
        }



        return point;
    }


    public void ShowForce()
    {
        m_trajectory = Instantiate(trajectory);
        m_targetArea = Instantiate(targetArea);
    }

}
