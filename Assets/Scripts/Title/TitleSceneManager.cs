using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
        yield return FadeManager.Instance.FadeOut(2);
        SceneManager.LoadScene("GameScene");
    }
    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
