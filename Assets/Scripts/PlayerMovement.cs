using System;
using NUnit.Framework;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 jumpForce;
    
    private Rigidbody playerRigidbody;
    public Transform groundCheckOrigin;
    
    private Vector3 direction;
    public LayerMask groundLayer;
    public float sphereRadius = 0.4f;
    public float checkDistance = 0.2f;
    
    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        direction = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W))
        {
            direction += transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction -= transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction -= transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += transform.right;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            playerRigidbody.AddForce(jumpForce, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed *= 2;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed /= 2;
        }
    }

    private void Move()
    {
        Vector3 normalizedDirection = direction.normalized;
        
        playerRigidbody.linearVelocity = 
            new Vector3(normalizedDirection.x * speed, playerRigidbody.linearVelocity.y, normalizedDirection.z * speed);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private bool IsGrounded()
    {
        return Physics.SphereCast(
            groundCheckOrigin.position,
            sphereRadius,
            Vector3.down,
            out RaycastHit hit,
            checkDistance,
            groundLayer,
            QueryTriggerInteraction.Ignore
        );
    }
}