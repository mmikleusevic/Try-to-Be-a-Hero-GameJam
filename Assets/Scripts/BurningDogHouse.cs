using System.Collections;
using TMPro;
using UnityEngine;

public class BurningDogHouse : MonoBehaviour
{
    [SerializeField] private TMP_Text dogText;
    [TextArea][SerializeField] private string fullText = "Something's wrong. Go check the dog house.";
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private float startDelay = 5f;
    [SerializeField] private float hideText = 5f;

    private void Start()
    {
        dogText.text = "";
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        yield return new WaitForSeconds(startDelay);
        foreach (char letter in fullText)
        {
            dogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(hideText);
        dogText.gameObject.SetActive(false);
    }
}