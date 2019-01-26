using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class TopPanel : MonoBehaviour
{
    [SerializeField] Animator animator;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI scoreText;
    public Image expFill;
    private const string pumpAnimationName = "Pump";

    public void UpdateHPBar(float hp)
    {
        animator.Play(pumpAnimationName);

        hpText.SetText("x"+hp.ToString());
        hpText.ForceMeshUpdate();
    }

    public void UpdateScore(int score, float percentage)
    {
        scoreText.SetText(score.ToString());
        scoreText.ForceMeshUpdate();
        expFill.fillAmount = percentage;
    }
}
