using System;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 jumpForce;
    [SerializeField] private TextMeshProUGUI useText;
    
    private Rigidbody playerRigidbody;
    public Transform groundCheckOrigin;
    
    private Vector3 direction;
    public LayerMask groundLayer;
    private IUseable currentUseableObject;
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            Use();
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
            groundCheckOrigin.position + new Vector3(0, 0.75f, 0),
            sphereRadius,
            Vector3.down,
            out RaycastHit hit,
            checkDistance,
            groundLayer,
            QueryTriggerInteraction.Ignore
        );
    }
    
    public void SetUseableObject(IUseable useableObject, string text)
    {
        ToggleCanvas(useableObject != null, text);
        currentUseableObject = useableObject;
    }

    private void ToggleCanvas(bool value, string text)
    {
        useText.gameObject.SetActive(value);
        useText.text = text;
    }

    private void Use()
    {   
        currentUseableObject?.Use();
        ToggleCanvas(false, string.Empty);
    }
}