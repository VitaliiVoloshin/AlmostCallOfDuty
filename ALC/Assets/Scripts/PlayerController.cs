using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class PlayerController : MonoBehaviour
{ 
    public float speed;
    public float gravity;
    public float maxVelocityChange;
    public bool canJump;
    public float jumpHeight;
    private bool grounded;
    public int hp;
    Ray cameraRay;             
    RaycastHit cameraRayHit;
    Weapon activeWeapon;

    public int damageDone;

    void Awake()
    {
        SetUp();   
    }


    void SetUp() {

        speed = 10.0f;
        gravity = 20.0f;
        maxVelocityChange = 10.0f;
        canJump = false;
        jumpHeight = 2.0f;
        grounded = false;
        hp = 100;

        GetComponent<Rigidbody>().freezeRotation = true;
        GetComponent<Rigidbody>().useGravity = false;
        activeWeapon = GetComponentInChildren<WeaponController>().activeWeapon;

    }

    void Update()
    {
        RotationToCursor();


        if (Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
        }

        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical")!=0f) {
            Movement();
        }
        


        if (hp <= 0) {
            Destroy(gameObject);        
        }

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

    void Movement() {
        if (grounded)
        {
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            targetVelocity *= speed;
            Vector3 velocity = GetComponent<Rigidbody>().velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);

            if (canJump && Input.GetButton("Jump"))
            {
                GetComponent<Rigidbody>().velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
            }
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
        hp -= damage;
    }
}
