using MoreMountains.Feedbacks;
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
        //SpawnUi and selected powerup
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
