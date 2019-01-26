using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarController : MonoBehaviour
{

    public Slider slider;

   // public Canvas hpCanvas;
   
    // Update is called once per frame
    public void UpdateHPBar(float hp, float maxHp)
    {
        slider.maxValue =maxHp;
        slider.value = hp;
    }
}
