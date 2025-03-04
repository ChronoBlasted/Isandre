using UnityEngine;
using UnityEngine.UI;

public class PowerUpCardUI : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI cardTitle;
    [SerializeField] TMPro.TextMeshProUGUI cardDescription;
    [SerializeField] Image cardImg;
    [SerializeField] Image panelBackground;

    [HideInInspector]
    public PowerUp CurrentPowerUp;

    public void SetTitle(string title)
    {
        cardTitle.text = title;
    }

    public void SetDescription(string description)
    {
        cardDescription.text = description;
    }

    public void SetImage(Image image)
    {
        cardImg.sprite = image.sprite;
    }

    public void SetBackground()
    {
        switch(CurrentPowerUp.rarity)
        {
            case RarityEnum.Common:
                panelBackground.color = RarityColor.Common;
                break;
            case RarityEnum.Rare:
                panelBackground.color = RarityColor.Rare;
                break;
            case RarityEnum.Epic:
                panelBackground.color = RarityColor.Epic;
                break;


        }
    }

    public void ClosePopup()
    {
        CurrentPowerUp.OnUse();
        UIManager.Instance.ClosePopup(UIManager.Instance.LevelUpPopup);
    }
}
