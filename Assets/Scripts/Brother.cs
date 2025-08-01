using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Brother : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject brother;
    [SerializeField] private float maxHealth;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject zoomOnPlayer;
    [SerializeField] private Vector3 offset;
    
    private Camera mainCamera;
    private float currentHealth;

    private void Start()
    {
        mainCamera = Camera.main;
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        healthBar.fillAmount = currentHealth / maxHealth;

        if (currentHealth == 0)
        {
            ZoomToEnd();
        }
    }
    
    private void ZoomToEnd()
    {
        Cursor.lockState = CursorLockMode.None;
        LevelManager.Instance.LoadScene(Scenes.End);
    }
}