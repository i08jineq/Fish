using UnityEngine;
using UnityEngine.EventSystems;

public class GameOverUI : MonoBehaviour, IPointerClickHandler
{
    public EventPublisher onClicked = new EventPublisher();
    public void OnPointerClick(PointerEventData eve)
    {
        onClicked.Invoke();
    }
}
