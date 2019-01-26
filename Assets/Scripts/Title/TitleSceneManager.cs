using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] FadeLayer fadeLayer;
    private void Awake()
    {
        fadeLayer.ForceColor(Color.black);
    }
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return fadeLayer.FadeInEnumerator(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStartButton()
    {
        StartCoroutine(ChangeSceneEnumerator());
    }

    private IEnumerator ChangeSceneEnumerator()
    {
        fadeLayer.gameObject.SetActive(true);
        yield return fadeLayer.FadeOutEnumerator(Color.black, 2);
        SceneManager.LoadScene("GameScene");
    }
    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
