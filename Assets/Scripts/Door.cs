using System;
using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, IUseable
{
    [SerializeField] private Quaternion endRotation;

    [SerializeField] private string useText;
    [SerializeField] private float duration;
    
    private Quaternion startRotation;
    private bool isOpen;

    private void Start()
    {
        startRotation = transform.rotation;
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
            float elapsed =  timer / duration;
            
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsed);
            
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

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMovement playerMovement))
        {
            playerMovement.SetUseableObject(null, string.Empty);
        }
    }
}
