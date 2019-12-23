using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]



public class PlayerController : Actor
{
    private Ray m_cameraRay;
    private RaycastHit m_cameraRayHit;
    private bool m_isGrounded;
    
    void Awake()
    {        
        stats = GetComponent<StatsController>();
        SetUp();   
    }

    void SetUp() {

        m_isGrounded = false;
        stats.attackSpeed = 2f;
        stats.damageCaused = 1.2f;
    }


    void Update()
    {
        transform.LookAt(RotationToCursor(transform));
        /*if (FindObjectOfType<CameraController>().player == null) {
            FindObjectOfType<CameraController>().player = gameObject;//////
        }*/

        if (Input.GetKey(KeyCode.Mouse0))
        {
           
            Shoot();
            if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
            {
                Movement(stats.movementSpeed * MovementSpeedDepensOnActiveWeapon());
            }
        }
        else {
            if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
            {
                Movement(stats.movementSpeed);
            }
        }
        
        if (stats.health <= 0)
        {
            Destroy(gameObject);
        }

    }

    float MovementSpeedDepensOnActiveWeapon()
    {
        if (GetComponentInChildren<WeaponController>().activeWeapon._weaponData.Identificator == "shotgun")
        {
            return 0.75f;
            
        }
        if (GetComponentInChildren<WeaponController>().activeWeapon._weaponData.Identificator == "rifle")
        {

            return 0.85f;
        }
        else return 1f;
    }

    void Shoot() {
        Weapon active = GetComponentInChildren<WeaponController>().activeWeapon;
        active.Shoot();
    }

    public Vector3 RotationToCursor(Transform position) {
        m_cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(m_cameraRay, out m_cameraRayHit))
        {
                Vector3 targetPosition = new Vector3(m_cameraRayHit.point.x, transform.position.y, m_cameraRayHit.point.z);
                return targetPosition;
        }
        return Vector3.zero;

    }

    void Movement(float speed) {
        if (m_isGrounded)
        {
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            targetVelocity *= speed*10;
            Vector3 velocity = GetComponent<Rigidbody>().velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -10, 10);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -10, 10);
            velocityChange.y = 0;
            GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);

        }
        GetComponent<Rigidbody>().AddForce(new Vector3(0, -20 * GetComponent<Rigidbody>().mass, 0));
        m_isGrounded = false;

    }

    void OnCollisionStay()
    {
        m_isGrounded = true;
    }


    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    public void TakeDamage(float damage) {
        stats.health -= damage*stats.damageTaken;
    }
}
