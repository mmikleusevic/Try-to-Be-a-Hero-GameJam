using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderToText : MonoBehaviour
{
  [SerializeField] private TMP_Text text;
  [SerializeField] private Slider slider;
  private void Start()
  {
    slider.maxValue = 100;
    slider.wholeNumbers = true;
    text.text = $"Slider value is: \n{slider.value}";
  }
  private void Update()
  {
    //text.text = slider.value.ToString();
    text.text = $"Volume: \n{slider.value}";
  }
}
