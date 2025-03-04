using MoreMountains.Feedbacks;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestLevelUp : InteractableBehaviour
{
    [SerializeField] private GameObject uiSelected;
    [SerializeField] private MMF_Player selectedFeedBack; // Not set yet
    [SerializeField] private ChestLevelUpData data;


    private PlayerMovement player;
    private void Start()
    {
        player = PlayerManager.Instance.playerMovement;
    }
    public override void OnInteract()
    {
        base.OnInteract();
        StartCoroutine(WaitToOpen());   
    }
    IEnumerator WaitToOpen()
    {
        selectedFeedBack.PlayFeedbacks();

        yield return new WaitForSeconds(data.timeToOpen);

        List<PowerUp> newPowerUp = new()
        {
            data.basicPowerUp[0],
            data.rarePowerUp[0],
            data.epicPowerUp[0],
        };

        UIManager.Instance.LevelUpPopup.SetPowerUp(newPowerUp);
        UIManager.Instance.AddPopup(UIManager.Instance.LevelUpPopup);
        Destroy(this);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !player.InteractableSelected)
        {
            player.InteractableSelected = this;
            uiSelected.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && player.InteractableSelected == this)
        {
            player.InteractableSelected = null;
            uiSelected.SetActive(false);
        }
    }

}
