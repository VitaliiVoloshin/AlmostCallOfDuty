using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]



public class PlayerController : ActorController
{
    [SerializeField] private InputController input;
    private Ray m_cameraRay;
    private RaycastHit m_cameraRayHit;
    private KeyCode shootbutton;
    private string verticalAxis;
    private string horizontalAxis;
    private bool m_isGrounded;
    
    void Awake()
    {        
        SetUpControls();
    }

    void SetUpControls(){
        stats.health = 100;
        shootbutton = input.shootButton;
        verticalAxis = input.verticalAxis;
        horizontalAxis = input.horizontalAxis;
        m_isGrounded = false;
        
    }


    void Update()
    {
        transform.LookAt(RotationToCursor(transform));

        if (Input.GetAxis(verticalAxis) != 0f || Input.GetAxis(horizontalAxis) != 0f)
        {
            if (Input.GetKey(shootbutton))
            {
                Shoot();
                Movement(stats.movementSpeed * MovementSpeedDepensOnActiveWeapon());
            }
            else
            {
                Movement(stats.movementSpeed);
            }

        }
        else if (Input.GetKey(shootbutton)) {
            Shoot();
        }
        Death();
    }

    float MovementSpeedDepensOnActiveWeapon()
    {
        if (m_weaponController.activeWeapon._weaponData.Identificator == "shotgun")
        {
            return 0.75f;
            
        }
        if (m_weaponController.activeWeapon._weaponData.Identificator == "rifle")
        {

            return 0.85f;
        }
        else return 1f;
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
            Vector3 targetVelocity = new Vector3(Input.GetAxis(horizontalAxis), 0, Input.GetAxis(verticalAxis));
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
}
