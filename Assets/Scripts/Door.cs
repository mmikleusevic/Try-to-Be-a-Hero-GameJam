using System;
using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, IUseable
{
    [SerializeField] private Quaternion endRotation;
    [SerializeField] private float duration;
    
    private readonly string useText = "Press E to open the door";
    private Quaternion startRotation;
    private bool isOpen;

    private void Start()
    {
        startRotation = transform.rotation;
        endRotation *= startRotation;
    }

    public void Use()
    {
        if (isOpen) return;
        
        StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        float timer = 0;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float elapsed = Mathf.Clamp01(timer / duration);
            
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsed);
            
            yield return null;
        }
        transform.rotation = endRotation;
        isOpen = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMovement playerMovement) && !isOpen)
        {
            playerMovement.SetUseableObject(this, useText);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMovement playerMovement) && !isOpen)
        {
            playerMovement.SetUseableObject(this, useText);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMovement playerMovement))
        {
            playerMovement.SetUseableObject(null, string.Empty);
        }
    }
}
