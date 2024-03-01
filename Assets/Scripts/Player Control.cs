using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] BoxCollider2D groundCheck;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject camTarget;
    [SerializeField] GameObject playerVCam;
    [SerializeField] GravityManager gravity;

    [SerializeField] bool isGrounded = false;

    private Vector2 perp;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        gravity.Objects.Add(gameObject);
    }

    void FixedUpdate()
    {
        // Ground checking
        isGrounded = groundCheck.IsTouchingLayers(LayerMask.GetMask("Ground"));

        // Rotate player to always face the center of the world
        transform.up = transform.position.normalized;
        playerVCam.transform.up = transform.position.normalized;
        perp = Vector2.Perpendicular(transform.position.normalized);

        if(isGrounded){
            // Raycast downwards
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.position, 5f, LayerMask.GetMask("Ground"));
            perp = Vector2.Perpendicular(hit.normal);
        }

        // Lateral Movement
        HandleLateralMovement();

        // Get the horizontal and vertical components of the velocity
        Vector2 horizontalVelocity = Vector2.Dot(rb.velocity, perp) * perp;
        Vector2 verticalVelocity = Vector2.Dot(rb.velocity, transform.up) * transform.up;

        // Clamp the horizontal speed
        float maxHorizontalSpeed = 7.5f;
        if (horizontalVelocity.magnitude > maxHorizontalSpeed)
        {
            horizontalVelocity = horizontalVelocity.normalized * maxHorizontalSpeed;
        }

        // Set the new velocity
        rb.velocity = horizontalVelocity + verticalVelocity;
    }

    void Update(){
        HandleJumping();
    }

    void HandleJumping()
    {
        if(isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(transform.up.normalized * 35, ForceMode2D.Impulse);
        }
    }

    void HandleLateralMovement()
    {
        if(Input.GetKey(KeyCode.A))
        {
            rb.AddForce(perp * 100, ForceMode2D.Force);
        }
        if(Input.GetKey(KeyCode.D))
        {
            rb.AddForce(-perp * 100, ForceMode2D.Force);
        }
    }
}
