using System.Collections.Generic;
using UnityEngine;

public class LevelUpPopup : Popup
{
    [SerializeField] Transform cardParent;
    [SerializeField] PowerUpCardUI cardPrefab;

    private List<PowerUp> powerUpSelected = new();

    private List<GameObject> powerUpCardInstantier = new();

    public void SetPowerUp(List<PowerUp> _powerUpSelected)
    {
        powerUpSelected.Clear();
        powerUpSelected = _powerUpSelected;
    }

    public void DrawCard()
    {
        foreach (PowerUp powerUp in powerUpSelected) {

            PowerUpCardUI newCard = Instantiate(cardPrefab, cardParent);
            newCard.SetTitle(powerUp.title);
            newCard.SetDescription(powerUp.Description);
            powerUpCardInstantier.Add(newCard.gameObject);
        }
    }

    public override void OpenPopup()
    {
        base.OpenPopup();
        DrawCard();
        Time.timeScale = 0;
        PlayerManager.Instance.playerMovement.enableInput = false;
    }

    public override void ClosePopup()
    {
        base.ClosePopup();
        Time.timeScale = 1;
        PlayerManager.Instance.playerMovement.enableInput = true;

        foreach (GameObject power in powerUpCardInstantier)
        {
            Destroy(power);
        }
    }
}
