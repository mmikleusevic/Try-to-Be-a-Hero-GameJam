using System;
using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, IUseable
{
    [SerializeField] private Quaternion endRotation;
    [SerializeField] private bool needsKey;
    [SerializeField] private float duration;
    
    private Quaternion startRotation;
    private readonly string useText = "Press E to open the door";
    private bool playerHasKey;
    private bool isOpen;

    private void Start()
    {
        startRotation = transform.rotation;
        endRotation *= startRotation;
    }

    public void Use(MouseLook mouseLook)
    {
        if (isOpen || !playerHasKey && needsKey) return;

        if (needsKey && mouseLook)
        {
            mouseLook.DestroySelectedObject();
        }
        
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
        if (other.gameObject.TryGetComponent(out PlayerMovement playerMovement) && !isOpen || playerHasKey && needsKey && !isOpen)
        {
            if (!playerMovement) return;
            playerMovement.SetUseableObject(this, useText);
        }
        if (other.gameObject.TryGetComponent(out MouseLook mouseLook) && !isOpen && needsKey)
        {
            playerHasKey = mouseLook.DoesPlayerHaveTheKey();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMovement playerMovement) && !isOpen || playerHasKey && needsKey && !isOpen)
        {
            if (!playerMovement) return;
            playerMovement.SetUseableObject(this, useText);
        }
        if (other.gameObject.TryGetComponent(out MouseLook mouseLook) && !isOpen)
        {
            playerHasKey = mouseLook.DoesPlayerHaveTheKey();
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