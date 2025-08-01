using System.Collections;
using TMPro;
using UnityEngine;

public class GFireExtinguisher : MonoBehaviour
{
    [SerializeField] private TMP_Text fireText;
    [TextArea][SerializeField] private string fullText = "Find the fire extinguisher!";
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private float startDelay = 2f;
    [SerializeField] private float hideText = 3f;

    private bool isTrigger = false;

    private void Start()
    {
        fireText.text = "";
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        yield return new WaitForSeconds(startDelay);
        foreach (char letter in fullText)
        {
            fireText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(hideText);
        fireText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTrigger)
        {
            return;
        }

        if (other.name == "Player")
        {
            isTrigger = true;
        }

    }
}