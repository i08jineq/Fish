using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RewardUI : MonoBehaviour
{
    public List<RewardButton> buttons = new List<RewardButton>();
    public TextMeshProUGUI descriptionText;
    public EventPublisher<PowerUp> onSelected = new EventPublisher<PowerUp>();

    public void Init()
    {
        foreach (var item in buttons)
        {
            item.onHovered.AddListener(OnHovered);
            item.onSelected.AddListener(onSelected.Invoke);
        }
    }

    private void OnHovered(PowerUp powerUp)
    {
        descriptionText.SetText(powerUp.description);
        descriptionText.ForceMeshUpdate();
    }

    public void SetRewards(PowerUp[] powerUps)
    {
        int count = buttons.Count;
        for (int i = 0; i < count; i++)
        {
            buttons[i].SetPowerup( powerUps[i]);
        }
    }
}
