using System.Collections;
using TMPro;
using UnityEngine;

public class HelpTheCat : MonoBehaviour
{
    [SerializeField] private TMP_Text catText;
    [TextArea][SerializeField] private string fullText;
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private float startDelay = 5f;
    [SerializeField] private float hideText = 5f;

    private void Start()
    {
        catText.text = "";
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        yield return new WaitForSeconds(startDelay);
        foreach (char letter in fullText)
        {
            catText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(hideText);
        catText.gameObject.SetActive(false);
    }
}