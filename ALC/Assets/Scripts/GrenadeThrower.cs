using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GrenadeThrower : MonoBehaviour
{

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
    public const float MIN_FORCE = 5f;


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
            testForce = Vector3.Distance(transform.position,FindObjectOfType<PlayerController>().RotationToCursor(transform));

            if (testForce > MAX_FORCE) {
                testForce = MAX_FORCE;
            }
            if (testForce < MIN_FORCE) {
                testForce = MIN_FORCE;
            }

            if (testForce != 0 && m_targetArea!=null)
            {
                CastParabola(testForce);
            }
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
        if (force > 0)
        {
            GameObject grenade = Instantiate(Resources.Load("Grenade"), transform.position, transform.rotation) as GameObject;
            grenade.GetComponent<Grenade>().trajectory = trajectory;
            grenade.GetComponent<Grenade>().owner = GetComponentInParent<PlayerController>();
        }
    }

    Vector3[] CastParabola(float range) {
        Vector3[] point;
        point = new Vector3[ITTERATIONS];
        point = MakeTrajectory(point, ITTERATIONS, range, true);
        return point;
    }

    Vector3[] RecastParabola(float range) {
        Vector3[] point;
        point = new Vector3[ITTERATIONS];
        point = MakeTrajectory(point, ITTERATIONS, range, false);
        return point;
    }


    Vector3[] MakeTrajectory(Vector3[] pointPositions, int nubmerOfIterrations, float lenght, bool withRaycast) {

        bool detectRaycast = false;
        Vector3 raycastPoint = Vector3.zero;

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
            parabolaPoint.y = (-Mathf.Pow(i, 2) + lenght * i) / lenght;
            parabolaPoint += direction;
            worldPoint = parabolaPoint;
            pointPositions[numberOnInstances - instanceCount] = worldPoint;
            i += lenght / numberOnInstances;
            instanceCount--;

            if (withRaycast) {
                if (!detectRaycast)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(pointPositions[numberOnInstances - instanceCount - 1], transform.forward, out hit, 6f))
                    {
                        detectRaycast = true;
                        raycastPoint = pointPositions[numberOnInstances - instanceCount - 1];
                    }
                }
            }

        }
        if (withRaycast) {
            if (detectRaycast)
            {
                float lul = Vector3.Distance(transform.position, raycastPoint);
                Vector3[] redraw = RecastParabola(lul);
                m_trajectory.SetVertexCount(ITTERATIONS);
                m_trajectory.SetPositions(redraw);
                m_trajectory.SetWidth(0.4f, 0.4f);
                m_targetArea.transform.position = redraw[redraw.Length - 1];
                return redraw;
            }
            else
            {
                m_trajectory.SetVertexCount(ITTERATIONS);
                m_trajectory.SetPositions(pointPositions);
                m_trajectory.SetWidth(0.4f, 0.4f);
                m_targetArea.transform.position = pointPositions[pointPositions.Length - 1];
                return pointPositions;

            }
        }
        else
        {
            m_trajectory.SetVertexCount(ITTERATIONS);
            m_trajectory.SetPositions(pointPositions);
            m_trajectory.SetWidth(0.4f, 0.4f);

            if (pointPositions.Length == ITTERATIONS)
            {
                m_targetArea.transform.position = pointPositions[pointPositions.Length - 1];
            }
            return pointPositions;
        }

    }



    public void ShowForce()
    {
            m_trajectory = Instantiate(trajectory);
            m_targetArea = Instantiate(targetArea);
    }

}
