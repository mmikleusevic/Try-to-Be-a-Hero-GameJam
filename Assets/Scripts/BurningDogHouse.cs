using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class BurningDogHouse : MonoBehaviour
{
    [SerializeField] private TMP_Text dogText;
    [TextArea][SerializeField] private string fullText = "The dog house is burning, find the shed key to get the fire extinguisher";
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private float hideText = 5f;
    [SerializeField] private AudioClip cracklingSound;
    
    private bool hasPlayed;
    
    private void Start()
    {
        dogText.text = "";
        StartCoroutine(PlayCracklingSound());
    }

    private IEnumerator TypeText()
    {
        hasPlayed = true;
        dogText.gameObject.SetActive(true);
        foreach (char letter in fullText)
        {
            dogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(hideText);
        dogText.gameObject.SetActive(false);
        
        enabled = false;
    }

    private IEnumerator PlayCracklingSound()
    {
        AudioManager.Instance.PlaySFXMusic(cracklingSound);
        
        yield return new WaitForSeconds(cracklingSound.length);

        yield return PlayCracklingSound();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasPlayed) return;
        StartCoroutine(TypeText());
    }
}