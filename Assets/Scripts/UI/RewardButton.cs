using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class RewardButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public EventPublisher<PowerUp> onSelected = new EventPublisher<PowerUp>();
    public EventPublisher<PowerUp> onHovered = new EventPublisher<PowerUp>();
    private PowerUp _powerUp;
    public Image icon;
    public TextMeshProUGUI nameText;

    public void SetPowerup(PowerUp powerUp)
    {
        _powerUp = powerUp;
        icon.sprite = powerUp.icon;
        nameText.SetText(powerUp.powerupName);
        nameText.ForceMeshUpdate();
    }

    public void OnPointerClick(PointerEventData pointerEvent)
    {
        onSelected.Invoke(_powerUp);
    }

    public void OnPointerEnter(PointerEventData pointerEvent)
    {
        onHovered.Invoke(_powerUp);
    }

}
