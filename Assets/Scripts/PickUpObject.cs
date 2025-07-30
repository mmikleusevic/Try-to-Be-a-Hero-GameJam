using System;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{ 
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
        transform.SetParent(playerTransform);
        transform.localPosition = pickedUpPosition;
        transform.localRotation = pickedUpRotation;
    }
}