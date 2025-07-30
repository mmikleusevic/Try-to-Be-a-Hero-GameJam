using System;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    [SerializeField] private GameObject parentObject;
    [SerializeField] private GameObject selectedObject;
    [SerializeField] private Vector3 pickedUpPosition;
    [SerializeField] private Quaternion pickedUpRotation;
    
    public void OutlineObject(bool isSelected)
    {
        if (!selectedObject) return;
        
        selectedObject.SetActive(isSelected);
    }

    public void PickUp(Transform playerTransform)
    {
        parentObject.transform.SetParent(playerTransform);
        parentObject.transform.localPosition = pickedUpPosition;
        parentObject.transform.localRotation = pickedUpRotation;
    }

    public void Use()
    {
        
    }
}