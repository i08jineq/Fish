using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseScreen : MonoBehaviour
{
    [SerializeField]protected Button resumeButton;
    [SerializeField] protected Button quitButton;
    [SerializeField] protected Button backToTitleButton;

    public EventPublisher onClickedQuit = new EventPublisher();
    public EventPublisher onClickedBackTOTitle = new EventPublisher();
    public EventPublisher onClickedResume = new EventPublisher();


    public void Init()
    {
        resumeButton.onClick.AddListener(onClickedResume.Invoke);
        quitButton.onClick.AddListener(onClickedQuit.Invoke);
        backToTitleButton.onClick.AddListener(onClickedBackTOTitle.Invoke);

    }

    private void OnClickedButton()
    {
        //play sound
    }
}
