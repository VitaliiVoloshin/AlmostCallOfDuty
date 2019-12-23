using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]



public class PlayerController : MonoBehaviour
{
    public StatsController stats;
    public float gravity;
    public float maxVelocityChange;
    public bool canJump;
    public float jumpHeight;
    private bool grounded;


    public Camera camera;


    Ray cameraRay;             
    RaycastHit cameraRayHit;


    public int damageDone;

    void Awake()
    {
        
        stats = GetComponent<StatsController>();
        SetUp();   
    }


    void SetUp() {
        gravity = 20.0f;
        maxVelocityChange = 10.0f;
        canJump = false;
        jumpHeight = 2.0f;
        grounded = false;
        stats.attackSpeed = 2f;
        stats.damageCaused = 1.2f;
        GetComponent<Rigidbody>().freezeRotation = true;
        GetComponent<Rigidbody>().useGravity = false;
    }


    void Update()
    {
        transform.LookAt(RotationToCursor(transform));
        if (FindObjectOfType<CameraController>().player == null) {
            FindObjectOfType<CameraController>().player = gameObject;
        }

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
        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(cameraRay, out cameraRayHit))
        {
                Vector3 targetPosition = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
                return targetPosition;
        }
        return Vector3.zero;

    }

    void Movement(float speed) {
        if (grounded)
        {
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            targetVelocity *= speed*10;
            Vector3 velocity = GetComponent<Rigidbody>().velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);

        }
        GetComponent<Rigidbody>().AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0));
        grounded = false;

    }

    void OnCollisionStay()
    {
        grounded = true;
    }

    float CalculateJumpVerticalSpeed()
    {
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    public void TakeDamage(float damage) {
        stats.health -= damage*stats.damageTaken;
    }
}
