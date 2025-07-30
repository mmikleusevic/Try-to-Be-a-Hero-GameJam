using System;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float sensitivity;
    [SerializeField] private float maxDistance = 100f;
    [SerializeField] private LayerMask hitLayers;
    
    private Camera mainCamera;
    private PickUpObject selectedObject;
    private PickUpObject currentOutlinedObject;
    
    private float rotationX;
    private float rotationY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }
        
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -30, 30);
        rotationY += mouseX;
        
        playerTransform.rotation = Quaternion.Euler(rotationX * sensitivity, rotationY * sensitivity, 0);
        
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, hitLayers))
        {
            if (hit.transform.TryGetComponent(out PickUpObject pickUpObject))
            {
                if (currentOutlinedObject == pickUpObject) return;
                
                ClearOutline();
                currentOutlinedObject = pickUpObject;
                currentOutlinedObject.OutlineObject(true);
            }
        }
        else
        {
            ClearOutline();
        }
    }

    private void ClearOutline()
    {
        if (!currentOutlinedObject) return;
        
        currentOutlinedObject.OutlineObject(false);
        currentOutlinedObject = null;
    }

    private void PickUp()
    {
        if (!currentOutlinedObject) return;
        
        currentOutlinedObject.PickUp(playerTransform);
        selectedObject = currentOutlinedObject;
        ClearOutline();
    }

    private void Use()
    {
        if (!selectedObject) return;
        
        selectedObject.Use();
    }
}