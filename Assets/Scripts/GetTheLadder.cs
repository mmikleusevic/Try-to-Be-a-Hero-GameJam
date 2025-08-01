using System.Collections;
using TMPro;
using UnityEngine;

public class GetTheLadder : MonoBehaviour
{
    [SerializeField] private TMP_Text ladderText;
    [TextArea][SerializeField] private string fullText = "Find the ladder";
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private float startDelay = 2f;
    [SerializeField] private float hideText = 3f;

    private bool isStarted;
    
    private void Start()
    {
        ladderText.text = "";
    }

    private IEnumerator TypeText()
    {
        ladderText.gameObject.SetActive(true);
        isStarted = true;
        yield return new WaitForSeconds(startDelay);
        foreach (char letter in fullText)
        {
            ladderText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(hideText);
        ladderText.text = "";
        ladderText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isStarted) return;
        StartCoroutine(TypeText());
    }
}