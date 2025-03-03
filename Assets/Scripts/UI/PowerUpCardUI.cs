using UnityEngine;
using UnityEngine.UI;

public class PowerUpCardUI : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI cardTitle;
    [SerializeField] TMPro.TextMeshProUGUI cardDescription;
    [SerializeField] Image cardImg;

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

    public void ClosePopup()
    {
        UIManager.Instance.ClosePopup(UIManager.Instance.LevelUpPopup);
    }
}
