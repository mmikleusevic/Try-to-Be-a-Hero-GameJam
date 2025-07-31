using System;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private const string PUNCH = "Punch";
    
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float sensitivity;
    [SerializeField] private float maxDistance = 100f;
    [SerializeField] private LayerMask hitLayers;
    [SerializeField] private int changeStep;
    [SerializeField] private Brother brother;
    [SerializeField] private float damage;
    
    private Camera mainCamera;
    private PickUpObject objectReadyForPickup;
    private PickUpObject currentlySelectedObject;
    private readonly List<PickUpObject> pickedUpObjects = new List<PickUpObject>();
    
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

        if (Input.mouseScrollDelta.y > 0)
        {
            ChangeSelectedObject(changeStep);
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            ChangeSelectedObject(-changeStep);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Punch();
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
                if (objectReadyForPickup == pickUpObject) return;
                
                ClearOutline();
                
                objectReadyForPickup = pickUpObject;
                objectReadyForPickup.OutlineObject(true);
            }
        }
        else
        {
            ClearOutline();
        }
    }

    private void ClearOutline()
    {
        if (!objectReadyForPickup) return;
        
        objectReadyForPickup.OutlineObject(false);
        objectReadyForPickup = null;
    }

    private void PickUp()
    {
        if (!objectReadyForPickup) return;

        objectReadyForPickup.PickUp(playerTransform);

        if (currentlySelectedObject) currentlySelectedObject.gameObject.SetActive(false);
        
        currentlySelectedObject = objectReadyForPickup;
        pickedUpObjects.Add(objectReadyForPickup);
        ClearOutline();
    }

    private void ChangeSelectedObject(int step)
    {
        if (pickedUpObjects.Count <= 0 || !currentlySelectedObject) return;
        
        int currentIndex = pickedUpObjects.IndexOf(currentlySelectedObject);
        int length = pickedUpObjects.Count;
        int index = (currentIndex + step  + length) % length;

        if (currentlySelectedObject) currentlySelectedObject.gameObject.SetActive(false);
        
        currentlySelectedObject = pickedUpObjects[index];

        if (currentlySelectedObject) currentlySelectedObject.gameObject.SetActive(true);
    }

    private void Punch()
    {
        playerAnimator.Play(PUNCH, -1, 0f);

        if (!brother) return;
        
        float distance = Vector3.Distance(transform.position, brother.transform.position);
        if (distance < 3)
        {
            brother.TakeDamage(damage);
        }
    }
    
    public bool CheckForKey()
    {
        if (currentlySelectedObject.name == "Key") return true;
        
        return false;
    }
}