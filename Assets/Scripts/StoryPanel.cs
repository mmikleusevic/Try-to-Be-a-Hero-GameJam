using System;
using UnityEngine;
using TMPro;
using System.Collections;

public class StoryPanel : MonoBehaviour
{
    public Action DisableMainMenu;
    
    [SerializeField] private TMP_Text storyText;
    [SerializeField] private string fullText;
    [SerializeField] private float typingSpeed = 0.05f;

    private void Start()
    {
        storyText.text = "";
    }

    public IEnumerator TypeText()
    {
        foreach (char letter in fullText)
        {
            storyText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        
        DisableMainMenu?.Invoke();
        
        yield return new WaitForSeconds(3f);
        
        LevelManager.Instance.LoadScene(Scenes.Level1);
    }
}