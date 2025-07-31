using System;
using UnityEngine;

public class Brother : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        if (currentHealth == 0)
        {
            //TODO trigger something death related
        }
    }
}
