using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public TextMeshPro text;


    public void SetText(int damage)
    {
        text.SetText(damage.ToString());
        text.ForceMeshUpdate();
    }
}
