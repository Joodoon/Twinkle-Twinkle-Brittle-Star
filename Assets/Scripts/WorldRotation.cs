using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;

public class WorldRotation : MonoBehaviour
{
    [SerializeField] GameObject world;
    [SerializeField] BoxCollider2D groundCheck;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject camTarget;

    private bool isGrounded = false;

    private float dist;
    [SerializeField] float rotSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        dist = Vector3.Distance(world.transform.position, transform.position);
        rotSpeed = .5f / dist;

        rb.gravityScale = 2.5f / (dist / 10);


        // Ground checking
        isGrounded = groundCheck.IsTouchingLayers(LayerMask.GetMask("Ground"));

        if(Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.AddForce(Vector2.up * 12, ForceMode2D.Impulse);
        }

        camTarget.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        // Lateral Movement
        if(Input.GetKey(KeyCode.A))
        {
            world.transform.Rotate(Vector3.forward, -rotSpeed);
            camTarget.transform.position = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z);
        }
        if(Input.GetKey(KeyCode.D))
        {
            world.transform.Rotate(Vector3.forward, rotSpeed);
            camTarget.transform.position = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
        }
        
        
    }
}
