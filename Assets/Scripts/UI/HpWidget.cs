using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HpWidget : MonoBehaviour
{
    [SerializeField] Animator animator;
    public TextMeshPro text;
    private const string pumpAnimationName = "Pump";

    public void UpdateHPBar(float hp, float maxHp)
    {
        animator.Play(pumpAnimationName);

        text.SetText(hp.ToString());
        text.ForceMeshUpdate();
    }
}
