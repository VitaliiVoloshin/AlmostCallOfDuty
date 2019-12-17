using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class UnitStats {

    private float reset = 1f;



    private float m_movementSpeed = 1f;
    private float m_attackSpeed = 1f;
    private float m_damageCaused = 1f;
    private float m_damageTaken = 1f;

    public float health { get; set; } = 100;

    public float movementSpeed {
        get { return m_movementSpeed; }
        set { m_movementSpeed *= value; }
    }
    public float attackSpeed
    {
        get { return m_attackSpeed; }
        set { m_attackSpeed *= value; }
    }
    public float damageCaused
    {
        get { return m_damageCaused; }
        set { m_damageCaused *= value; }
    }
    public float damageTaken
    {
        get { return m_damageTaken; }
        set { m_damageTaken *= value; }
    }

    public void ResetToDefault(float param) {
        param = reset;
    }
}

public class PlayerController : MonoBehaviour
{
    public UnitStats stats = new UnitStats();
    public float gravity;
    public float maxVelocityChange;
    public bool canJump;
    public float jumpHeight;
    private bool grounded;
    Ray cameraRay;             
    RaycastHit cameraRayHit;

    public int damageDone;

    void Awake()
    {
        stats.ResetToDefault(stats.movementSpeed);
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
        RotationToCursor();


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
        damageDone += active.Shoot();
    }

    void RotationToCursor() {
        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(cameraRay, out cameraRayHit))
        {
            if (cameraRayHit.transform.tag == "Ground")
            {
                Vector3 targetPosition = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
                transform.LookAt(targetPosition);
            }
        }
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

            /*if (canJump && Input.GetButton("Jump"))
            {
                GetComponent<Rigidbody>().velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
            }*/
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

    public void TakeDamage(int damage) {
        stats.health -= damage*stats.damageTaken;
    }
}
