using UnityEngine;
using TMPro;
using System.Collections;

public class StoryPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text storyText;
    [SerializeField] private string fullText;
    [SerializeField] private float typingSpeed = 0.05f;

    private void Start()
    {
        storyText.text = "";
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        foreach (char letter in fullText)
        {
            storyText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}