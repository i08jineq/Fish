using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] FadeLayer fadeLayer;

    [SerializeField] Animator animatorComponent;
    [SerializeField] GameObject[] buttonUI;
    [SerializeField] SEController seController;


    [SerializeField] GameObject seaEffectObj;

    private void Awake()
    {
        fadeLayer.ForceColor(Color.black);
        seaEffectObj.SetActive(false);
    }
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return fadeLayer.FadeInEnumerator(1);
    }


    public void OnClickStartButton()
    {
        animatorComponent.SetBool("start", true);
        StartCoroutine(ChangeSceneEnumerator());
    }

    private IEnumerator ChangeSceneEnumerator()
    {
        for (int i = 0; i < buttonUI.Length; i++) {
            buttonUI[i].SetActive(false);
        }
        yield return new WaitForSeconds(3.0f);
        seaEffectObj.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        seController.playSE(1);
        yield return new WaitForSeconds(2.5f);
        fadeLayer.gameObject.SetActive(true);
        yield return fadeLayer.FadeOutEnumerator(Color.black, 2);
        SceneManager.LoadScene("GameScene");
    }
    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
