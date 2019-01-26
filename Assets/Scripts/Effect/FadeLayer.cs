using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeLayer : MonoBehaviour
{
    [SerializeField] private Image image;
    public void ForceColor(Color color)
    {
        transform.SetAsLastSibling();
        image.color = color;
    }

    public void FadeIn()
    {

    }


}
