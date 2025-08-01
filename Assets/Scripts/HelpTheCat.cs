using System.Collections;
using TMPro;
using UnityEngine;

public class HelpTheCat : MonoBehaviour
{
    [SerializeField] private AudioClip catMeowAudioClip;
    [SerializeField] private TMP_Text catText;
    [TextArea][SerializeField] private string fullText;
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private float startDelay = 5f;
    [SerializeField] private float hideText = 5f;

    private Coroutine coroutine;
    
    private IEnumerator Start()
    {
        catText.text = "";
        StartCoroutine(TypeText());
        
        yield return new WaitForSeconds(startDelay);
        coroutine = StartCoroutine(Meow());
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
        catText.text = "";
        catText.gameObject.SetActive(false);
    }

    private IEnumerator Meow()
    {
        AudioManager.Instance.PlaySFXMusic(catMeowAudioClip);

        yield return new WaitForSeconds(3f);

        yield return Meow();
    }
    
    public void StopMeowing()
    {
        StopCoroutine(coroutine);
    }
}